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

    public async Task<Result<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>> CreateIndividualEntrepreneurAsync(
        long clientAccountId,
        string firstName,
        string middleName,
        string lastName,
        string inn,
        string ogrnip,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        var fullName = ValidatedInput.ValueOrThrow(
            FullName.Create(firstName, middleName, lastName),
            "FullName was validated by FluentValidation but FullName.Create failed.");

        var applicantInn = ValidatedInput.ValueOrThrow(
            Inn.Create(inn),
            "INN was validated by FluentValidation but Inn.Create failed.");

        var applicantOgrnip = ValidatedInput.ValueOrThrow(
            Ogrnip.Create(ogrnip),
            "OGRNIP was validated by FluentValidation but Ogrnip.Create failed.");

        var applicantEmail = ValidatedInput.ValueOrThrow(
            Email.Create(email),
            "Applicant email was validated by FluentValidation but Email.Create failed.");

        var applicantPhoneNumber = ValidatedInput.ValueOrThrow(
            PhoneNumber.Create(phoneNumber),
            "PhoneNumber was validated by FluentValidation but PhoneNumber.Create failed.");

        var account = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return Result.Failure<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>(
                [Errors.ClientErrors.ClientNotFound]);
        }

        var applicantPartyResult = IndividualEntrepreneurApplicantParty.Create(
            clientAccount.Id,
            fullName,
            applicantInn,
            applicantOgrnip,
            applicantEmail,
            applicantPhoneNumber,
            DateTimeOffset.UtcNow);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<IndividualEntrepreneurApplicantParty, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        var hasSameTypeApplicantParty = await _applicantParties.ExistsByClientAccountIdAndTypeAsync(
            clientAccount.Id,
            ApplicantPartyType.IndividualEntrepreneur,
            cancellationToken);
        if (!hasSameTypeApplicantParty)
        {
            applicantPartyResult.Value.MarkAsCurrentDefaultTemplate();
        }

        return applicantPartyResult;
    }

    public async Task<Result<LegalEntityApplicantParty, IReadOnlyList<Error>>> CreateLegalEntityAsync(
        long clientAccountId,
        string organizationName,
        string inn,
        string kpp,
        string ogrn,
        string email,
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        var applicantOrganizationName = ValidatedInput.ValueOrThrow(
            OrganizationName.Create(organizationName),
            "OrganizationName was validated by FluentValidation but OrganizationName.Create failed.");

        var applicantInn = ValidatedInput.ValueOrThrow(
            Inn.Create(inn),
            "INN was validated by FluentValidation but Inn.Create failed.");

        var applicantKpp = ValidatedInput.ValueOrThrow(
            Kpp.Create(kpp),
            "KPP was validated by FluentValidation but Kpp.Create failed.");

        var applicantOgrn = ValidatedInput.ValueOrThrow(
            Ogrn.Create(ogrn),
            "OGRN was validated by FluentValidation but Ogrn.Create failed.");

        var applicantEmail = ValidatedInput.ValueOrThrow(
            Email.Create(email),
            "Applicant email was validated by FluentValidation but Email.Create failed.");

        var applicantPhoneNumber = ValidatedInput.ValueOrThrow(
            PhoneNumber.Create(phoneNumber),
            "PhoneNumber was validated by FluentValidation but PhoneNumber.Create failed.");

        var account = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return Result.Failure<LegalEntityApplicantParty, IReadOnlyList<Error>>(
                [Errors.ClientErrors.ClientNotFound]);
        }

        var applicantPartyResult = LegalEntityApplicantParty.Create(
            clientAccount.Id,
            applicantOrganizationName,
            applicantInn,
            applicantKpp,
            applicantOgrn,
            applicantEmail,
            applicantPhoneNumber,
            DateTimeOffset.UtcNow);

        if (applicantPartyResult.IsFailure)
        {
            return Result.Failure<LegalEntityApplicantParty, IReadOnlyList<Error>>(
                applicantPartyResult.Error);
        }

        var hasSameTypeApplicantParty = await _applicantParties.ExistsByClientAccountIdAndTypeAsync(
            clientAccount.Id,
            ApplicantPartyType.LegalEntity,
            cancellationToken);
        if (!hasSameTypeApplicantParty)
        {
            applicantPartyResult.Value.MarkAsCurrentDefaultTemplate();
        }

        return applicantPartyResult;
    }
}
