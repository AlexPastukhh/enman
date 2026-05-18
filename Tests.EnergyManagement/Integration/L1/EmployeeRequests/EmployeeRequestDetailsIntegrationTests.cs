using System.Net;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.EmployeeRequests;

[Collection("IntegrationTestCollection")]
public sealed class EmployeeRequestDetailsIntegrationTests : L1IntegrationTestBase
{
    private const long CurrentEmployeeId = 700;
    private const long OtherEmployeeId = 701;

    public EmployeeRequestDetailsIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/employee/requests/1");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_WithClientAccount_ReturnsForbidden()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/employee/requests/1");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_ForMissingRequest_ReturnsNotFound()
    {
        var client = AuthenticatedEmployeeClient();

        var response = await client.GetAsync("/api/employee/requests/987654");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_ReturnsDetailsPayloadWithNotStartedReview()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Employee details request body.");
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var details = await GetEmployeeRequestDetailsAsync(client, row!.Id);

        details.RequestId.Should().Be(row.Id);
        details.RequestType.Should().Be("Connection");
        details.Status.Should().Be("InReview");
        details.Details.Should().Be("Employee details request body.");
        details.CreatedAt.Should().NotBe(default);
        details.ObjectAddress.Should().Contain(City);
        details.ObjectAddress.Should().Contain(Street);
        details.Applicant.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        details.Applicant.ApplicantPartyType.Should().Be("Individual");
        details.Applicant.DisplayName.Should().Be($"{LastName} {FirstName} {MiddleName}");
        details.Applicant.Email.Should().Be(ApplicantEmail);
        details.Applicant.PhoneNumber.Should().Be(PhoneNumber);
        details.ReviewState.Should().Be("NotStarted");
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_IncludesUnverifiedApplicantVerificationSummary()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var details = await GetEmployeeRequestDetailsAsync(client, row!.Id);

        details.ApplicantVerification.Should().NotBeNull();
        details.ApplicantVerification!.Required.Should().BeTrue();
        details.ApplicantVerification.Status.Should().Be("Unverified");
        details.ApplicantVerification.CanRun.Should().BeTrue();
        details.ApplicantVerification.Message.Should().Be("Данные не проверены");
        details.ReviewState.Should().Be("NotStarted");
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_IncludesVerifiedApplicantVerificationSummary()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await UpdateApplicantVerificationStatusAsync(applicantParty.ApplicantPartyId, "Verified");
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var details = await GetEmployeeRequestDetailsAsync(client, row!.Id);

        details.ApplicantVerification.Should().NotBeNull();
        details.ApplicantVerification!.Required.Should().BeTrue();
        details.ApplicantVerification.Status.Should().Be("Verified");
        details.ApplicantVerification.CanRun.Should().BeFalse();
        details.ApplicantVerification.Message.Should().Be("Данные проверены");
        details.ReviewState.Should().Be("NotStarted");
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_ReturnsStartedReviewStateForCurrentAndAnotherEmployee()
    {
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
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-10));

        var client = AuthenticatedEmployeeClient();

        var currentDetails = await GetEmployeeRequestDetailsAsync(client, startedByCurrent.Id);
        var anotherDetails = await GetEmployeeRequestDetailsAsync(client, startedByAnother.Id);

        currentDetails.ReviewState.Should().Be("StartedByCurrentEmployee");
        anotherDetails.ReviewState.Should().Be("StartedByAnotherEmployee");
    }

    [Fact]
    public async Task GetEmployeeRequestDetails_ReturnsCompletedReviewStates()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Approved request.");
        var approved = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(approved!.Id, "Approved");
        await InsertRequestReviewAsync(
            approved.Id,
            reviewStatus: "Approved",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-20),
            completedByEmployeeId: CurrentEmployeeId,
            completedAt: DateTimeOffset.UtcNow.AddMinutes(-10));

        await CreateConnectionRequestAsync(account.AccountId, details: "Rejected request.");
        var rejected = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(rejected!.Id, "Rejected");
        await InsertRequestReviewAsync(
            rejected.Id,
            reviewStatus: "Rejected",
            startedByEmployeeId: CurrentEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-20),
            completedByEmployeeId: CurrentEmployeeId,
            completedAt: DateTimeOffset.UtcNow.AddMinutes(-10),
            rejectionFeedback: "Need more data.");

        var client = AuthenticatedEmployeeClient();

        var approvedDetails = await GetEmployeeRequestDetailsAsync(client, approved.Id);
        var rejectedDetails = await GetEmployeeRequestDetailsAsync(client, rejected.Id);

        approvedDetails.ReviewState.Should().Be("Approved");
        rejectedDetails.ReviewState.Should().Be("Rejected");
    }

    private HttpClient AuthenticatedEmployeeClient()
    {
        return AuthenticatedL1Client(
            CurrentEmployeeId,
            email: "employee@example.com",
            role: "Employee");
    }
}
