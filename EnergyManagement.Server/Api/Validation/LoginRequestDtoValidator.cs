using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .Custom((email, context) =>
            {
                var result = Email.Create(email ?? string.Empty);
                if (result.IsFailure)
                {
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(FieldNames.Auth.Email, error.Code);
                    }
                }
            });

        RuleFor(dto => dto.Password)
            .Custom((password, context) =>
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    context.AddFailure(
                        FieldNames.Auth.Password,
                        Error.Errors.Account.PasswordIsRequired.Code);
                }
            });
    }
}
