using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class EmployeeRejectRequestReviewDtoValidator
    : AbstractValidator<EmployeeRejectRequestReviewDto>
{
    public EmployeeRejectRequestReviewDtoValidator()
    {
        RuleFor(dto => dto.Feedback)
            .Custom((feedback, context) =>
            {
                if (!string.IsNullOrWhiteSpace(feedback)
                    && feedback.Length > RejectionFeedback.MaxLength)
                {
                    context.AddFailure(
                        FieldNames.EmployeeRejectRequestReview.Feedback,
                        Error.Errors.L1Domain.RejectionFeedbackIsTooLong.Code);
                }
            });
    }
}
