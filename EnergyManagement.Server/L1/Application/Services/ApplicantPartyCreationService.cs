using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Services;

public sealed class ApplicantPartyCreationService : IApplicantPartyCreationService
{
    private readonly IAccountRepository _accounts;
    private readonly IApplicantPartyRepository _applicantParties;

    public ApplicantPartyCreationService(
        IAccountRepository accounts,
        IApplicantPartyRepository applicantParties)
    {
        _accounts = accounts;
        _applicantParties = applicantParties;
    }

    public async Task<Result<IndividualApplicantParty, IReadOnlyList<Error>>> CreateIndividualAsync(
        long clientAccountId,
        string firstName,
        string middleName,
        string lastName,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        var errors = new List<Error>();

        var fullNameResult = FullName.Create(firstName, middleName, lastName);
        if (fullNameResult.IsFailure)
        {
            errors.AddRange(fullNameResult.Error);
        }

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            errors.AddRange(emailResult.Error);
        }

        var phoneResult = PhoneNumber.Create(phoneNumber);
        if (phoneResult.IsFailure)
        {
            errors.AddRange(phoneResult.Error);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(errors);
        }

        var account = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(
                [Errors.ClientErrors.ClientNotFound]);
        }

        var applicantPartyResult = IndividualApplicantParty.Create(
            clientAccount,
            fullNameResult.Value,
            emailResult.Value,
            phoneResult.Value);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        _applicantParties.Add(applicantPartyResult.Value);

        return applicantPartyResult;
    }
}
