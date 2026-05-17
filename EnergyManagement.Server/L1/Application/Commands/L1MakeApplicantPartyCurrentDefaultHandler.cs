using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1MakeApplicantPartyCurrentDefaultHandler
    : IRequestHandler<L1MakeApplicantPartyCurrentDefaultCommand, UnitResult<IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly L1DbContext _context;

    public L1MakeApplicantPartyCurrentDefaultHandler(
        IApplicantPartyRepository applicantParties,
        L1DbContext context)
    {
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> Handle(
        L1MakeApplicantPartyCurrentDefaultCommand command,
        CancellationToken cancellationToken)
    {
        var selectedApplicantParty = await _applicantParties.GetOwnedByIdAsync(
            command.ApplicantPartyId,
            command.ClientAccountId,
            cancellationToken);

        if (selectedApplicantParty is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ApplicantPartyIsRequired]);
        }

        var accountApplicantParties = await _applicantParties.ListOwnedByAccountIdAsync(
            command.ClientAccountId,
            cancellationToken);

        foreach (var applicantParty in accountApplicantParties
            .Where(x => x.ApplicantPartyType == selectedApplicantParty.ApplicantPartyType
                && x.Id != selectedApplicantParty.Id
                && x.IsCurrentActiveVersion))
        {
            var markInactiveResult = applicantParty.MarkInactiveVersion();
            if (markInactiveResult.IsFailure)
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(markInactiveResult.Error);
            }
        }

        if (!selectedApplicantParty.IsCurrentActiveVersion)
        {
            var markCurrentResult = selectedApplicantParty.MarkAsCurrentDefaultTemplate();
            if (markCurrentResult.IsFailure)
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(markCurrentResult.Error);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
