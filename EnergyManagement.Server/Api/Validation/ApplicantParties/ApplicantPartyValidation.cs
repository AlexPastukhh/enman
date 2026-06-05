using Domain.EnergyManagement;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Api.Contracts.ApplicantParties;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation.ApplicantParties;

internal static class ApplicantPartyValidation
{
    public static void ValidateIndividualApplicant<T>(
        CreateIndividualApplicantPartyDto? dto,
        ValidationContext<T> context,
        string prefix)
    {
        if (dto is null)
        {
            context.AddFailure(
                Join(prefix, string.Empty),
                Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            return;
        }

        ValidateFullName(dto.FullName, context, Join(prefix, FieldNames.ApplicantParty.FullName));
        ValidateContact(dto.Email, dto.PhoneNumber, context, prefix);
    }

    public static void ValidateIndividualEntrepreneurApplicant<T>(
        CreateIndividualEntrepreneurApplicantPartyDto? dto,
        ValidationContext<T> context,
        string prefix)
    {
        if (dto is null)
        {
            context.AddFailure(
                Join(prefix, string.Empty),
                Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            return;
        }

        ValidateFullName(dto.FullName, context, Join(prefix, FieldNames.ApplicantParty.FullName));
        ValidateInn(dto.Inn, context, Join(prefix, FieldNames.ApplicantParty.Inn));
        ValidateOgrnip(dto.Ogrnip, context, Join(prefix, FieldNames.ApplicantParty.Ogrnip));
        ValidateContact(dto.Email, dto.PhoneNumber, context, prefix);
    }

    public static void ValidateLegalEntityApplicant<T>(
        CreateLegalEntityApplicantPartyDto? dto,
        ValidationContext<T> context,
        string prefix)
    {
        if (dto is null)
        {
            context.AddFailure(
                Join(prefix, string.Empty),
                Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            return;
        }

        ValidateOrganizationName(dto.OrganizationName, context, Join(prefix, FieldNames.ApplicantParty.OrganizationName));
        ValidateInn(dto.Inn, context, Join(prefix, FieldNames.ApplicantParty.Inn));
        ValidateKpp(dto.Kpp, context, Join(prefix, FieldNames.ApplicantParty.Kpp));
        ValidateOgrn(dto.Ogrn, context, Join(prefix, FieldNames.ApplicantParty.Ogrn));
        ValidateContact(dto.Email, dto.PhoneNumber, context, prefix);
    }

    public static void ValidateConnectionRequestNewApplicant<T>(
        InlineApplicantPartyForRequestDto? dto,
        ValidationContext<T> context,
        string prefix)
    {
        if (dto is null)
        {
            context.AddFailure(
                Join(prefix, string.Empty),
                Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            return;
        }

        if (!Enum.TryParse<ApplicantPartyType>(
                dto.ApplicantPartyType,
                ignoreCase: false,
                out var applicantPartyType)
            || !Enum.IsDefined(applicantPartyType))
        {
            context.AddFailure(
                Join(prefix, FieldNames.ApplicantParty.ApplicantPartyType),
                Error.Errors.General.ValueIsInvalid.Code);
            return;
        }

        switch (applicantPartyType)
        {
            case ApplicantPartyType.Individual:
                ValidateFullName(dto.FullName, context, Join(prefix, FieldNames.ApplicantParty.FullName));
                break;

            case ApplicantPartyType.IndividualEntrepreneur:
                ValidateFullName(dto.FullName, context, Join(prefix, FieldNames.ApplicantParty.FullName));
                ValidateInn(dto.Inn, context, Join(prefix, FieldNames.ApplicantParty.Inn));
                ValidateOgrnip(dto.Ogrnip, context, Join(prefix, FieldNames.ApplicantParty.Ogrnip));
                break;

            case ApplicantPartyType.LegalEntity:
                ValidateOrganizationName(dto.OrganizationName, context, Join(prefix, FieldNames.ApplicantParty.OrganizationName));
                ValidateInn(dto.Inn, context, Join(prefix, FieldNames.ApplicantParty.Inn));
                ValidateKpp(dto.Kpp, context, Join(prefix, FieldNames.ApplicantParty.Kpp));
                ValidateOgrn(dto.Ogrn, context, Join(prefix, FieldNames.ApplicantParty.Ogrn));
                break;
        }

        ValidateContact(dto.Email, dto.PhoneNumber, context, prefix);
    }

    private static void ValidateContact<T>(
        string? email,
        string? phoneNumber,
        ValidationContext<T> context,
        string prefix)
    {
        var emailResult = Email.Create(email ?? string.Empty);
        if (emailResult.IsFailure)
        {
            AddFailures(context, Join(prefix, FieldNames.ApplicantParty.Email), emailResult.Error);
        }

        var phoneResult = PhoneNumber.Create(phoneNumber ?? string.Empty);
        if (phoneResult.IsFailure)
        {
            AddFailures(context, Join(prefix, FieldNames.ApplicantParty.PhoneNumber), phoneResult.Error);
        }
    }

    private static void ValidateFullName<T>(
        FullNameDto? fullName,
        ValidationContext<T> context,
        string fieldName)
    {
        if (fullName is null)
        {
            context.AddFailure(fieldName, Error.Errors.General.ValueIsRequired.Code);
            return;
        }

        var fullNameResult = FullName.Create(
            fullName.FirstName ?? string.Empty,
            fullName.MiddleName ?? string.Empty,
            fullName.LastName ?? string.Empty);

        if (fullNameResult.IsFailure)
        {
            foreach (var error in fullNameResult.Error)
            {
                context.AddFailure(MapFullNameField(error, fieldName), error.Code);
            }
        }
    }

    private static void AddFailures<T>(
        ValidationContext<T> context,
        string fieldName,
        IReadOnlyList<Error> errors)
    {
        foreach (var error in errors)
        {
            context.AddFailure(fieldName, error.Code);
        }
    }

    private static void ValidateOrganizationName<T>(
        string? organizationName,
        ValidationContext<T> context,
        string fieldName)
    {
        var result = OrganizationName.Create(organizationName ?? string.Empty);
        if (result.IsFailure)
        {
            AddFailures(context, fieldName, result.Error);
        }
    }

    private static void ValidateInn<T>(
        string? inn,
        ValidationContext<T> context,
        string fieldName)
    {
        var result = Inn.Create(inn ?? string.Empty);
        if (result.IsFailure)
        {
            AddFailures(context, fieldName, result.Error);
        }
    }

    private static void ValidateKpp<T>(
        string? kpp,
        ValidationContext<T> context,
        string fieldName)
    {
        var result = Kpp.Create(kpp ?? string.Empty);
        if (result.IsFailure)
        {
            AddFailures(context, fieldName, result.Error);
        }
    }

    private static void ValidateOgrn<T>(
        string? ogrn,
        ValidationContext<T> context,
        string fieldName)
    {
        var result = Ogrn.Create(ogrn ?? string.Empty);
        if (result.IsFailure)
        {
            AddFailures(context, fieldName, result.Error);
        }
    }

    private static void ValidateOgrnip<T>(
        string? ogrnip,
        ValidationContext<T> context,
        string fieldName)
    {
        var result = Ogrnip.Create(ogrnip ?? string.Empty);
        if (result.IsFailure)
        {
            AddFailures(context, fieldName, result.Error);
        }
    }

    private static string MapFullNameField(Error error, string fullNameField)
    {
        if (error == Error.Errors.Account.FirstNameIsRequired
            || error == Error.Errors.Account.FirstNameIsTooLarge)
        {
            return Join(fullNameField, FieldNames.FullName.FirstName);
        }

        if (error == Error.Errors.Account.MiddleNameIsRequired
            || error == Error.Errors.Account.MiddleNameIsTooLarge)
        {
            return Join(fullNameField, FieldNames.FullName.MiddleName);
        }

        if (error == Error.Errors.Account.LastNameIsRequired
            || error == Error.Errors.Account.LastNameIsTooLarge)
        {
            return Join(fullNameField, FieldNames.FullName.LastName);
        }

        return fullNameField;
    }

    private static string Join(string prefix, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(prefix))
        {
            return fieldName;
        }

        if (string.IsNullOrWhiteSpace(fieldName))
        {
            return prefix;
        }

        return $"{prefix}.{fieldName}";
    }
}
