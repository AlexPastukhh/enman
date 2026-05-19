using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class CreateIndividualApplicantPartyDtoValidator
    : AbstractValidator<CreateIndividualApplicantPartyDto>
{
    public CreateIndividualApplicantPartyDtoValidator()
    {
        RuleFor(dto => dto)
            .Custom((dto, context) =>
            {
                ApplicantPartyValidation.ValidateIndividualApplicant(
                    dto,
                    context,
                    prefix: string.Empty);
            });
    }
}
