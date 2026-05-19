using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class EmployeeApproveRequestReviewHandler
    : IRequestHandler<EmployeeApproveRequestReviewCommand, EmployeeApproveRequestReviewCommandResult>
{
    private readonly IEmployeeRepository _employees;
    private readonly IClientRequestRepository _clientRequests;
    private readonly L1DbContext _context;

    public EmployeeApproveRequestReviewHandler(
        IEmployeeRepository employees,
        IClientRequestRepository clientRequests,
        L1DbContext context)
    {
        _employees = employees;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<EmployeeApproveRequestReviewCommandResult> Handle(
        EmployeeApproveRequestReviewCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employee is null)
        {
            return EmployeeApproveRequestReviewCommandResult.Forbidden();
        }

        var request = await _clientRequests.GetByIdAsync(command.RequestId, cancellationToken);
        if (request is not Domain.EnergyManagement.ConnectionRequest connectionRequest)
        {
            return EmployeeApproveRequestReviewCommandResult.NotFound();
        }

        // Current temporary Employee visibility policy: every active Employee can review every L1 request.
        // Future assignment/queue slices can replace this with stricter visibility rules.
        var approveReview = connectionRequest.ApproveReview(employee, DateTimeOffset.UtcNow);
        if (approveReview.IsFailure)
        {
            return EmployeeApproveRequestReviewCommandResult.Invalid(approveReview.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return EmployeeApproveRequestReviewCommandResult.Approved();
    }
}
