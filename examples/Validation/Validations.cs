using FluentValidation;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Contracts;

namespace Hospital.proj.Server.Application.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterDto>
    {
        public RegisterRequestValidator(TimeProvider time)
        {
            RuleFor(r => r)
                .RequestMustBeNotNull();

            RuleFor(r => r.Email)
                .MustBeValueObject(Email.Create);

            RuleFor(r => r.Name)
                .MustBeFullName();

            RuleFor(r => r.Password)
                .MustBeValueObject(Password.HashPassword);

            RuleFor(r => r.PasswordConfirm)
                .Equal(r => r.Password)
                .WithMessage(r =>
                    Errors.Account.PasswordConfirmationFailed.Code);

        }

        public class LoginRequestValidator : AbstractValidator<LoginDto>
        {
            public LoginRequestValidator()
            {
                RuleFor(l => l)
                    .RequestMustBeNotNull();

                RuleFor(l => l.Email)
                    .MustBeValueObject(Email.Create);

                RuleFor(l => l.Password)
                    .NotEmpty();
            }
        }


        public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordDto>
        {
            public ChangePasswordRequestValidator()
            {
                RuleFor(r => r.Password)
                .MustBeValueObject(Password.HashPassword);

                RuleFor(r => r.PasswordConfirm)
                    .Equal(r => r.Password)
                    .WithMessage(r =>
                        Errors.Account.PasswordConfirmationFailed.Code);

            }
        }
    }
}
