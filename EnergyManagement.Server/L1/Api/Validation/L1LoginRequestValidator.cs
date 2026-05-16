using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentValidation;

namespace EnergyManagement.Server.L1.Api.Validation;

public sealed class L1LoginRequestValidator : AbstractValidator<L1LoginRequest>
{
    public L1LoginRequestValidator()
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
                if (string.IsNullOrWhiteSpace(password))
                {
                    context.AddFailure(
                        L1FieldNames.Auth.Password,
                        Error.Errors.Account.PasswordIsRequired.Code);
                }
            });
    }
}
