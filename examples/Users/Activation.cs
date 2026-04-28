using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using System.Security.Cryptography;

namespace Hospital.proj.Domain.Users
{
    public class Activation
    {
        public string SecurityCode { get; private set; }
        public DateTimeOffset CodeExpirationTime { get; private set; }
        public bool IsActivated { get; private set; } = false;
        public Maybe<DateTimeOffset> ActivatedAt { get; private set; }=Maybe.None;
        public bool IsActivationAttempted { get; private set; } = false;
        public User User { get; internal set; } = default!;

        #region Empty Constructor
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private Activation() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        #endregion

        private Activation(
            string securityCode,
            DateTimeOffset securityCodeExpiration )
        {
            SecurityCode = securityCode;
            CodeExpirationTime = securityCodeExpiration;
            
        }

        internal UnitResult<Error> ActivateAccount(
            string inputCode,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(inputCode);
            Guard.IsNotNull(utcNow);

            if (inputCode != SecurityCode)
            {
                IsActivationAttempted = true;
                return Errors.Account.InValidActivationCode;
            }

            if(utcNow > CodeExpirationTime)
            {
                IsActivationAttempted = true;
                return Errors.Account.ActivationCodeHasExpired;
            }

            IsActivationAttempted = true;
            IsActivated = true;
            ActivatedAt = utcNow;
            return UnitResult.Success<Error>();
        }

        internal static Activation StartAccountActivation(DateTimeOffset utcNow)
        {
            var code = Convert.ToHexString(RandomNumberGenerator.GetBytes(128));

            var expirationDate = utcNow.AddHours(1);

            var aaInfo =new Activation(code!, expirationDate);

            return aaInfo;
        }

    }
}