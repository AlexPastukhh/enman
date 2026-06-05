using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;

namespace EnergyManagement.Server.Application.Commands;

public sealed class CreateLegalEntityApplicantPartyHandler
    : IRequestHandler<CreateLegalEntityApplicantPartyCommand, Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>>
{
    private readonly IApplicantPartyCreationService _applicantPartyCreation;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly EnergyManagementDbContext _context;

    public CreateLegalEntityApplicantPartyHandler(
        IApplicantPartyCreationService applicantPartyCreation,
        IApplicantPartyRepository applicantParties,
        EnergyManagementDbContext context)
    {
        _applicantPartyCreation = applicantPartyCreation;
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<Result<CreateApplicantPartyResponse, IReadOnlyList<Error>>> Handle(
        CreateLegalEntityApplicantPartyCommand command,
        CancellationToken cancellationToken)
    {
        var applicantPartyResult = await _applicantPartyCreation.CreateLegalEntityAsync(
            command.ClientAccountId,
            command.OrganizationName,
            command.Inn,
            command.Kpp,
            command.Ogrn,
            command.Email,
            command.PhoneNumber,
            cancellationToken);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<CreateApplicantPartyResponse, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        _applicantParties.Add(applicantPartyResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<CreateApplicantPartyResponse, IReadOnlyList<Error>>(
            new CreateApplicantPartyResponse(
                applicantPartyResult.Value.Id,
                applicantPartyResult.Value.ClientAccountId));
    }
}
