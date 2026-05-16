using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1CreateIndividualApplicantPartyHandler
    : IRequestHandler<L1CreateIndividualApplicantPartyCommand, Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyCreationService _applicantPartyCreation;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly L1DbContext _context;

    public L1CreateIndividualApplicantPartyHandler(
        IApplicantPartyCreationService applicantPartyCreation,
        IApplicantPartyRepository applicantParties,
        L1DbContext context)
    {
        _applicantPartyCreation = applicantPartyCreation;
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>> Handle(
        L1CreateIndividualApplicantPartyCommand command,
        CancellationToken cancellationToken)
    {
        var applicantPartyResult = await _applicantPartyCreation.CreateIndividualAsync(
            command.ClientAccountId,
            command.FirstName,
            command.MiddleName,
            command.LastName,
            command.Email,
            command.PhoneNumber,
            cancellationToken);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        _applicantParties.Add(applicantPartyResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(
            new L1CreateIndividualApplicantPartyResponse(
                applicantPartyResult.Value.Id,
                applicantPartyResult.Value.ClientAccountId));
    }
}
