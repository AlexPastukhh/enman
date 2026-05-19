using System.Net;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Testing.TestDatabase;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.EmployeeRequests;

[Collection("IntegrationTestCollection")]
public sealed class EmployeeRequestListIntegrationTests : AppIntegrationTestBase
{
    private const long CurrentEmployeeId = 700;
    private const long OtherEmployeeId = 701;

    public EmployeeRequestListIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task ListEmployeeRequests_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/employee/requests");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ListEmployeeRequests_WithClientAccount_ReturnsForbidden()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/employee/requests");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ListEmployeeRequests_WithNoRequests_ReturnsEmptyList()
    {
        await ResetDatabaseAsync();
        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(client);

        response.Requests.Should().BeEmpty();
    }

    [Fact]
    public async Task ListEmployeeRequests_ReturnsCompactRowsWithReviewStates()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "No review yet.");
        var notStarted = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Started by current employee.");
        var startedByCurrent = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await InsertRequestReviewAsync(
            startedByCurrent!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-20));

        await CreateConnectionRequestAsync(account.AccountId, details: "Started by another employee.");
        var startedByAnother = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await InsertRequestReviewAsync(
            startedByAnother!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: OtherEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-15));

        await CreateConnectionRequestAsync(account.AccountId, details: "Approved request.");
        var approved = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(approved!.Id, "Approved");
        await InsertRequestReviewAsync(
            approved.Id,
            reviewStatus: "Approved",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            completedByEmployeeId: CurrentEmployeeId,
            completedAt: DateTimeOffset.UtcNow.AddMinutes(-5));

        await CreateConnectionRequestAsync(account.AccountId, details: "Rejected request.");
        var rejected = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(rejected!.Id, "Rejected");
        await InsertRequestReviewAsync(
            rejected.Id,
            reviewStatus: "Rejected",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            completedByEmployeeId: CurrentEmployeeId,
            completedAt: DateTimeOffset.UtcNow.AddMinutes(-5),
            rejectionFeedback: "Need more data.");

        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(client);

        response.Requests.Should().HaveCount(5);
        response.Requests.Single(x => x.RequestId == notStarted!.Id).ReviewState.Should().Be("NotStarted");
        response.Requests.Single(x => x.RequestId == startedByCurrent.Id).ReviewState.Should().Be("StartedByCurrentEmployee");
        response.Requests.Single(x => x.RequestId == startedByAnother.Id).ReviewState.Should().Be("StartedByAnotherEmployee");
        response.Requests.Single(x => x.RequestId == approved.Id).ReviewState.Should().Be("Approved");
        response.Requests.Single(x => x.RequestId == rejected.Id).ReviewState.Should().Be("Rejected");

        var sample = response.Requests.Single(x => x.RequestId == notStarted!.Id);
        sample.RequestType.Should().Be("Connection");
        sample.Status.Should().Be("InReview");
        sample.ApplicantDisplayName.Should().Be($"{LastName} {FirstName} {MiddleName}");
        sample.ObjectAddress.Should().Contain(City);
        sample.ObjectAddress.Should().Contain(Street);
        sample.CreatedAt.Should().NotBe(default);
    }

    [Fact]
    public async Task ListEmployeeRequests_IncludesUnverifiedApplicantVerificationSummary()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(client);

        var verification = response.Requests
            .Single(x => x.RequestId == request!.Id)
            .ApplicantVerification;
        verification.Should().NotBeNull();
        verification!.Required.Should().BeTrue();
        verification.Status.Should().Be("Unverified");
        verification.CanRun.Should().BeTrue();
        verification.Message.Should().Be("Данные не проверены");
    }

    [Fact]
    public async Task ListEmployeeRequests_IncludesVerifiedApplicantVerificationSummary()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await UpdateApplicantVerificationStatusAsync(applicantParty.ApplicantPartyId, "Verified");
        await CreateConnectionRequestAsync(account.AccountId);
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(client);

        var verification = response.Requests
            .Single(x => x.RequestId == request!.Id)
            .ApplicantVerification;
        verification.Should().NotBeNull();
        verification!.Required.Should().BeTrue();
        verification.Status.Should().Be("Verified");
        verification.CanRun.Should().BeFalse();
        verification.Message.Should().Be("Данные проверены");
    }

    [Fact]
    public async Task ListEmployeeRequests_WithStatusFilter_ReturnsMatchingRows()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "In review request.");

        await CreateConnectionRequestAsync(account.AccountId, details: "Approved request.");
        var approved = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(approved!.Id, "Approved");
        await InsertRequestReviewAsync(
            approved.Id,
            reviewStatus: "Approved",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            completedByEmployeeId: CurrentEmployeeId,
            completedAt: DateTimeOffset.UtcNow.AddMinutes(-5));

        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(client, status: "Approved");

        response.Requests.Should().ContainSingle();
        response.Requests.Single().RequestId.Should().Be(approved.Id);
        response.Requests.Single().Status.Should().Be("Approved");
    }

    [Fact]
    public async Task ListEmployeeRequests_WithReviewStateFilter_ReturnsMatchingRows()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Started by current employee.");
        var startedByCurrent = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await InsertRequestReviewAsync(
            startedByCurrent!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-20));

        await CreateConnectionRequestAsync(account.AccountId, details: "Started by another employee.");
        var startedByAnother = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await InsertRequestReviewAsync(
            startedByAnother!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: OtherEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-15));

        var client = AuthenticatedEmployeeClient();

        var response = await GetEmployeeRequestsAsync(
            client,
            reviewState: "StartedByAnotherEmployee");

        response.Requests.Should().ContainSingle();
        response.Requests.Single().RequestId.Should().Be(startedByAnother.Id);
        response.Requests.Single().ReviewState.Should().Be("StartedByAnotherEmployee");
    }

    [Fact]
    public async Task ListEmployeeRequests_WithInvalidStatusFilter_ReturnsValidationProblem()
    {
        var client = AuthenticatedEmployeeClient();

        var response = await client.GetAsync("/api/employee/requests?status=Done");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task ListEmployeeRequests_WithInvalidReviewStateFilter_ReturnsValidationProblem()
    {
        var client = AuthenticatedEmployeeClient();

        var response = await client.GetAsync("/api/employee/requests?reviewState=OwnedByMe");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    private HttpClient AuthenticatedEmployeeClient()
    {
        return AuthenticatedL1Client(
            CurrentEmployeeId,
            email: "employee@example.com",
            role: "Employee");
    }

    private Task ResetDatabaseAsync()
    {
        return new TestDatabaseManager(_fixture.ConnectionString).ResetAsync();
    }
}
