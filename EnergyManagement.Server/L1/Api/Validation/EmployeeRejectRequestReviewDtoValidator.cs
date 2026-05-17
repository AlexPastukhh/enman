using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using FluentValidation;

namespace EnergyManagement.Server.L1.Api.Validation;

public sealed class EmployeeRejectRequestReviewDtoValidator
    : AbstractValidator<EmployeeRejectRequestReviewDto>
{
    public EmployeeRejectRequestReviewDtoValidator()
    {
        RuleFor(dto => dto.Feedback)
            .Custom((feedback, context) =>
            {
                if (string.IsNullOrWhiteSpace(feedback))
                {
                    context.AddFailure(
                        L1FieldNames.EmployeeRejectRequestReview.Feedback,
                        Error.Errors.General.ValueIsRequired.Code);
                    return;
                }

                if (feedback.Length > RejectionFeedback.MaxLength)
                {
                    context.AddFailure(
                        L1FieldNames.EmployeeRejectRequestReview.Feedback,
                        Error.Errors.L1Domain.RejectionFeedbackIsTooLong.Code);
                }
            });
    }
}
