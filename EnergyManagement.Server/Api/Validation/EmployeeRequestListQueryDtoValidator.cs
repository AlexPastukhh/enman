using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Queries;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class EmployeeRequestListQueryDtoValidator
    : AbstractValidator<EmployeeRequestListQueryDto>
{
    public EmployeeRequestListQueryDtoValidator()
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
                        FieldNames.EmployeeRequestList.Status,
                        Error.Errors.General.ValueIsInvalid.Code);
                }
            });

        RuleFor(query => query.ReviewState)
            .Custom((reviewState, context) =>
            {
                if (string.IsNullOrWhiteSpace(reviewState))
                {
                    return;
                }

                if (!Enum.TryParse<EmployeeRequestReviewState>(reviewState, ignoreCase: false, out var parsed)
                    || !Enum.IsDefined(parsed))
                {
                    context.AddFailure(
                        FieldNames.EmployeeRequestList.ReviewState,
                        Error.Errors.General.ValueIsInvalid.Code);
                }
            });
    }
}
