using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.L1Domain;
using Tests.EnergyManagement.TestHelpers.L1;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.Requests;

public class ConnectionRequestReviewTests
{
    [Fact]
    public void Approve_marks_in_review_request_as_approved()
    {
        var request = CreateInReviewRequest();
        var reviewer = EmployeeRef.Create(5).Value;

        var result = request.Approve(reviewer);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Approved);
    }

    [Fact]
    public void Approve_records_review_decision()
    {
        var request = CreateInReviewRequest();
        var reviewer = EmployeeRef.Create(5).Value;

        request.Approve(reviewer);

        request.ReviewDecision.Should().NotBeNull();
        request.ReviewDecision!.Decision.Should().Be(ReviewDecision.Approved);
        request.ReviewDecision.Reviewer.Should().Be(reviewer);
        request.ReviewDecision.RejectionFeedback.Should().BeNull();
    }

    [Fact]
    public void Approve_fails_when_request_is_not_in_review()
    {
        var request = CreateApprovedRequest();

        var result = request.Approve(EmployeeRef.Create(5).Value);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OnlyInReviewRequestCanBeApproved);
    }

    [Fact]
    public void Failed_approve_does_not_change_state()
    {
        var request = CreateRejectedRequest();
        var originalDecision = request.ReviewDecision;

        var result = request.Approve(EmployeeRef.Create(5).Value);

        result.IsFailure.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Rejected);
        request.ReviewDecision.Should().BeSameAs(originalDecision);
    }

    [Fact]
    public void Reject_marks_in_review_request_as_rejected()
    {
        var request = CreateInReviewRequest();

        var result = request.Reject(EmployeeRef.Create(5).Value, null);

        result.IsSuccess.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Rejected);
    }

    [Fact]
    public void Reject_allows_null_feedback()
    {
        var request = CreateInReviewRequest();

        var result = request.Reject(EmployeeRef.Create(5).Value, null);

        result.IsSuccess.Should().BeTrue();
        request.ReviewDecision.Should().NotBeNull();
        request.ReviewDecision!.Decision.Should().Be(ReviewDecision.Rejected);
        request.ReviewDecision.RejectionFeedback.Should().BeNull();
    }

    [Fact]
    public void Reject_fails_when_request_is_not_in_review()
    {
        var request = CreateRejectedRequest();

        var result = request.Reject(EmployeeRef.Create(5).Value, null);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OnlyInReviewRequestCanBeRejected);
    }

    [Fact]
    public void Failed_reject_does_not_change_state()
    {
        var request = CreateApprovedRequest();
        var originalDecision = request.ReviewDecision;

        var result = request.Reject(EmployeeRef.Create(5).Value, null);

        result.IsFailure.Should().BeTrue();
        request.Status.Should().Be(RequestStatus.Approved);
        request.ReviewDecision.Should().BeSameAs(originalDecision);
    }

    private static ConnectionRequest CreateApprovedRequest()
    {
        var request = CreateInReviewRequest();
        request.Approve(EmployeeRef.Create(5).Value);

        return request;
    }

    private static ConnectionRequest CreateRejectedRequest()
    {
        var request = CreateInReviewRequest();
        request.Reject(EmployeeRef.Create(5).Value, RejectionFeedback.Create("Need more data.").Value);

        return request;
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
}
