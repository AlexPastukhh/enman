using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class ConnectionRequest : ClientRequest
{
    public RequestReview? Review { get; private set; }

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

        if (Id <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestIsRequired]);
        }

        if (employee.Id <= 0)
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

        if (Review is not null && Review.Status == RequestReviewStatus.Started)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewAlreadyStarted]);
        }

        Review = RequestReview.StartForRequest(
            Id,
            employee,
            startedAt);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ApproveReview(
        Employee employee,
        DateTimeOffset decidedAt)
    {
        if (Review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeApproved]);
        }

        var approve = Review.Approve(employee, decidedAt);
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
        if (Review is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewMustBeStarted]);
        }

        if (Status != RequestStatus.InReview)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyInReviewRequestCanBeRejected]);
        }

        var reject = Review.Reject(
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

}
