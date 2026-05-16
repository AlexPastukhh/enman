using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.L1.Application.Security;
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
                var result = Email.Create(email);
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
                var result = L1PasswordHasher.HashPassword(password);
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
