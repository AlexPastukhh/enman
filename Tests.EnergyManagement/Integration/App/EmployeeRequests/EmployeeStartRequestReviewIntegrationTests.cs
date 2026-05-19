using System.Net;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Testing.TestDatabase;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.EmployeeRequests;

[Collection("IntegrationTestCollection")]
public sealed class EmployeeStartRequestReviewIntegrationTests : AppIntegrationTestBase
{
    private const long CurrentEmployeeId = 700;
    private const long OtherEmployeeId = 701;

    public EmployeeStartRequestReviewIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task StartRequestReview_WithoutAuth_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await PostWithCsrfAsync(
            client,
            "/api/employee/requests/1/review/start");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task StartRequestReview_WithClientAccount_ReturnsForbidden()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email);

        var response = await PostWithCsrfAsync(
            client,
            "/api/employee/requests/1/review/start");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task StartRequestReview_ForMissingRequest_ReturnsNotFound()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, 987654);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task StartRequestReview_WithEmployeeRoleClaimButNoEmployeeAccount_ReturnsForbidden()
    {
        await ResetDatabaseAsync();
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, 987654);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task StartRequestReview_StartsReviewAndReturnsNoContent()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Start review command request.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, request!.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);
        (await response.Content.ReadAsStringAsync()).Should().BeEmpty();

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");

        var review = await GetRequestReviewRowAsync(request.Id);
        review.Should().NotBeNull();
        review!.Status.Should().Be("Started");
        review.StartedByEmployeeId.Should().Be(CurrentEmployeeId);
        review.StartedAt.Should().NotBe(default);
        review.CompletedByEmployeeId.Should().BeNull();
        review.CompletedAt.Should().BeNull();
        review.RejectionReason.Should().BeNull();

        var details = await GetEmployeeRequestDetailsAsync(client, request.Id);
        details.ReviewState.Should().Be("StartedByCurrentEmployee");

        var employeeAccount = await GetAccountRowAsync(CurrentEmployeeId);
        employeeAccount.Should().NotBeNull();
        employeeAccount!.AccountType.Should().Be("Employee");
        employeeAccount.Role.Should().Be("Employee");
    }

    [Fact]
    public async Task StartRequestReview_WithInactiveEmployee_ReturnsValidationProblemAndDoesNotCreateReview()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId, isActive: false);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Inactive employee cannot start review.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, request!.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        (await GetRequestReviewRowAsync(request.Id)).Should().BeNull();
    }

    [Fact]
    public async Task StartRequestReview_WhenAlreadyStarted_ReturnsValidationProblemAndDoesNotChangeReview()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Already started request.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var startedAt = DateTimeOffset.UtcNow.AddMinutes(-30);
        await InsertRequestReviewAsync(
            request!.Id,
            reviewStatus: "Started",
            startedByEmployeeId: OtherEmployeeId,
            startedAt: startedAt);
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, request.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var review = await GetRequestReviewRowAsync(request.Id);
        review.Should().NotBeNull();
        review!.Status.Should().Be("Started");
        review.StartedByEmployeeId.Should().Be(OtherEmployeeId);
        review.StartedAt.Should().Be(startedAt);
        review.CompletedByEmployeeId.Should().BeNull();
    }

    [Fact]
    public async Task StartRequestReview_WhenRequestIsNotInReview_ReturnsValidationProblemAndDoesNotCreateReview()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: "Approved request.");
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(request!.Id, "Approved");
        var client = AuthenticatedEmployeeClient();

        var response = await StartEmployeeRequestReviewRequestAsync(client, request.Id);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("Approved");
        (await GetRequestReviewRowAsync(request.Id)).Should().BeNull();
    }

    private HttpClient AuthenticatedEmployeeClient()
    {
        return AuthenticatedClient(
            CurrentEmployeeId,
            email: "employee@example.com",
            role: "Employee");
    }

    private Task ResetDatabaseAsync()
    {
        return new TestDatabaseManager(_fixture.ConnectionString).ResetAsync();
    }
}
