using FluentValidation;

namespace EnergyManagement.Server.L1.Api.Validation;

public sealed class L1CreateIndividualApplicantPartyDtoValidator
    : AbstractValidator<L1CreateIndividualApplicantPartyDto>
{
    public L1CreateIndividualApplicantPartyDtoValidator()
    {
        RuleFor(dto => dto)
            .Custom((dto, context) =>
            {
                L1ApplicantPartyValidation.ValidateIndividualApplicant(
                    dto,
                    context,
                    prefix: string.Empty);
            });
    }
}
