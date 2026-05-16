using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using FluentValidation;

namespace EnergyManagement.Server.L1.Api.Validation;

public sealed class L1ListMyRequestsQueryDtoValidator
    : AbstractValidator<L1ListMyRequestsQueryDto>
{
    public L1ListMyRequestsQueryDtoValidator()
    {
        RuleFor(query => query.Status)
            .Custom((status, context) =>
            {
                if (string.IsNullOrWhiteSpace(status))
                {
                    return;
                }

                if (!Enum.TryParse<RequestStatus>(status, ignoreCase: false, out var parsed)
                    || !Enum.IsDefined(parsed))
                {
                    context.AddFailure(
                        L1FieldNames.ListMyRequests.Status,
                        Error.Errors.General.ValueIsInvalid.Code);
                }
            });
    }
}
