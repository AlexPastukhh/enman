using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class CreateLegalEntityApplicantPartyDtoValidator
    : AbstractValidator<CreateLegalEntityApplicantPartyDto>
{
    public CreateLegalEntityApplicantPartyDtoValidator()
    {
        RuleFor(dto => dto)
            .Custom((dto, context) =>
            {
                ApplicantPartyValidation.ValidateLegalEntityApplicant(
                    dto,
                    context,
                    prefix: string.Empty);
            });
    }
}
