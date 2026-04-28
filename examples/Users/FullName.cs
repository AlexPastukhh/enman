using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using static Hospital.proj.Domain.Common.Errors;

namespace Hospital.proj.Domain.Users
{
    public class FullName : ValueObject
    {
        public string FirstName {  get; }
        public string MiddleName { get; }
        public string LastName{ get; }
        private FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public static Result<FullName,IReadOnlyList<Error>>Create(
            string firstName,
            string middleName,
            string lastName)
        {
            var errors = new List<Error>();

            if (string.IsNullOrEmpty(firstName))
            {
                errors.Add(Errors.Account.FirstNameIsRequired);
            }
            else if(firstName.Length > 50)
            {
                errors.Add(Errors.Account.FirstNameIsTooLong);
            }

            
            if (string.IsNullOrEmpty(middleName))
            {
                errors.Add(Errors.Account.MiddleNameIsRequired);
            }
            else if (middleName.Length > 50)
            {
                errors.Add(Errors.Account.MiddleNameIsTooLong);
            }

                   
            if (string.IsNullOrEmpty(lastName))
            {
                errors.Add(Errors.Account.LastNameIsRequired);
            }
            else if(lastName.Length > 50)
            {
                errors.Add(Errors.Account.LastNameIsTooLong);
            }

            if (errors.Any())
            {
                return errors;
            }

            var fullName = new FullName(firstName, middleName, lastName);
            return fullName;

    }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }

        public static implicit operator string(FullName fullName)
            => string.Join(" ",fullName.FirstName, fullName.MiddleName, fullName.LastName);
    }
}