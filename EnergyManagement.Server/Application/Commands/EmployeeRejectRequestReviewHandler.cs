using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class EmployeeRejectRequestReviewHandler
    : IRequestHandler<EmployeeRejectRequestReviewCommand, EmployeeRejectRequestReviewCommandResult>
{
    private readonly IEmployeeRepository _employees;
    private readonly IClientRequestRepository _clientRequests;
    private readonly L1DbContext _context;

    public EmployeeRejectRequestReviewHandler(
        IEmployeeRepository employees,
        IClientRequestRepository clientRequests,
        L1DbContext context)
    {
        _employees = employees;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<EmployeeRejectRequestReviewCommandResult> Handle(
        EmployeeRejectRequestReviewCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employee is null)
        {
            return EmployeeRejectRequestReviewCommandResult.Forbidden();
        }

        var request = await _clientRequests.GetByIdAsync(command.RequestId, cancellationToken);
        if (request is not Domain.EnergyManagement.ConnectionRequest connectionRequest)
        {
            return EmployeeRejectRequestReviewCommandResult.NotFound();
        }

        Domain.EnergyManagement.RejectionFeedback? feedback = null;
        if (!string.IsNullOrWhiteSpace(command.Feedback))
        {
            var feedbackResult = Domain.EnergyManagement.RejectionFeedback.Create(command.Feedback);
            if (feedbackResult.IsFailure)
            {
                return EmployeeRejectRequestReviewCommandResult.Invalid(feedbackResult.Error);
            }

            feedback = feedbackResult.Value;
        }

        var rejectReview = connectionRequest.RejectReview(
            employee,
            feedback,
            DateTimeOffset.UtcNow);
        if (rejectReview.IsFailure)
        {
            return EmployeeRejectRequestReviewCommandResult.Invalid(rejectReview.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return EmployeeRejectRequestReviewCommandResult.Rejected();
    }
}
