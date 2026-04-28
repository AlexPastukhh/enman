using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Contracts;

namespace Hospital.proj.Server.Application.Validation
{
    public static class CustomRules
    {
        //public static IRuleBuilder<RegisterDto, RegisterDto> CanBeUser(
        //   this IRuleBuilder<RegisterDto, RegisterDto> builder,
        //   DateTimeOffset now)
        //{
        //    var nextBuilder = (IRuleBuilder<T, string>)builder.Custom((register, context) =>
        //    {
        //        var result = User.CreateAccountOrThrow(
        //            register.Name.FirstName,
        //            register.Name.MiddleName,
        //            register.Name.LastName,
        //            register.Email,
        //            register.Password,
        //            now);
        //        if (result.IsFailure)
        //        {
        //            context.AddFailure(new ValidationFailure(
        //                context.PropertyPath,
        //                result.Error.Code));
        //        }
        //    });
        //    return nextBuilder;
        //}

        public static IRuleBuilder<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> builder,
            Func<string, Result<TValueObject, Error>> factory)
        {
            var nextBuilder = (IRuleBuilder<T, string>)builder.Custom((value, context) =>
            {
                var result = factory(value);
                if (result.IsFailure)
                {
                    context.AddFailure(new ValidationFailure(
                        context.PropertyPath,
                        result.Error.Code));
                }
            });
            return nextBuilder;
        }

        public static IRuleBuilder<T, NameDto> MustBeFullName<T>(
           this IRuleBuilder<T, NameDto> builder)
        {
            var nextBuilder = (IRuleBuilder<T, NameDto>)builder.Custom((value, context) =>
            {
                var result = FullName.Create(value.FirstName, value.MiddleName, value.LastName);
                if (result.IsFailure)
                {
                    foreach (var failure in result.Error)
                    {
                        context.AddFailure(new ValidationFailure(
                            context.PropertyPath,
                            failure.Code)
                        );
                    }
                }
            });
            return nextBuilder;
        }

        public static IRuleBuilder<T, T> RequestMustBeNotNull<T>(
           this IRuleBuilder<T, T> builder)
        {
            var nextBuilder = (IRuleBuilder<T, T>)builder.Custom((request, context) =>
            {
                if (request is null)
                {
                    context.AddFailure(Errors.General.RequestBodyIsNull.Code);
                }
            });
            return nextBuilder;
        }

    }
}
