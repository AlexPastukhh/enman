using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class ConnectionRequest : ClientRequest
{
    private ReviewDecisionRecord? _reviewDecision;

    private RequestReview? _review;

    public ReviewDecisionRecord? ReviewDecision => _reviewDecision;

    [NotMapped]
    public RequestReview? Review => _review;

    private ConnectionRequest(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress,
        DateTimeOffset createdAt)
        : base(
            applicantParty,
            ClientRequestType.Connection,
            details,
            objectAddress,
            createdAt)
    {
    }

    private ConnectionRequest()
    {
    }

    public static Result<ConnectionRequest, IReadOnlyList<Error>> Create(
        ApplicantParty applicantParty,
        string details,
        Address objectAddress)
    {
        var errors = ValidateCreateInput(applicantParty, details, objectAddress);
        if (errors.Count > 0)
        {
            return Result.Failure<ConnectionRequest, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ConnectionRequest, IReadOnlyList<Error>>(
            new ConnectionRequest(
                applicantParty,
                details,
                objectAddress,
                DateTimeOffset.UtcNow));
    }

    public UnitResult<IReadOnlyList<Error>> StartReview(
        Employee employee,
        DateTimeOffset startedAt)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canReview = employee.EnsureCanReview();
        if (canReview.IsFailure)
        {
            return canReview;
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanStartReview]);
        }

        if (_review is not null && _review.Status == RequestReviewStatus.Started)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewAlreadyStarted]);
        }

        _review = RequestReview.StartForRequest(
            Id,
            employee,
            startedAt);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ApproveReview(
        Employee employee,
        DateTimeOffset decidedAt)
    {
        if (_review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeApproved]);
        }

        var approve = _review.Approve(employee, decidedAt);
        if (approve.IsFailure)
        {
            return approve;
        }

        Status = RequestStatus.Approved;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> RejectReview(
        Employee employee,
        RejectionFeedback? feedback,
        DateTimeOffset decidedAt)
    {
        if (_review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeRejected]);
        }

        var reject = _review.Reject(
            employee,
            feedback,
            decidedAt);

        if (reject.IsFailure)
        {
            return reject;
        }

        Status = RequestStatus.Rejected;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> MarkAgreementExchangeFailed(
        long agreementProposalExchangeId,
        DateTimeOffset failedAt)
    {
        if (agreementProposalExchangeId <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        if (Status != RequestStatus.Approved)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyApprovedRequestCanBeMarkedAgreementExchangeFailed]);
        }

        Status = RequestStatus.AgreementExchangeFailed;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    // Existing L1 review API is intentionally kept for current implemented behavior compatibility.
    public UnitResult<IReadOnlyList<Error>> CanApprove(EmployeeRef reviewer)
    {
        if (reviewer is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ReviewerIsRequired]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeApproved]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> Approve(EmployeeRef reviewer)
    {
        var canApprove = CanApprove(reviewer);
        if (canApprove.IsFailure)
        {
            return canApprove;
        }

        Status = RequestStatus.Approved;
        _reviewDecision = ReviewDecisionRecord.Approved(
            reviewer,
            DateTimeOffset.UtcNow);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> CanReject(EmployeeRef reviewer)
    {
        if (reviewer is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ReviewerIsRequired]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeRejected]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> Reject(
        EmployeeRef reviewer,
        RejectionFeedback? feedback)
    {
        var canReject = CanReject(reviewer);
        if (canReject.IsFailure)
        {
            return canReject;
        }

        Status = RequestStatus.Rejected;
        _reviewDecision = ReviewDecisionRecord.Rejected(
            reviewer,
            feedback,
            DateTimeOffset.UtcNow);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
