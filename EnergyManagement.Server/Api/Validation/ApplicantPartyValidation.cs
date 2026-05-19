using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

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

        var emailResult = Email.Create(dto.Email ?? string.Empty);
        if (emailResult.IsFailure)
        {
            AddFailures(context, Join(prefix, FieldNames.ApplicantParty.Email), emailResult.Error);
        }

        var phoneResult = PhoneNumber.Create(dto.PhoneNumber ?? string.Empty);
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
