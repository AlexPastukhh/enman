using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class ConnectionRequest : ClientRequest
{
    private ReviewDecisionRecord? _reviewDecision;

    public ReviewDecisionRecord? ReviewDecision => _reviewDecision;

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
