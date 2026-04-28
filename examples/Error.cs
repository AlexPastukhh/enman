using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital.proj.Domain.Common
{
    public sealed class Error : ValueObject
    {
        public string Code { get; }
        public int StatusCode { get; }  // Error.csChanged from HttpStatusCode to int

        internal Error(
            string code,
            int status)  // Changed parameter from HttpStatusCode to int
        {
            Contract.Requires(
                code is not null);

            Code = code!;
            StatusCode = status;  // Assigning the integer status code
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
    public static class Errors
    {
        public static class Account
        {
            public static readonly Error InValidActivationCode =
                new Error("account.activation.code.is.invalid", 400);  // 400 BadRequest

            public static readonly Error ActivationCodeHasExpired =
                new Error("account.activation.code.has.expired", 410);  // 410 Gone

            public static readonly Error InValidChangePasswordSecret =
                new Error("account.change.password.secret.is.invalid", 400);  // 400 BadRequest

            public static readonly Error ChangePasswordSecretHasExpired =
                new Error("account.change.password.secret.has.expired", 410);  // 410 Gone

            public static readonly Error ChangePasswordSubmitHasExpired =
                new Error("account.change.password.submit.has.expired", 410);  // 410 Gone

            public static readonly Error PasswordConfirmationFailed =
                new Error("account.password.confirmation.failed", 400);  // 400 BadRequest

            public static readonly Error FirstNameIsTooLong =
                new Error("account.firstName.is.too.large", 400);  // 400 BadRequest

            public static readonly Error MiddleNameIsTooLong =
                new Error("account.middleName.is.too.large", 400);  // 400 BadRequest

            public static readonly Error LastNameIsTooLong =
                new Error("account.lastName.is.too.large", 400);  // 400 BadRequest

            public static readonly Error FirstNameIsRequired =
                new Error("account.firstName.is.required", 400);  // 400 BadRequest

            public static readonly Error MiddleNameIsRequired =
                new Error("account.middleName.is.required", 400);  // 400 BadRequest

            public static readonly Error LastNameIsRequired =
                new Error("account.lastName.is.required", 400);  // 400 BadRequest

            public static readonly Error EmailIsAlreadyRegistered =
                new Error("account.email.is.already.registered", 409);  // 409 Conflict

            public static readonly Error PasswordsDontMatch =
                new Error("account.passwords.don't.match", 400);  // 400 BadRequest
        }

        public static class UserErrors
        {
            public static readonly Error NotFoundForRequestedId =
                new Error("user.not.found", 404);  // 404 NotFound

            public static readonly Error NotFoundForRequestedEmail =
                new Error("user.not.found", 404);  // 404 NotFound

            public static readonly Error NotFoundForRequestedCode =
                new Error("user.not.found", 404);  // 404 NotFound
        }

        public static class General
        {
            public static readonly Error NotFound =
                new Error("record.not.found", 404);  // 404 NotFound

            public static readonly Error ValueIsInvalid =
                new Error("value.is.invalid", 400);  // 400 BadRequest

            public static readonly Error ValueIsRequired =
                new Error("value.is.required", 400);  // 400 BadRequest

            public static readonly Error InvalidLength =
                new Error("invalid.string.length", 400);  // 400 BadRequest

            public static readonly Error CollectionIsTooSmall =
                new Error("collection.is.too.small", 400);  // 400 BadRequest

            public static readonly Error CollectionIsTooLarge =
                new Error("collection.is.too.large", 400);  // 400 BadRequest

            public static readonly Error StringIsTooSmall =
                new Error("string.is.too.small", 400);  // 400 BadRequest

            public static readonly Error StringIsTooLarge =
                new Error("string.is.too.large", 400);  // 400 BadRequest

            public static readonly Error InternalServerError =
                new Error("internal.server.error", 500);  // 500 InternalServerError

            public static readonly Error RequestBodyIsNull =
                new Error("request.body.is.null", 400);  // 400 BadRequest

            public static readonly Error RouteValueIsNull =
                new Error("request.body.is.null", 400);  // 400 BadRequest
        }

        public static class Infrastructure
        {
            public static readonly Error EmailWasntSent =
                new Error("infrastructure.email.wasn't.sent", 500);  // 500 InternalServerError

            public static readonly Error EmailAddressDoesntExistOrUnavailabe =
                new Error("infrastructure.email.doesn't.exist.or.unavailable", 404);  // 404 NotFound

            public static readonly Error FailedToAddRecord =
                new Error("infrastructure.failed.to.add.record", 500);  // 500 InternalServerError

            public static readonly Error FailedToSaveChanges =
                new Error("infrastructure.failed.to.save.changes", 500);  // 500 InternalServerError
        }
    }


}


