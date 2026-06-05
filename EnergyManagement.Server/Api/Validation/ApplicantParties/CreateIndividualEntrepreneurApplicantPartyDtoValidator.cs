using EnergyManagement.Server.Api.Contracts.ApplicantParties;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation.ApplicantParties;

public sealed class CreateIndividualEntrepreneurApplicantPartyDtoValidator
    : AbstractValidator<CreateIndividualEntrepreneurApplicantPartyDto>
{
    public CreateIndividualEntrepreneurApplicantPartyDtoValidator()
    {
        RuleFor(dto => dto)
            .Custom((dto, context) =>
            {
                ApplicantPartyValidation.ValidateIndividualEntrepreneurApplicant(
                    dto,
                    context,
                    prefix: string.Empty);
            });
    }
}
