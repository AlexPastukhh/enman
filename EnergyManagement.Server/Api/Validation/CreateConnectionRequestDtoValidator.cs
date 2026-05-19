using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class CreateConnectionRequestDtoValidator
    : AbstractValidator<CreateConnectionRequestDto>
{
    public const string ExistingApplicantContext = "Existing";
    public const string NewApplicantContext = "New";

    public CreateConnectionRequestDtoValidator()
    {
        RuleFor(dto => dto)
            .Custom((dto, context) =>
            {
                ValidateApplicantContext(dto, context);
                ValidateDetails(dto.Details, context);
                ValidateAddress(dto.Address, context);

                if (string.Equals(dto.ApplicantContextType, NewApplicantContext, StringComparison.Ordinal))
                {
                    ApplicantPartyValidation.ValidateIndividualApplicant(
                        dto.NewApplicantParty,
                        context,
                        FieldNames.CreateConnectionRequest.NewApplicantParty);
                }
            });
    }

    private static void ValidateApplicantContext(
        CreateConnectionRequestDto dto,
        ValidationContext<CreateConnectionRequestDto> context)
    {
        if (string.IsNullOrWhiteSpace(dto.ApplicantContextType))
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.ApplicantContextType,
                Error.Errors.General.ValueIsRequired.Code);
            return;
        }

        if (!string.Equals(dto.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal)
            && !string.Equals(dto.ApplicantContextType, NewApplicantContext, StringComparison.Ordinal))
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.ApplicantContextType,
                Error.Errors.General.ValueIsInvalid.Code);
            return;
        }

        var hasExistingApplicantPartyId = dto.ExistingApplicantPartyId.HasValue;
        var hasNewApplicantParty = dto.NewApplicantParty is not null;

        if (hasExistingApplicantPartyId && hasNewApplicantParty)
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.ApplicantContextType,
                Error.Errors.General.ValueIsInvalid.Code);
            return;
        }

        if (!hasExistingApplicantPartyId && !hasNewApplicantParty)
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.ApplicantContextType,
                Error.Errors.General.ValueIsRequired.Code);
            return;
        }

        if (string.Equals(dto.ApplicantContextType, ExistingApplicantContext, StringComparison.Ordinal))
        {
            if (!hasExistingApplicantPartyId)
            {
                context.AddFailure(
                    FieldNames.CreateConnectionRequest.ExistingApplicantPartyId,
                    Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            }

            if (hasNewApplicantParty)
            {
                context.AddFailure(
                    FieldNames.CreateConnectionRequest.NewApplicantParty,
                    Error.Errors.General.ValueIsInvalid.Code);
            }
        }

        if (string.Equals(dto.ApplicantContextType, NewApplicantContext, StringComparison.Ordinal))
        {
            if (!hasNewApplicantParty)
            {
                context.AddFailure(
                    FieldNames.CreateConnectionRequest.NewApplicantParty,
                    Error.Errors.L1Domain.ApplicantPartyIsRequired.Code);
            }

            if (hasExistingApplicantPartyId)
            {
                context.AddFailure(
                    FieldNames.CreateConnectionRequest.ExistingApplicantPartyId,
                    Error.Errors.General.ValueIsInvalid.Code);
            }
        }
    }

    private static void ValidateDetails(
        string? details,
        ValidationContext<CreateConnectionRequestDto> context)
    {
        if (string.IsNullOrWhiteSpace(details))
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.Details,
                Error.Errors.ClientRequestErrors.ClientRequestTextIsRequired.Code);
        }

        if (details is not null && details.Length > 3000)
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.Details,
                Error.Errors.ClientRequestErrors.ClientRequestTextIsTooLong.Code);
        }
    }

    private static void ValidateAddress(
        AddressDto? address,
        ValidationContext<CreateConnectionRequestDto> context)
    {
        if (address is null)
        {
            context.AddFailure(
                FieldNames.CreateConnectionRequest.Address,
                Error.Errors.AddressErrors.AddressIsRequired.Code);
            return;
        }

        var addressResult = Address.Create(
            address.PostalCode ?? string.Empty,
            address.Region ?? string.Empty,
            address.City ?? string.Empty,
            address.Street ?? string.Empty,
            address.House ?? string.Empty,
            address.Building,
            address.Apartment);

        if (addressResult.IsFailure)
        {
            foreach (var error in addressResult.Error)
            {
                context.AddFailure(MapAddressField(error), error.Code);
            }
        }
    }

    private static string MapAddressField(Error error)
    {
        var address = FieldNames.CreateConnectionRequest.Address;

        if (error == Error.Errors.AddressErrors.PostalCodeIsRequired
            || error == Error.Errors.AddressErrors.PostalCodeIsInvalid)
        {
            return $"{address}.{FieldNames.Address.PostalCode}";
        }

        if (error == Error.Errors.AddressErrors.RegionIsRequired
            || error == Error.Errors.AddressErrors.RegionIsTooLong)
        {
            return $"{address}.{FieldNames.Address.Region}";
        }

        if (error == Error.Errors.AddressErrors.CityIsRequired
            || error == Error.Errors.AddressErrors.CityIsTooLong)
        {
            return $"{address}.{FieldNames.Address.City}";
        }

        if (error == Error.Errors.AddressErrors.StreetIsRequired
            || error == Error.Errors.AddressErrors.StreetIsTooLong)
        {
            return $"{address}.{FieldNames.Address.Street}";
        }

        if (error == Error.Errors.AddressErrors.HouseIsRequired
            || error == Error.Errors.AddressErrors.HouseIsTooLong)
        {
            return $"{address}.{FieldNames.Address.House}";
        }

        if (error == Error.Errors.AddressErrors.BuildingIsTooLong)
        {
            return $"{address}.{FieldNames.Address.Building}";
        }

        if (error == Error.Errors.AddressErrors.ApartmentIsTooLong)
        {
            return $"{address}.{FieldNames.Address.Apartment}";
        }

        return address;
    }
}
