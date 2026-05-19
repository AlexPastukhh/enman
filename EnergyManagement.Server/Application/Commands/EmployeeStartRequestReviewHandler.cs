using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class EmployeeStartRequestReviewHandler
    : IRequestHandler<EmployeeStartRequestReviewCommand, EmployeeStartRequestReviewCommandResult>
{
    private readonly IEmployeeRepository _employees;
    private readonly IClientRequestRepository _clientRequests;
    private readonly EnergyManagementDbContext _context;

    public EmployeeStartRequestReviewHandler(
        IEmployeeRepository employees,
        IClientRequestRepository clientRequests,
        EnergyManagementDbContext context)
    {
        _employees = employees;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<EmployeeStartRequestReviewCommandResult> Handle(
        EmployeeStartRequestReviewCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employee is null)
        {
            return EmployeeStartRequestReviewCommandResult.Forbidden();
        }

        var request = await _clientRequests.GetByIdAsync(command.RequestId, cancellationToken);
        if (request is not Domain.EnergyManagement.ConnectionRequest connectionRequest)
        {
            return EmployeeStartRequestReviewCommandResult.NotFound();
        }

        // Current temporary Employee visibility policy: every active Employee can review every request.
        // Future assignment/queue slices can replace this with stricter visibility rules.
        var startReview = connectionRequest.StartReview(employee, DateTimeOffset.UtcNow);
        if (startReview.IsFailure)
        {
            return EmployeeStartRequestReviewCommandResult.Invalid(startReview.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return EmployeeStartRequestReviewCommandResult.Started();
    }
}
