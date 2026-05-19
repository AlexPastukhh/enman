using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class RunApplicantPartyVerificationFromRequestHandler
    : IRequestHandler<RunApplicantPartyVerificationFromRequestCommand, RunApplicantPartyVerificationFromRequestCommandResult>
{
    private readonly IEmployeeRepository _employees;
    private readonly IClientRequestRepository _clientRequests;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly IApplicantPartyMockVerificationService _mockVerificationService;
    private readonly L1DbContext _context;

    public RunApplicantPartyVerificationFromRequestHandler(
        IEmployeeRepository employees,
        IClientRequestRepository clientRequests,
        IApplicantPartyRepository applicantParties,
        IApplicantPartyMockVerificationService mockVerificationService,
        L1DbContext context)
    {
        _employees = employees;
        _clientRequests = clientRequests;
        _applicantParties = applicantParties;
        _mockVerificationService = mockVerificationService;
        _context = context;
    }

    public async Task<RunApplicantPartyVerificationFromRequestCommandResult> Handle(
        RunApplicantPartyVerificationFromRequestCommand command,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (employee is null)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.Forbidden();
        }

        var canReview = employee.EnsureCanReview();
        if (canReview.IsFailure)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.Forbidden();
        }

        var request = await _clientRequests.GetByIdAsync(command.RequestId, cancellationToken);
        if (request is not Domain.EnergyManagement.ConnectionRequest connectionRequest)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.NotFound();
        }

        // Current temporary Employee visibility policy: every active Employee can review every request.
        // Future assignment/queue slices can replace this with stricter visibility rules.
        var applicantParty = await _applicantParties.GetByIdAsync(
            connectionRequest.ApplicantPartyId,
            cancellationToken);
        if (applicantParty is null)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.Invalid(
                [Error.Errors.L1Domain.ApplicantPartyIsRequired]);
        }

        var mockResult = await _mockVerificationService.VerifyAsync(applicantParty, cancellationToken);
        if (!mockResult.IsPassed)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.Invalid(
                [Error.Errors.L1Domain.ApplicantPartyIsIncomplete]);
        }

        var markVerified = applicantParty.MarkVerified();
        if (markVerified.IsFailure)
        {
            return RunApplicantPartyVerificationFromRequestCommandResult.Invalid(markVerified.Error);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return RunApplicantPartyVerificationFromRequestCommandResult.Verified(
            new RunApplicantPartyVerificationResponse(
                connectionRequest.Id,
                applicantParty.Id,
                applicantParty.VerificationStatus.ToString(),
                mockResult.Result,
                mockResult.Message));
    }
}
