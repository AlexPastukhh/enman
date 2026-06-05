using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class CreateIndividualEntrepreneurApplicantPartyHandler
    : IRequestHandler<CreateIndividualEntrepreneurApplicantPartyCommand, Result<CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyCreationService _applicantPartyCreation;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly EnergyManagementDbContext _context;

    public CreateIndividualEntrepreneurApplicantPartyHandler(
        IApplicantPartyCreationService applicantPartyCreation,
        IApplicantPartyRepository applicantParties,
        EnergyManagementDbContext context)
    {
        _applicantPartyCreation = applicantPartyCreation;
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<Result<CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>> Handle(
        CreateIndividualEntrepreneurApplicantPartyCommand command,
        CancellationToken cancellationToken)
    {
        var applicantPartyResult = await _applicantPartyCreation.CreateIndividualEntrepreneurAsync(
            command.ClientAccountId,
            command.FirstName,
            command.MiddleName,
            command.LastName,
            command.Inn,
            command.Ogrnip,
            command.Email,
            command.PhoneNumber,
            cancellationToken);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        _applicantParties.Add(applicantPartyResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(
            new CreateIndividualApplicantPartyResponse(
                applicantPartyResult.Value.Id,
                applicantPartyResult.Value.ClientAccountId));
    }
}
