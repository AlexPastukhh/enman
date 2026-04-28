
using System.Text.RegularExpressions;
using Hospital.proj.Domain.Common;
using CSharpFunctionalExtensions;

namespace Hospital.proj.Domain.Users
{
    public class Email:ValueObject
    {
        public string Value { get;  }
        private Email(string value)
        {
            Value = value;
        }
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        private Email() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public static Result<Email,Error> Create(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return Errors.General.ValueIsRequired;
            }

            if (Regex.IsMatch(email, @"^(.+)@(.+)$")== false)
            {
                return Errors.General.ValueIsInvalid;
            }

            if(email.Length > 100)
            {
                return Errors.General.StringIsTooLarge;
            }

            return new Email(email);
        }

        public static implicit operator string(Email email) => email.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}