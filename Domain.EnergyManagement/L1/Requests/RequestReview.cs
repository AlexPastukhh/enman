using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class RequestReview
{
    public long RequestId { get; private set; }

    public RequestReviewStatus Status { get; private set; }

    public long StartedByEmployeeId { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public long? CompletedByEmployeeId { get; private set; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public RejectionFeedback? RejectionFeedback { get; private set; }

    private RequestReview()
    {
    }

    internal static RequestReview StartForRequest(
        long requestId,
        Employee employee,
        DateTimeOffset startedAt)
    {
        if (requestId <= 0)
        {
            throw new InvalidOperationException("Request must be persisted before review can start.");
        }

        if (employee is null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        if (employee.Id <= 0)
        {
            throw new ArgumentException(
                "Employee must be persisted before review can start.",
                nameof(employee));
        }

        return new RequestReview
        {
            RequestId = requestId,
            Status = RequestReviewStatus.Started,
            StartedByEmployeeId = employee.Id,
            StartedAt = startedAt
        };
    }

    internal UnitResult<IReadOnlyList<Error>> Approve(
        Employee employee,
        DateTimeOffset decidedAt)
    {
        var canComplete = CanComplete(employee);
        if (canComplete.IsFailure)
        {
            return canComplete;
        }

        Status = RequestReviewStatus.Approved;
        CompletedByEmployeeId = employee.Id;
        CompletedAt = decidedAt;
        RejectionFeedback = null;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    internal UnitResult<IReadOnlyList<Error>> Reject(
        Employee employee,
        RejectionFeedback? feedback,
        DateTimeOffset decidedAt)
    {
        var canComplete = CanComplete(employee);
        if (canComplete.IsFailure)
        {
            return canComplete;
        }

        Status = RequestReviewStatus.Rejected;
        CompletedByEmployeeId = employee.Id;
        CompletedAt = decidedAt;
        RejectionFeedback = feedback;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private UnitResult<IReadOnlyList<Error>> CanComplete(Employee employee)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
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

        if (Status != RequestReviewStatus.Started)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyStartedReviewCanBeCompleted]);
        }

        if (StartedByEmployeeId != employee.Id)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.RequestReviewStartedByAnotherEmployee]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
