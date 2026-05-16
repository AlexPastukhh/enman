using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.L1.Api.Validation;

public sealed class L1RegisterClientAccountDtoValidator
    : AbstractValidator<L1RegisterClientAccountDto>
{
    public L1RegisterClientAccountDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .Custom((email, context) =>
            {
                var result = Email.Create(email ?? string.Empty);
                if (result.IsFailure)
                {
                    foreach (var error in result.Error)
                    {
                        context.AddFailure(L1FieldNames.Auth.Email, error.Code);
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
                        context.AddFailure(L1FieldNames.Auth.Password, error.Code);
                    }
                }
            });
    }
}
