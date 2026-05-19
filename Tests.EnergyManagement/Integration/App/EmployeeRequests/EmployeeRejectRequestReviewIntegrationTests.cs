using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Api;
using EnergyManagement.Testing.TestDatabase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.EmployeeRequests;

[Collection("IntegrationTestCollection")]
public sealed class EmployeeRejectRequestReviewIntegrationTests : AppIntegrationTestBase
{
    private const long CurrentEmployeeId = 700;
    private const long OtherEmployeeId = 701;
    private const string RejectionReason = "Applicant must provide additional documents.";

    public EmployeeRejectRequestReviewIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task RejectRequestReview_WithoutAuth_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, 1, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RejectRequestReview_WithClientAccount_ReturnsForbidden()
    {
        await ResetDatabaseAsync();
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email);

        var response = await RejectEmployeeRequestReviewRequestAsync(client, 1, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task RejectRequestReview_WithoutCsrfToken_ReturnsAntiforgeryProblem()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await client.PostAsJsonAsync(
            "/api/employee/requests/1/review/reject",
            new EmployeeRejectRequestReviewDto(RejectionReason));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>()
            ?? throw new InvalidOperationException("ProblemDetails response body was empty.");
        problem.Extensions["code"]!.ToString().Should().Contain(AntiforgeryConstants.FailureCode);
    }

    [Fact]
    public async Task RejectRequestReview_ForMissingRequest_ReturnsNotFound()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, 987654, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task RejectRequestReview_WithMissingFeedback_RejectsWithoutRejectionReason(string? feedback)
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var request = await CreateStartedRequestAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, feedback);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("Rejected");
        var review = await GetRequestReviewRowAsync(request.Id);
        review!.Status.Should().Be("Rejected");
        review.CompletedByEmployeeId.Should().Be(CurrentEmployeeId);
        review.RejectionReason.Should().BeNull();
    }

    [Fact]
    public async Task RejectRequestReview_WithTooLongFeedback_ReturnsValidationProblemAndDoesNotMutate()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var request = await CreateStartedRequestAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();
        var feedback = new string('x', global::Domain.EnergyManagement.RejectionFeedback.MaxLength + 1);

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, feedback);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");
        var review = await GetRequestReviewRowAsync(request.Id);
        review!.Status.Should().Be("Started");
        review.CompletedByEmployeeId.Should().BeNull();
        review.RejectionReason.Should().BeNull();
    }

    [Fact]
    public async Task RejectRequestReview_WhenReviewNotStarted_ReturnsValidationProblemAndDoesNotMutate()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var request = await CreateRequestAsync("Not started rejection request.");
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");
        (await GetRequestReviewRowAsync(request.Id)).Should().BeNull();
    }

    [Fact]
    public async Task RejectRequestReview_WhenStartedByAnotherEmployee_ReturnsValidationProblemAndDoesNotMutate()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        await InsertEmployeeAsync(OtherEmployeeId);
        var request = await CreateStartedRequestAsync(OtherEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("InReview");
        var review = await GetRequestReviewRowAsync(request.Id);
        review!.Status.Should().Be("Started");
        review.StartedByEmployeeId.Should().Be(OtherEmployeeId);
        review.CompletedByEmployeeId.Should().BeNull();
        review.RejectionReason.Should().BeNull();
    }

    [Fact]
    public async Task RejectRequestReview_RejectsStartedReviewAndReturnsNoContent()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var request = await CreateStartedRequestAsync(CurrentEmployeeId);
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);
        (await response.Content.ReadAsStringAsync()).Should().BeEmpty();

        var requestAfter = await GetRequestRowAsync(request.Id);
        requestAfter!.Status.Should().Be("Rejected");

        var review = await GetRequestReviewRowAsync(request.Id);
        review.Should().NotBeNull();
        review!.Status.Should().Be("Rejected");
        review.StartedByEmployeeId.Should().Be(CurrentEmployeeId);
        review.CompletedByEmployeeId.Should().Be(CurrentEmployeeId);
        review.CompletedAt.Should().NotBeNull();
        review.RejectionReason.Should().Be(RejectionReason);

        var details = await GetEmployeeRequestDetailsAsync(client, request.Id);
        details.Status.Should().Be("Rejected");
        details.ReviewState.Should().Be("Rejected");
    }

    [Fact]
    public async Task RejectRequestReview_WhenAlreadyRejected_ReturnsValidationProblemAndDoesNotChangeReview()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(CurrentEmployeeId);
        var request = await CreateRequestAsync("Already rejected request.");
        var rejectedAt = DateTimeOffset.UtcNow.AddMinutes(-5);
        await UpdateRequestStatusAsync(request.Id, "Rejected");
        await InsertCompletedReviewStateAsync(request.Id, "Rejected", CurrentEmployeeId, rejectedAt, "Already rejected.");
        var client = AuthenticatedEmployeeClient();

        var response = await RejectEmployeeRequestReviewRequestAsync(client, request.Id, RejectionReason);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var review = await GetRequestReviewRowAsync(request.Id);
        review!.Status.Should().Be("Rejected");
        review.CompletedByEmployeeId.Should().Be(CurrentEmployeeId);
        review.CompletedAt.Should().Be(rejectedAt);
        review.RejectionReason.Should().Be("Already rejected.");
    }

    private async Task<RequestRow> CreateRequestAsync(string details)
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, details: details);
        return await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId)
            ?? throw new InvalidOperationException("Request was not created.");
    }

    private async Task<RequestRow> CreateStartedRequestAsync(long startedByEmployeeId)
    {
        var request = await CreateRequestAsync("Reject review command request.");
        await InsertRequestReviewAsync(
            request.Id,
            reviewStatus: "Started",
            startedByEmployeeId: startedByEmployeeId,
            startedAt: DateTimeOffset.UtcNow.AddMinutes(-15));
        return request;
    }

    private async Task InsertCompletedReviewStateAsync(
        long requestId,
        string reviewStatus,
        long employeeId,
        DateTimeOffset completedAt,
        string? rejectionReason)
    {
        await InsertRequestReviewAsync(
            requestId,
            reviewStatus: reviewStatus,
            startedByEmployeeId: employeeId,
            startedAt: completedAt.AddMinutes(-5),
            completedByEmployeeId: employeeId,
            completedAt: completedAt,
            rejectionFeedback: rejectionReason);
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
