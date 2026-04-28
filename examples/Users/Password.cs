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
    public class Password:Entity
    {
        public PasswordHash Hash { get; private set; }
        private  ChangePassword? _currentPasswordChange=default!;
        public Maybe<ChangePassword> CurrentPasswordChange  => 
            Maybe.From(_currentPasswordChange);
        
        internal Password(PasswordHash hash)
        {
            Hash = hash;
        }

        public static Result<PasswordHash, Error> HashPassword(string password)
        {
            var hashPassword =PasswordHash.HashPassword(password);
            if (hashPassword.IsFailure)
            {
                return hashPassword.Error;
            }

            return hashPassword.Value;
        }

        public static UnitResult<Error> VerifyPassword(PasswordHash usersPasswordHash, 
            string password)
        {
            Guard.IsNotNull(usersPasswordHash);
            Guard.IsNotNull(password);

            var result =PasswordHash.VerifyPassword(usersPasswordHash, password);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return UnitResult.Success<Error>();
        }

        public void StartPasswordChange(DateTimeOffset utcNow)
        {
            Guard.IsNotNull(utcNow);

            var passwordChange = ChangePassword.StartPasswordChange(utcNow);
            _currentPasswordChange = passwordChange;
        }

        public UnitResult<Error> SubmitPasswordChange(string secret,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(secret);
            Guard.IsNotNull(utcNow);

            if (CurrentPasswordChange.HasNoValue)
            {
                throw new InvalidOperationException("There is no started procedures of password changing to submit");
            }

            var submit = CurrentPasswordChange.Value.SubmitChange(secret, utcNow);
            if (submit.IsFailure)
            {
                return submit.Error;
            }

            return UnitResult.Success<Error>();
        }



        public UnitResult<Error> ChangePasswordOrThrow(PasswordHash hashedPwd,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(hashedPwd);
            Guard.IsNotNull(utcNow);

            if (CurrentPasswordChange.HasNoValue)
            {
                throw new InvalidOperationException("Cant change password," +
                    "   no started procedure for change");
            }

            if (!CurrentPasswordChange.Value.IsSubmitted)
            {
                throw new InvalidOperationException("Need to submit password change");
            }

            if (CurrentPasswordChange.Value.SubmitExpirationTime.Value < utcNow)
            {
                return Errors.Account.ChangePasswordSubmitHasExpired;
            }

            Hash = hashedPwd;
            _currentPasswordChange = default!;

            return UnitResult.Success<Error>();
        }

    }   


}
