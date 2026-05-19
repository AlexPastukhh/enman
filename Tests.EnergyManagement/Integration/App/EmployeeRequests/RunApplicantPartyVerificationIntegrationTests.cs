using System.Data;
using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Testing.TestDatabase;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.EmployeeRequests;

[Collection("IntegrationTestCollection")]
public sealed class RunApplicantPartyVerificationIntegrationTests : AppIntegrationTestBase
{
    private const long CurrentEmployeeId = 700;

    public RunApplicantPartyVerificationIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task RunApplicantPartyVerification_WithoutAuth_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await RunApplicantPartyVerificationRequestAsync(client, 1);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RunApplicantPartyVerification_WithClientAccount_ReturnsForbidden()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await RunApplicantPartyVerificationRequestAsync(client, 1);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task RunApplicantPartyVerification_ForMissingRequest_ReturnsNotFound()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await RunApplicantPartyVerificationRequestAsync(client, 987654);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task RunApplicantPartyVerification_WithEmployeeRoleClaimButNoEmployeeAccount_ReturnsForbidden()
    {
        await ResetDatabaseAsync();
        var client = AuthenticatedEmployeeClient();

        var response = await RunApplicantPartyVerificationRequestAsync(client, 987654);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task RunApplicantPartyVerification_WhenEmployeeAndValidRequest_ReturnsOkAndMarksApplicantPartyVerified()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Verification command request.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();
        var agreementExchangeCountBefore = await GetAgreementExchangeCountAsync();

        var response = await RunApplicantPartyVerificationRequestAsync(client, request!.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<RunApplicantPartyVerificationResponseDto>()
            ?? throw new InvalidOperationException("Verification response body was empty.");
        result.RequestId.Should().Be(request.Id);
        result.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        result.VerificationStatus.Should().Be("Verified");
        result.MockResult.Should().Be("Passed");
        result.Message.Should().Be("Mock verification passed.");

        var applicantAfter = await GetApplicantPartyRowAsync(applicantParty.ApplicantPartyId);
        applicantAfter!.VerificationStatus.Should().Be("Verified");

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");

        var reviewAfter = await GetRequestReviewRowAsync(request.Id);
        reviewAfter.Should().BeNull();

        var agreementExchangeCountAfter = await GetAgreementExchangeCountAsync();
        agreementExchangeCountAfter.Should().Be(agreementExchangeCountBefore);
    }

    [Fact]
    public async Task RunApplicantPartyVerification_DoesNotChangeStartedReviewState()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Verification does not change review.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var startedAt = DateTimeOffset.UtcNow.AddMinutes(-10);
        await InsertRequestReviewAsync(
            request!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: startedAt);
        var client = AuthenticatedEmployeeClient();

        var response = await RunApplicantPartyVerificationRequestAsync(client, request.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.OK);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");

        var reviewAfter = await GetRequestReviewRowAsync(request.Id);
        reviewAfter.Should().NotBeNull();
        reviewAfter!.Status.Should().Be("Started");
        reviewAfter.StartedByEmployeeId.Should().Be(CurrentEmployeeId);
        reviewAfter.StartedAt.Should().Be(startedAt);
        reviewAfter.CompletedByEmployeeId.Should().BeNull();
        reviewAfter.CompletedAt.Should().BeNull();
        reviewAfter.RejectionReason.Should().BeNull();
    }

    private Task<HttpResponseMessage> RunApplicantPartyVerificationRequestAsync(
        HttpClient client,
        long requestId)
    {
        return PostWithCsrfAsync(
            client,
            $"/api/employee/requests/{requestId}/applicant-party/verification/run");
    }

    private HttpClient AuthenticatedEmployeeClient()
    {
        return AuthenticatedL1Client(
            CurrentEmployeeId,
            email: "employee@example.com",
            role: "Employee");
    }

    private async Task<int> GetAgreementExchangeCountAsync()
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "SELECT COUNT(*) FROM dbo.L1AgreementProposalExchanges",
            connection)
        {
            CommandType = CommandType.Text
        };

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not read L1AgreementProposalExchanges count.");

        return (int)scalar;
    }

    private Task ResetDatabaseAsync()
    {
        return new TestDatabaseManager(_fixture.ConnectionString).ResetAsync();
    }
}
