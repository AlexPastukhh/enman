using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1CreateConnectionRequestHandler
    : IRequestHandler<L1CreateConnectionRequestCommand, UnitResult<IReadOnlyList<Error>>>
{
    private const string ExistingApplicantContext = "Existing";
    private const string NewApplicantContext = "New";

    private readonly IApplicantPartyCreationService _applicantPartyCreation;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly IClientRequestRepository _clientRequests;
    private readonly L1DbContext _context;

    public L1CreateConnectionRequestHandler(
        IApplicantPartyCreationService applicantPartyCreation,
        IApplicantPartyRepository applicantParties,
        IClientRequestRepository clientRequests,
        L1DbContext context)
    {
        _applicantPartyCreation = applicantPartyCreation;
        _applicantParties = applicantParties;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> Handle(
        L1CreateConnectionRequestCommand command,
        CancellationToken cancellationToken)
    {
        var branchErrors = ValidateApplicantContext(command);
        if (branchErrors.Count > 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(branchErrors);
        }

        var addressResult = Address.Create(
            command.PostalCode,
            command.Region,
            command.City,
            command.Street,
            command.House,
            command.Building,
            command.Apartment);

        if (addressResult.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                addressResult.Error);
        }

        var requestInputErrors = ValidateRequestInput(command.Details);
        if (requestInputErrors.Count > 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(requestInputErrors);
        }

        if (string.Equals(command.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal))
        {
            return await HandleExistingApplicantAsync(command, addressResult.Value, cancellationToken);
        }

        return await HandleNewApplicantAsync(command, addressResult.Value, cancellationToken);
    }

    private async Task<UnitResult<IReadOnlyList<Error>>> HandleExistingApplicantAsync(
        L1CreateConnectionRequestCommand command,
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
        L1CreateConnectionRequestCommand command,
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

    private static IReadOnlyList<Error> ValidateApplicantContext(
        L1CreateConnectionRequestCommand command)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(command.ApplicantContextType))
        {
            errors.Add(Errors.General.ValueIsRequired);
            return errors;
        }

        if (!string.Equals(command.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal)
            && !string.Equals(command.ApplicantContextType, NewApplicantContext, StringComparison.Ordinal))
        {
            errors.Add(Errors.General.ValueIsInvalid);
            return errors;
        }

        var hasExistingApplicantPartyId = command.ExistingApplicantPartyId.HasValue;
        var hasNewApplicantParty = command.NewApplicantParty is not null;

        if (hasExistingApplicantPartyId && hasNewApplicantParty)
        {
            errors.Add(Errors.General.ValueIsInvalid);
            return errors;
        }

        if (!hasExistingApplicantPartyId && !hasNewApplicantParty)
        {
            errors.Add(Errors.General.ValueIsRequired);
            return errors;
        }

        if (string.Equals(command.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal))
        {
            if (!hasExistingApplicantPartyId)
            {
                errors.Add(Errors.L1Domain.ApplicantPartyIsRequired);
            }

            if (hasNewApplicantParty)
            {
                errors.Add(Errors.General.ValueIsInvalid);
            }
        }

        if (string.Equals(command.ApplicantContextType, NewApplicantContext, StringComparison.Ordinal))
        {
            if (!hasNewApplicantParty)
            {
                errors.Add(Errors.L1Domain.ApplicantPartyIsRequired);
            }

            if (hasExistingApplicantPartyId)
            {
                errors.Add(Errors.General.ValueIsInvalid);
            }
        }

        return errors;
    }

    private static IReadOnlyList<Error> ValidateRequestInput(string details)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(details))
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
        }

        if (details is not null && details.Length > 3000)
        {
            errors.Add(Errors.ClientRequestErrors.ClientRequestTextIsTooLong);
        }

        return errors;
    }
}
