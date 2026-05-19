using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Commands;

public sealed class CreateConnectionRequestHandler
    : IRequestHandler<CreateConnectionRequestCommand, UnitResult<IReadOnlyList<Error>>>
{
    private const string ExistingApplicantContext = "Existing";
    private const string NewApplicantContext = "New";

    private readonly IApplicantPartyCreationService _applicantPartyCreation;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly IClientRequestRepository _clientRequests;
    private readonly EnergyManagementDbContext _context;

    public CreateConnectionRequestHandler(
        IApplicantPartyCreationService applicantPartyCreation,
        IApplicantPartyRepository applicantParties,
        IClientRequestRepository clientRequests,
        EnergyManagementDbContext context)
    {
        _applicantPartyCreation = applicantPartyCreation;
        _applicantParties = applicantParties;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> Handle(
        CreateConnectionRequestCommand command,
        CancellationToken cancellationToken)
    {
        var objectAddress = ValidatedInput.ValueOrThrow(
            Address.Create(
                command.PostalCode,
                command.Region,
                command.City,
                command.Street,
                command.House,
                command.Building,
                command.Apartment),
            "Address was validated by FluentValidation but Address.Create failed.");

        if (string.Equals(command.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal))
        {
            return await HandleExistingApplicantAsync(command, objectAddress, cancellationToken);
        }

        return await HandleNewApplicantAsync(command, objectAddress, cancellationToken);
    }

    private async Task<UnitResult<IReadOnlyList<Error>>> HandleExistingApplicantAsync(
        CreateConnectionRequestCommand command,
        Address objectAddress,
        CancellationToken cancellationToken)
    {
        var applicantParty = await _applicantParties.GetOwnedByIdAsync(
            command.ExistingApplicantPartyId!.Value,
            command.ClientAccountId,
            cancellationToken);

        if (applicantParty is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ApplicantPartyIsRequired]);
        }

        var requestResult = ConnectionRequest.Create(
            applicantParty,
            command.Details,
            objectAddress);

        if (requestResult.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                requestResult.Error);
        }

        _clientRequests.Add(requestResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private async Task<UnitResult<IReadOnlyList<Error>>> HandleNewApplicantAsync(
        CreateConnectionRequestCommand command,
        Address objectAddress,
        CancellationToken cancellationToken)
    {
        var newApplicant = command.NewApplicantParty!;
        var createdApplicant = await _applicantPartyCreation.CreateIndividualAsync(
            command.ClientAccountId,
            newApplicant.FirstName,
            newApplicant.MiddleName,
            newApplicant.LastName,
            newApplicant.Email,
            newApplicant.PhoneNumber,
            cancellationToken);

        if (createdApplicant.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(createdApplicant.Error);
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        _applicantParties.Add(createdApplicant.Value);
        await _context.SaveChangesAsync(cancellationToken);

        var requestResult = ConnectionRequest.Create(
            createdApplicant.Value,
            command.Details,
            objectAddress);

        if (requestResult.IsFailure)
        {
            await transaction.RollbackAsync(cancellationToken);
            return UnitResult.Failure<IReadOnlyList<Error>>(requestResult.Error);
        }

        _clientRequests.Add(requestResult.Value);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
