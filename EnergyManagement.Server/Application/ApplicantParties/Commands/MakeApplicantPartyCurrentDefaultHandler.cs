using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.ApplicantParties.Commands;

public sealed class MakeApplicantPartyCurrentDefaultHandler
    : IRequestHandler<MakeApplicantPartyCurrentDefaultCommand, UnitResult<IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly EnergyManagementDbContext _context;

    public MakeApplicantPartyCurrentDefaultHandler(
        IApplicantPartyRepository applicantParties,
        EnergyManagementDbContext context)
    {
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> Handle(
        MakeApplicantPartyCurrentDefaultCommand command,
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
