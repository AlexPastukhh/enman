using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Domain.Users
{
    public class ChangePassword :ValueObject
    {
        public string Secret { get; }
        public DateTimeOffset SecretExpirationTime { get; }
        public bool IsSubmitted { get; private set; } = false;
        public bool IsAttemptedToSubmit { get; private set; } = false;
        public Maybe<DateTimeOffset> SubmitExpirationTime { get; private set; } = Maybe.None;

        #region Empty Constructor
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private ChangePassword() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        #endregion

        private ChangePassword(string secret,
            DateTimeOffset secretExpiresAt)
        {
            Secret = secret;
            SecretExpirationTime = secretExpiresAt;
        }

        internal static ChangePassword StartPasswordChange(DateTimeOffset utcNow)
        {
            var secret = Convert.ToHexString(
                RandomNumberGenerator.GetBytes(128));

            var expirationDate = utcNow.AddMinutes(5);

            return new ChangePassword(secret, expirationDate);
        }

        internal UnitResult<Error> SubmitChange(string secret,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(secret);
            Guard.IsNotNull(utcNow);

            if (IsSubmitted)
            {
                throw new InvalidOperationException(
                    "Password change has been submitted");
            }

            if (IsAttemptedToSubmit)
            {
                throw new InvalidOperationException(
                    "Password change has been attempted to submit");
            }


            if (secret != Secret)
            {
                IsAttemptedToSubmit = true;
                return Errors.Account.InValidChangePasswordSecret;
            }

            if (utcNow > SecretExpirationTime)
            {
                IsAttemptedToSubmit = true;
                return Errors.Account.ChangePasswordSecretHasExpired;
            }

            IsAttemptedToSubmit= true;
            IsSubmitted = true;
            SubmitExpirationTime= Maybe.From(utcNow.AddMinutes(15)); // May check how to implement something based on time 

            return UnitResult.Success<Error>();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Secret;
            yield return IsSubmitted;
            yield return IsAttemptedToSubmit;
            yield return SecretExpirationTime;
            yield return SubmitExpirationTime;
        }
    }
}
