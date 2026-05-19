using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class RegisterClientAccountDtoValidator
    : AbstractValidator<RegisterClientAccountDto>
{
    public RegisterClientAccountDtoValidator()
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
                var result = PasswordHash.ValidatePlainTextPassword(password);
                if (result.IsFailure)
                {
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(FieldNames.Auth.Password, error.Code);
                    }
                }
            });
    }
}
