using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Domain.Users
{
    public class User : Entity
    {
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Password Password{ get; private set; } //Maybe need to create Password wiht PH and PC with enc logic of both
        public Activation AccountActivation { get; private set; }

        
        #region emptyConstructor
#pragma warning disable CS8618
        protected User() { }
#pragma warning restore CS8618
        #endregion
        private User(
            FullName fullName,
            Email email,
            Password password,
            Activation accountActivation)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            AccountActivation = accountActivation;
        }


        public static User CreateAccountOrThrow(
            string firstName,
            string middleName,
            string lastName,
            string email,
            string password,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(utcNow);

            var errors = new List<Error>();

            var fullName = FullName.Create(firstName, middleName, lastName).Value;

            var userEmail = Email.Create(email).Value;


            var passwordHash = PasswordHash.HashPassword(password).Value;
            var passwordProp = new Password(passwordHash);

            var activation = Activation.StartAccountActivation(utcNow);

            var user = new User(
                fullName,
                userEmail,
                passwordProp,
                activation);
            activation.User = user;


            return user;
        }

        public static User CreateAccount(
            FullName fullName,
            Email email,
            PasswordHash passwordHash,
            DateTimeOffset utcNow)
        {
            Guard.IsNotNull(fullName);
            Guard.IsNotNull(email);
            Guard.IsNotNull(passwordHash);
            Guard.IsNotNull(utcNow);

            var activation = Activation.StartAccountActivation(utcNow);

            var passwordProp = new Password(passwordHash);

            var user = new User(
                 fullName,
                 email,
                 passwordProp,
                 activation);
            activation.User = user;


            return user;
        }



        public UnitResult<Error> ActivateAccount(
            string inputCode,
            DateTimeOffset utcNow)
        {
            Guard.IsFalse(AccountActivation.IsActivated);
            Guard.IsFalse(AccountActivation.IsActivationAttempted);

            var result = AccountActivation.ActivateAccount(inputCode, utcNow);

            return result;
        }

        

        //internal void AddSecret(UserSecret secretToAdd)
        //{
        //    _userSecrets.Add(secretToAdd);
        //}

    }
}
