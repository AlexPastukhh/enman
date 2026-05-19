using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class ListMyRequestsQueryDtoValidator
    : AbstractValidator<ListMyRequestsQueryDto>
{
    public ListMyRequestsQueryDtoValidator()
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
                        FieldNames.ListMyRequests.Status,
                        Error.Errors.General.ValueIsInvalid.Code);
                }
            });
    }
}
