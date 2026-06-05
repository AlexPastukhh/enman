using EnergyManagement.Server.Api.Contracts.ApplicantParties;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation.ApplicantParties;

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
