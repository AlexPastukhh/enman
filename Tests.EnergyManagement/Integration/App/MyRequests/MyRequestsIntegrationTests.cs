using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.MyRequests;

[Collection("IntegrationTestCollection")]
public sealed class MyRequestsIntegrationTests : AppIntegrationTestBase
{
    public MyRequestsIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task ListMyRequests_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/requests");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ListMyRequests_WithNoRequests_ReturnsEmptyArray()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var requests = await GetMyRequestsAsync(client);

        requests.Should().BeEmpty();
    }

    [Fact]
    public async Task ListMyRequests_ReturnsCurrentAccountRequestSummaries()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client);
        var persisted = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        requests.Should().ContainSingle();
        var request = requests.Single();
        request.RequestId.Should().Be(persisted!.Id);
        request.RequestType.Should().Be("Connection");
        request.Status.Should().Be("InReview");
        request.Summary.Should().Be(RequestDetails);
        request.ObjectAddress.City.Should().Be(City);
        request.ObjectAddress.Street.Should().Be(Street);
    }

    [Fact]
    public async Task ListMyRequests_DoesNotReturnAnotherAccountRequests()
    {
        var accountWithRequest = await RegisterAccountAsync();
        await CreateApplicantPartyAsync(accountWithRequest.AccountId);
        await CreateConnectionRequestAsync(accountWithRequest.AccountId);

        var accountWithoutRequest = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(
            accountWithoutRequest.AccountId,
            accountWithoutRequest.Email);

        var requests = await GetMyRequestsAsync(client);

        requests.Should().BeEmpty();
    }

    [Fact]
    public async Task ListMyRequests_WithStatusFilter_ReturnsMatchingRequests()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "First in-review request.");
        var inReview = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Second approved request.");
        var approved = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(approved!.Id, "Approved");

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client, status: "Approved");

        requests.Should().ContainSingle();
        requests.Single().RequestId.Should().Be(approved.Id);
        requests.Single().Status.Should().Be("Approved");
        requests.Single().RequestId.Should().NotBe(inReview!.Id);
    }

    [Fact]
    public async Task ListMyRequests_WithInvalidStatusFilter_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/requests?status=Done");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task ListMyRequests_WithEmptyStatusFilter_ReturnsSuccess()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/requests?status=");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task ListMyRequests_ReturnsNewestFirst()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Older request.");
        var older = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(older!.Id, DateTimeOffset.UtcNow.AddDays(-1));

        await CreateConnectionRequestAsync(account.AccountId, details: "Newer request.");
        var newer = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(newer!.Id, DateTimeOffset.UtcNow);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client);

        requests.Should().HaveCount(2);
        requests[0].RequestId.Should().Be(newer.Id);
        requests[1].RequestId.Should().Be(older.Id);
    }

    [Fact]
    public async Task GetMyRequestDetails_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/requests/1");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForOwnInReviewRequest_ReturnsSubmittedRequestData()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row!.Id);

        details.RequestId.Should().Be(row.Id);
        details.RequestType.Should().Be("Connection");
        details.Status.Should().Be("InReview");
        details.CreatedAt.Should().NotBe(default);
        details.SubmittedRequest.Details.Should().Be(RequestDetails);
        details.SubmittedRequest.ObjectAddress.City.Should().Be(City);
        details.SubmittedRequest.ObjectAddress.Street.Should().Be(Street);
        details.ReviewResult.Should().BeNull();
    }

    [Fact]
    public async Task GetMyRequestDetails_ForMissingRequest_ReturnsNotFound()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/requests/987654");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForAnotherAccountRequest_ReturnsNotFound()
    {
        var accountWithRequest = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(accountWithRequest.AccountId);
        await CreateConnectionRequestAsync(accountWithRequest.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        var anotherAccount = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(anotherAccount.AccountId, anotherAccount.Email);

        var response = await client.GetAsync($"/api/requests/{row!.Id}");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForRejectedRequest_ReturnsFeedbackAndSubmittedData()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(-5);
        const string rejectionReason = "Need more object address details.";
        await UpdateRequestReviewAsync(
            row!.Id,
            status: "Rejected",
            decision: "Rejected",
            decidedAt,
            rejectionReason);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row.Id);

        details.Status.Should().Be("Rejected");
        details.SubmittedRequest.Details.Should().Be(RequestDetails);
        details.SubmittedRequest.ObjectAddress.City.Should().Be(City);
        details.ReviewResult.Should().NotBeNull();
        details.ReviewResult!.Decision.Should().Be("Rejected");
        details.ReviewResult.DecidedAt.Should().BeCloseTo(decidedAt, TimeSpan.FromSeconds(1));
        details.ReviewResult.Rejection.Should().NotBeNull();
        details.ReviewResult.Rejection!.Reason.Should().Be(rejectionReason);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForApprovedRequest_ReturnsApprovedDecision()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(-3);
        await UpdateRequestReviewAsync(
            row!.Id,
            status: "Approved",
            decision: "Approved",
            decidedAt,
            rejectionReason: null);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row.Id);

        details.Status.Should().Be("Approved");
        details.ReviewResult.Should().NotBeNull();
        details.ReviewResult!.Decision.Should().Be("Approved");
        details.ReviewResult.DecidedAt.Should().BeCloseTo(decidedAt, TimeSpan.FromSeconds(1));
        details.ReviewResult.Rejection.Should().BeNull();
    }

}

