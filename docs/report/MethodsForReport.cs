
// Демонстрационная реализация логики создания и верификации паролей.
// Поведение: валидация -> PBKDF2-хеширование -> проверка с использованием соли.
public class Password
    {
        private readonly PasswordHash _hash;


        private Password(PasswordHash hash)
        {
            _hash = hash;
        }


	// Валидирует входной пароль и возвращает объект с хешем при успехе.
	// В случае ошибок возвращается список ошибок типа Error.
	public static Result<Password, IReadOnlyList<Error>> Create(string password)
        {
            var errors = new List<Error>();


            var hashResult = PasswordHash.HashPassword(password);
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


		// Проверяет соответствие переданного пароля сохранённому хешу.
		// Возвращает UnitResult — либо успех, либо одна ошибка.
		public UnitResult<Error> VerifyPassword(string password)
        {
            return PasswordHash.VerifyPassword(_hash, password);
        }


       
        private class PasswordHash
        {
            public string Value { get; init; }


            private const int SaltSize = 16;
            private const int HashSize = 32;
            private const int Iterations = 100000;
            private static readonly Regex SpecialCharPattern = new(@"(?=.*[!@#$%^&*()])");
            private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;


            private PasswordHash(string value) => Value = value;


			// Валидирует пароль и вычисляет PBKDF2-хеш со случайной солью.
			// Возвращает строку в формате {hashHex}-{saltHex}.
			internal static Result<PasswordHash, IReadOnlyList<Error>> HashPassword(string password)
            {
                var errors = new List<Error>();


                var validate = ValidatePassword(password);
                if (validate.IsFailure)
                {
                    errors.AddRange(validate.Error);
                    return Result.Failure<PasswordHash, IReadOnlyList<Error>>(errors);
                }


                byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    Iterations,
                    AlgorithmName,
                    HashSize);


                var passwordHash = new PasswordHash($"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}");


                return Result.Success<PasswordHash, IReadOnlyList<Error>>(passwordHash);
            }


			// Проверяет правила пароля (не пустой, длина, наличие спецсимволов).
			// Возвращает список ошибок при нарушениях.
			internal static UnitResult<IReadOnlyList<Error>> ValidatePassword(string password)
            {
                var errors = new List<Error>();


                if (string.IsNullOrWhiteSpace(password))
                {
                    return Result.Failure<PasswordHash, IReadOnlyList<Error>>(
                        errors.Append(Account.PasswordIsRequired).ToList());
                }


                if (password.Length < 12)
                {
                    errors.Add(Account.PasswordIsTooShort);
                }


                if (password.Length > 100)
                {
                    errors.Add(Account.PasswordIsTooLong);
                }


                if (SpecialCharPattern.Match(password).Success == false)
                {
                    errors.Add(Account.PasswordLacksSpecialCharacters);
                }


                if (errors.Any())
                {
                    return Result.Failure<PasswordHash, IReadOnlyList<Error>>(errors);
                }


                return Result.Success<IReadOnlyList<Error>>();
            }


			// Пересчитывает хеш для переданного пароля и выполняет
			// фиксированное по времени сравнение с сохранённым хешем.
			internal static UnitResult<Error> VerifyPassword(PasswordHash passwordHash, string password)
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
                    return Account.PasswordConfirmationDoesntMatch;
                }


                return UnitResult.Success<Error>();
            }
        }
    }


   
	// Простая реализация валидации электронной почты (для отчёта).
	public class Email
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string Value { get; }


        private Email(string value) => Value = value;
        private Email() { }


	// Валидирует userEmail и возвращает объект Email или список ошибок.
	public static Result<Email, IReadOnlyList<Error>> Create(string userEmail)
        {
            var errors = new List<Error>();


            if (string.IsNullOrWhiteSpace(userEmail))
            {
                errors.Add(Account.EmailIsRequired);
                return Result.Failure<Email, IReadOnlyList<Error>>(errors);
            }


            if (!EmailRegex.IsMatch(userEmail))
            {
                errors.Add(Account.EmailIsInvalid);
                return Result.Failure<Email, IReadOnlyList<Error>>(errors);
            }


            return Result.Success<Email, IReadOnlyList<Error>>(new Email(userEmail));
        }
    }


   
	// Простая реализация валидации телефонного номера в формате РФ.
	public class PhoneNumber
    {
        public string Value { get; }
        private PhoneNumber(string value) => Value = value;
        private PhoneNumber() { }


	// Валидирует строку телефона (не пустая и соответствует маске).
	// Возвращает объект PhoneNumber или список ошибок.
	public static Result<PhoneNumber, IReadOnlyList<Error>> Create(string value)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(value))
            {
                errors.Add(Account.PhoneNumberIsRequired);
                return Result.Failure<PhoneNumber, IReadOnlyList<Error>>(errors);
            }
            if (!IsValidPhoneNumber(value))
            {
                errors.Add(Account.PhoneNumberIsInvalid);
                return Result.Failure<PhoneNumber, IReadOnlyList<Error>>(errors);
            }


            return Result.Success<PhoneNumber, IReadOnlyList<Error>>(new PhoneNumber(value));
        }


		// Проверка по регулярному выражению: +7, 8 или 7, затем 10 цифр.
		private static bool IsValidPhoneNumber(string value)
        {
            return Regex.IsMatch(value, @"^(\+7|8|7)\d{10}$");
        }
    }
