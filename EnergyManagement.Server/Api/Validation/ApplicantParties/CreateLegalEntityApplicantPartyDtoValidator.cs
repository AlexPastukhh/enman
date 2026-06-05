using EnergyManagement.Server.Api.Contracts.ApplicantParties;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation.ApplicantParties;

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
