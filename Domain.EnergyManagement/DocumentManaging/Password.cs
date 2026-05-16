using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using CommunityToolkit.Diagnostics;
using static Domain.EnergyManagement.Common.Error;
using static Domain.EnergyManagement.Common.Error.Errors;
using System.Text.RegularExpressions;

namespace Domain.EnergyManagement.DocumentManaging
{
    /// <summary>
    /// Представляет пароль пользователя как объект-значение. Класс хранит
    /// пароль в виде хешированной строки и предоставляет фабричные методы
    /// и методы верификации для создания и проверки пароля без раскрытия
    /// самого текста пароля.
    /// </summary>
    /// <remarks>
    /// Пример и пояснение:
    /// 
    /// ![Пример силы пароля](../docs/images/password_example.svg)
    /// 
    /// Формула энтропии пароля (приблизительно):
    /// $$H = L \log_2 N$$
    /// где $L$ — длина пароля, а $N$ — размер множества допустимых символов.
    /// 
    /// График зависимости энтропии от длины:
    /// 
    /// ![График энтропии](../docs/images/password_graph.svg)
    /// </remarks>
    public class Password : ValueObject
    {
        private PasswordHash? _hash { get; set; }
        /// <summary>
        /// Возвращает строковое представление хеша пароля.
        /// </summary>
        public string Hash
        {
            get => _hash!.Value;
            private init => _hash = !string.IsNullOrEmpty(value) ?
                PasswordHash.ConvertFromString(value)
                : null;
        }

        // Modify constructor to set via property
        private Password(PasswordHash hash)
        {
            _hash = hash;
        }

        private Password()
        {
        }

        /// <summary>
        /// Создаёт новый экземпляр <see cref="Password"/>, валидируя и
        /// хешируя переданный пароль в открытом виде.
        /// </summary>
        /// <param name="password">Пароль в открытом виде для валидации и хеширования.</param>
        /// <returns>
        /// <see cref="Result{TValue,TError}"/>, содержащий созданный
        /// <see cref="Password"/> при успешной валидации или список ошибок
        /// <see cref="Error"/> при неудаче.
        /// </returns>
        public static Result<Password, IReadOnlyList<Error>> Create(string password)
        {
            var errors = new List<Error>();

            var hashResult = PasswordHash.CreateFromPlainTextPassword(password);
            if (hashResult.IsFailure)
            {
                errors.AddRange(hashResult.Error);
            }

            if (errors.Any())
            {
                return Result.Failure<Password, IReadOnlyList<Error>>(errors);
            }

            return Result.Success<Password, IReadOnlyList<Error>>(new Password(hashResult.Value));
        }

        /// <summary>
        /// Проверяет, соответствует ли переданный пароль в открытом виде
        /// сохранённому хешу.
        /// </summary>
        /// <param name="password">Пароль в открытом виде для проверки.</param>
        /// <returns>Результат <see cref="UnitResult{Error}"/>, указывающий на успех или возвращающий ошибку.</returns>
        public UnitResult<Error> VerifyPassword(string password)
        {
            var result = PasswordHash.VerifyPlainTextPassword(_hash!, password);

            return result;
        }

        /// <summary>
        /// Восстанавливает экземпляр <see cref="Password"/> из существующей
        /// строковой репрезентации хеша пароля.
        /// </summary>
        /// <param name="hashString">Строковое представление хеша пароля.</param>
        /// <returns>Экземпляр <see cref="Password"/>, соответствующий указанному хешу.</returns>
        public static Password ConvertFromString(string hashString)
        {
            var hash = PasswordHash.ConvertFromString(hashString);
            return new Password(hash);

        }

        /// <summary>
        /// Сравнивает <see cref="Password"/> со строковым представлением хеша
        /// на равенство.
        /// </summary>
        public static bool operator ==(Password left, string right)
        {
            if (ReferenceEquals(left, null))
                return string.IsNullOrEmpty(right);

            return left.Hash == right;
        }

        /// <summary>
        /// Сравнивает <see cref="Password"/> со строковым представлением хеша
        /// на неравенство.
        /// </summary>
        public static bool operator !=(Password left, string right)
        {
            return !(left == right);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return _hash!.Value;
        }
    }

    public class PasswordHash : ValueObject
    {
        public string Value { get; init; }

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static Regex SpecialCharPattern = new Regex(@"(?=.*[!@#$%^&*\(\)])");
        private static readonly HashAlgorithmName AlgorithmName =
            HashAlgorithmName.SHA512;

        private PasswordHash(string value)
        {
            Value = value;
        }

        private PasswordHash()
        {
        }

        public static Result<PasswordHash, IReadOnlyList<Error>> CreateFromPlainTextPassword(string? password)
        {
            var errors = new List<Error>();

            var validate = ValidatePlainTextPassword(password);
            if (validate.IsFailure)
            {
                errors.AddRange(validate.Error);
                return Result.Failure<PasswordHash, IReadOnlyList<Error>>(errors);
            }

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password!,
                salt,
                Iterations,
                AlgorithmName,
                HashSize);

            var passwordHash = new PasswordHash($"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}");

            return Result.Success<PasswordHash, IReadOnlyList<Error>>(passwordHash);
        }

        public static UnitResult<IReadOnlyList<Error>> ValidatePlainTextPassword(string? password)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<PasswordHash, IReadOnlyList<Error>>(
                    errors.Append(Errors.Account.PasswordIsRequired).ToList());
            }

            if (password.Length < 12)
            {
                errors.Add(Errors.Account.PasswordIsTooShort);
            }

            if (password.Length > 100)
            {
                errors.Add(Errors.Account.PasswordIsTooLong);
            }

            if (SpecialCharPattern.Match(password).Success == false)
            {
                errors.Add(Errors.Account.PasswordLacksSpecialCharacters);
            }

            if (errors.Any())
            {
                return Result.Failure<PasswordHash, IReadOnlyList<Error>>(errors);
            }

            return Result.Success<IReadOnlyList<Error>>();
        }

        public static UnitResult<Error> VerifyPlainTextPassword(PasswordHash passwordHash, string? password)
        {
            string[] valueParts = passwordHash.Value.Split("-");
            byte[] hash = Convert.FromHexString(valueParts[0]);
            byte[] salt = Convert.FromHexString(valueParts[1]);

            if (string.IsNullOrWhiteSpace(password))
            {
                return UnitResult.Failure<Error>(Account.PasswordConfirmationDoesntMatch);
            }
            var newHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                AlgorithmName,
                HashSize);

            if (!CryptographicOperations.FixedTimeEquals(hash, newHash))
            {
                return Errors.Account.PasswordConfirmationDoesntMatch;
            }


            return UnitResult.Success<Error>();
        }


        public static PasswordHash ConvertFromString(string hash)
        {
            Guard.IsNotNullOrWhiteSpace(hash);

            string[] valueParts = hash.Split("-");

            if (valueParts.Length < 2)
            {
                throw new FormatException("Provided string has invalid format");
            }

            Convert.FromHexString(valueParts[0]);
            Convert.FromHexString(valueParts[1]);

            return new PasswordHash(hash);

        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(PasswordHash hash)
            => hash.Value;

    }
}

