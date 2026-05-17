using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.L1Domain;
using Tests.EnergyManagement.TestHelpers.L1;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.Requests;

public class ConnectionRequestL2ReviewTests
{
    [Fact]
    public void StartReview_creates_started_review_for_persisted_in_review_request()
    {
        var request = CreatePersistedInReviewRequest();
        var employee = CreatePersistedEmployee(id: 7);
        var startedAt = DateTimeOffset.UtcNow;

        var result = request.StartReview(employee, startedAt);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.InReview);
        request.Review.Should().NotBeNull();
        request.Review!.Status.Should().Be(RequestReviewStatus.Started);
        request.Review.StartedByEmployeeId.Should().Be(employee.Id);
        request.Review.StartedAt.Should().Be(startedAt);
    }

    [Fact]
    public void StartReview_fails_when_request_is_not_in_review()
    {
        var request = CreateApprovedRequest();
        var originalReview = request.Review;

        var result = request.StartReview(
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OnlyInReviewRequestCanStartReview);
        request.Status.Should().Be(RequestStatus.Approved);
        request.Review.Should().BeSameAs(originalReview);
    }

    [Fact]
    public void StartReview_fails_when_review_is_already_started()
    {
        var request = CreatePersistedInReviewRequest();
        request.StartReview(CreatePersistedEmployee(id: 7), DateTimeOffset.UtcNow);
        var originalReview = request.Review;

        var result = request.StartReview(
            CreatePersistedEmployee(id: 8),
            DateTimeOffset.UtcNow.AddMinutes(1));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestReviewAlreadyStarted);
        request.Status.Should().Be(RequestStatus.InReview);
        request.Review.Should().BeSameAs(originalReview);
    }

    [Fact]
    public void StartReview_on_transient_request_returns_failure()
    {
        var request = CreateInReviewRequest();

        var result = request.StartReview(
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestIsRequired);
        request.Review.Should().BeNull();
    }

    [Fact]
    public void ApproveReview_requires_started_review()
    {
        var request = CreatePersistedInReviewRequest();

        var result = request.ApproveReview(
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestReviewMustBeStarted);
        request.Status.Should().Be(RequestStatus.InReview);
    }

    [Fact]
    public void ApproveReview_by_review_starter_approves_request_and_review()
    {
        var request = CreatePersistedInReviewRequest();
        var employee = CreatePersistedEmployee(id: 7);
        request.StartReview(employee, DateTimeOffset.UtcNow);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(10);

        var result = request.ApproveReview(employee, decidedAt);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Approved);
        request.Review!.Status.Should().Be(RequestReviewStatus.Approved);
        request.Review.CompletedByEmployeeId.Should().Be(employee.Id);
        request.Review.CompletedAt.Should().Be(decidedAt);
        request.Review.RejectionFeedback.Should().BeNull();
    }

    [Fact]
    public void ApproveReview_by_another_employee_fails_without_mutation()
    {
        var request = CreatePersistedInReviewRequest();
        var starter = CreatePersistedEmployee(id: 7);
        request.StartReview(starter, DateTimeOffset.UtcNow);
        var review = request.Review!;

        var result = request.ApproveReview(
            CreatePersistedEmployee(id: 8),
            DateTimeOffset.UtcNow.AddMinutes(10));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestReviewStartedByAnotherEmployee);
        request.Status.Should().Be(RequestStatus.InReview);
        review.Status.Should().Be(RequestReviewStatus.Started);
        review.CompletedByEmployeeId.Should().BeNull();
        review.CompletedAt.Should().BeNull();
    }

    [Fact]
    public void RejectReview_requires_started_review()
    {
        var request = CreatePersistedInReviewRequest();

        var result = request.RejectReview(
            CreatePersistedEmployee(id: 7),
            null,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestReviewMustBeStarted);
        request.Status.Should().Be(RequestStatus.InReview);
    }

    [Fact]
    public void RejectReview_by_review_starter_rejects_request_and_review()
    {
        var request = CreatePersistedInReviewRequest();
        var employee = CreatePersistedEmployee(id: 7);
        var feedback = RejectionFeedback.Create("Need additional documents.").Value;
        request.StartReview(employee, DateTimeOffset.UtcNow);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(10);

        var result = request.RejectReview(employee, feedback, decidedAt);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Rejected);
        request.Review!.Status.Should().Be(RequestReviewStatus.Rejected);
        request.Review.CompletedByEmployeeId.Should().Be(employee.Id);
        request.Review.CompletedAt.Should().Be(decidedAt);
        request.Review.RejectionFeedback.Should().Be(feedback);
    }

    [Fact]
    public void RejectReview_by_another_employee_fails_without_mutation()
    {
        var request = CreatePersistedInReviewRequest();
        var starter = CreatePersistedEmployee(id: 7);
        request.StartReview(starter, DateTimeOffset.UtcNow);
        var review = request.Review!;

        var result = request.RejectReview(
            CreatePersistedEmployee(id: 8),
            RejectionFeedback.Create("No.").Value,
            DateTimeOffset.UtcNow.AddMinutes(10));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestReviewStartedByAnotherEmployee);
        request.Status.Should().Be(RequestStatus.InReview);
        review.Status.Should().Be(RequestReviewStatus.Started);
        review.CompletedByEmployeeId.Should().BeNull();
        review.CompletedAt.Should().BeNull();
        review.RejectionFeedback.Should().BeNull();
    }

    private static ConnectionRequest CreateApprovedRequest()
    {
        var request = CreatePersistedInReviewRequest();
        var employee = CreatePersistedEmployee(id: 7);

        request.StartReview(employee, DateTimeOffset.UtcNow);
        request.ApproveReview(employee, DateTimeOffset.UtcNow.AddMinutes(1));

        return request;
    }

    private static ConnectionRequest CreatePersistedInReviewRequest()
    {
        return CreateInReviewRequest().WithId(100);
    }

    private static ConnectionRequest CreateInReviewRequest()
    {
        return ConnectionRequest.Create(
            CreatePersistedApplicant(),
            L1ValidTestData.RequestDetails,
            L1ValidTestData.Address).Value;
    }

    private static IndividualApplicantParty CreatePersistedApplicant()
    {
        return IndividualApplicantParty.Create(
            clientAccountId: 10,
            L1ValidTestData.FullName,
            L1ValidTestData.Email,
            L1ValidTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value.WithId(42);
    }

    private static Employee CreatePersistedEmployee(long id)
    {
        return Employee.Create(
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow).Value.WithId(id);
    }
}
