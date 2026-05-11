using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1CreateIndividualApplicantPartyHandler
    : IRequestHandler<L1CreateIndividualApplicantPartyCommand, Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;
    private readonly IApplicantPartyRepository _applicantParties;
    private readonly L1DbContext _context;

    public L1CreateIndividualApplicantPartyHandler(
        IAccountRepository accounts,
        IApplicantPartyRepository applicantParties,
        L1DbContext context)
    {
        _accounts = accounts;
        _applicantParties = applicantParties;
        _context = context;
    }

    public async Task<Result<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>> Handle(
        L1CreateIndividualApplicantPartyCommand command,
        CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        var fullNameResult = FullName.Create(command.FirstName, command.MiddleName, command.LastName);
        if (fullNameResult.IsFailure)
        {
            errors.AddRange(fullNameResult.Error);
        }

        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            errors.AddRange(emailResult.Error);
        }

        var phoneResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneResult.IsFailure)
        {
            errors.AddRange(phoneResult.Error);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(errors);
        }

        var account = await _accounts.GetByIdAsync(command.ClientAccountId, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return Result.Failure<L1CreateIndividualApplicantPartyResponse, IReadOnlyList<Error>>(
                [Errors.ClientErrors.ClientNotFound]);
        }

        var applicantPartyResult = IndividualApplicantParty.Create(
            clientAccount,
            fullNameResult.Value,
            emailResult.Value,
            phoneResult.Value);

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
