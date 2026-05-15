using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Security;

public static partial class L1PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;

    public static Result<PasswordHash, IReadOnlyList<Error>> HashPassword(string password)
    {
        var validation = ValidatePassword(password);
        if (validation.Count > 0)
        {
            return Result.Failure<PasswordHash, IReadOnlyList<Error>>(validation);
        }

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            AlgorithmName,
            HashSize);

        var hashString = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";

        return Result.Success<PasswordHash, IReadOnlyList<Error>>(
            PasswordHash.ConvertFromString(hashString));
    }

    public static bool VerifyPassword(PasswordHash passwordHash, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        var valueParts = passwordHash.Value.Split("-");
        if (valueParts.Length != 2)
        {
            return false;
        }

        byte[] hash;
        byte[] salt;
        try
        {
            hash = Convert.FromHexString(valueParts[0]);
            salt = Convert.FromHexString(valueParts[1]);
        }
        catch (FormatException)
        {
            return false;
        }

        var newHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            AlgorithmName,
            HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }

    private static IReadOnlyList<Error> ValidatePassword(string password)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add(Errors.Account.PasswordIsRequired);
            return errors;
        }

        if (password.Length < 12)
        {
            errors.Add(Errors.Account.PasswordIsTooShort);
        }

        if (password.Length > 100)
        {
            errors.Add(Errors.Account.PasswordIsTooLong);
        }

        if (!SpecialCharacterRegex().IsMatch(password))
        {
            errors.Add(Errors.Account.PasswordLacksSpecialCharacters);
        }

        return errors;
    }

    [GeneratedRegex(@"(?=.*[!@#$%^&*\(\)])")]
    private static partial Regex SpecialCharacterRegex();
}
