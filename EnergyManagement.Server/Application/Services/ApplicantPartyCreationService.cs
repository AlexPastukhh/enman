using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Services;

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
        var fullName = ValidatedInput.ValueOrThrow(
            FullName.Create(firstName, middleName, lastName),
            "FullName was validated by FluentValidation but FullName.Create failed.");

        var applicantEmail = ValidatedInput.ValueOrThrow(
            Email.Create(email),
            "Applicant email was validated by FluentValidation but Email.Create failed.");

        var applicantPhoneNumber = ValidatedInput.ValueOrThrow(
            PhoneNumber.Create(phoneNumber),
            "PhoneNumber was validated by FluentValidation but PhoneNumber.Create failed.");

        var account = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(
                [Errors.ClientErrors.ClientNotFound]);
        }

        var applicantPartyResult = IndividualApplicantParty.Create(
            clientAccount,
            fullName,
            applicantEmail,
            applicantPhoneNumber);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<IndividualApplicantParty, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        var hasSameTypeApplicantParty = await _applicantParties.ExistsByClientAccountIdAndTypeAsync(
            clientAccount.Id,
            ApplicantPartyType.Individual,
            cancellationToken);
        if (!hasSameTypeApplicantParty)
        {
            applicantPartyResult.Value.MarkAsCurrentDefaultTemplate();
        }

        return applicantPartyResult;
    }
}
