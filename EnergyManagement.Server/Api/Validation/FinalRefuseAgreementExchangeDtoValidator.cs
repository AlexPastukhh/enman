using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class FinalRefuseAgreementExchangeDtoValidator
    : AbstractValidator<FinalRefuseAgreementExchangeDto>
{
    public FinalRefuseAgreementExchangeDtoValidator()
    {
        RuleFor(dto => dto.Reason)
            .Custom((reason, context) =>
            {
                if (reason is null)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(reason))
                {
                    context.AddFailure(
                        FieldNames.FinalRefuseAgreementExchange.Reason,
                        Error.Errors.L1Domain.FinalRefusalReasonIsRequired.Code);
                    return;
                }

                if (reason.Length > FinalRefusalReason.MaxLength)
                {
                    context.AddFailure(
                        FieldNames.FinalRefuseAgreementExchange.Reason,
                        Error.Errors.L1Domain.FinalRefusalReasonIsTooLong.Code);
                }
            });
    }
}
