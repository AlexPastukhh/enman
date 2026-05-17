using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Api;
using FluentValidation;
using FluentValidation.Results;

namespace EnergyManagement.Server.L1.Api.Validation;

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
                        L1FieldNames.AgreementExchangeList.Status,
                        Error.Errors.General.ValueIsInvalid.Code));
                }
            });
    }
}
