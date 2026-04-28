using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using System.Security.Cryptography;

namespace Hospital.proj.Domain.Users
{
    public class PasswordHash:ValueObject
    {
        public string Value { get; }

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName AlgorithmName =
           HashAlgorithmName.SHA512;

        private PasswordHash(string value)
        {
            Value = value;
        }


        internal static Result<PasswordHash, Error> HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Errors.General.ValueIsRequired;
            }

            if (password.Length < 12)
            {
                return Errors.General.StringIsTooSmall;
            }

            if(password.Length > 100)
            {
                return Errors.General.StringIsTooLarge;
            }


            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                AlgorithmName,
                HashSize);

            var passwordHash = new PasswordHash($"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}");

            return passwordHash;
        }

        internal static UnitResult<Error> VerifyPassword(PasswordHash passwordHash,string password)
        {
            string[] valueParts = passwordHash.Value.Split("-");
            byte[] hash = Convert.FromHexString(valueParts[0]);
            byte[] salt = Convert.FromHexString(valueParts[1]);

            var newHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                AlgorithmName,
                HashSize);

            if(!CryptographicOperations.FixedTimeEquals(hash, newHash))
            {
                return Errors.Account.PasswordsDontMatch;
            }


            return UnitResult.Success<Error>();
        }


        public static PasswordHash ConvertFromString(string hash)
        {
            Guard.IsNotNullOrWhiteSpace(hash);

            string[] valueParts = hash.Split("-");

            if(valueParts.Length < 2)
            {
                throw new FormatException("Provided string has invalid format");
            }

            Convert.FromHexString(valueParts[0]);
            Convert.FromHexString(valueParts[1]);

            return new PasswordHash(hash);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(PasswordHash hash)
            => hash.Value;

    }
}