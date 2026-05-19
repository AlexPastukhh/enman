using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Api;
using FluentValidation;
using FluentValidation.Results;

namespace EnergyManagement.Server.Api.Validation;

public sealed class AgreementExchangeListQueryDtoValidator
    : AbstractValidator<AgreementExchangeListQueryDto>
{
    public AgreementExchangeListQueryDtoValidator()
    {
        RuleFor(x => x.Status)
            .Custom((status, context) =>
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return;
                }

                if (!Enum.TryParse<AgreementExchangeStatus>(status, ignoreCase: false, out var parsed)
                    || !Enum.IsDefined(parsed))
                {
                    context.AddFailure(new ValidationFailure(
                        FieldNames.AgreementExchangeList.Status,
                        Error.Errors.General.ValueIsInvalid.Code));
                }
            });
    }
}
