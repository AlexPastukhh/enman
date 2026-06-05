using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Account;

namespace Domain.EnergyManagement.DocumentManaging
{
public class PhoneNumber : ValueObject
{
    public const int MaxLength = 12;

    /// <summary>
    /// Обёртка для телефонного номера. Содержит валидацию по регулярному выражению.
    /// </summary>
    /// <remarks>
    /// Пример шаблона и соответствие форматов:
    /// ![Phone regex](../docs/images/phone_regex.svg)
    /// </remarks>
    public string Value { get; }
    private PhoneNumber(string value)
    {
        Value = value;
    }
    private PhoneNumber(){}
    public static Result<PhoneNumber, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();
        if (string.IsNullOrEmpty(value))
        {
            errors.Add(PhoneNumberIsRequired);
            return Result.Failure<PhoneNumber, IReadOnlyList<Error>>(errors);
        }
        if (!IsValidPhoneNumber(value))
        {
            errors.Add(PhoneNumberIsInvalid);
            return Result.Failure<PhoneNumber, IReadOnlyList<Error>>(errors);
        }
        
        return Result.Success<PhoneNumber, IReadOnlyList<Error>>(new PhoneNumber(value));
    }
    private static bool IsValidPhoneNumber(string value)
    {
        // Russian phone number: +7, 8, or 7 prefix followed by exactly 10 digits
        return Regex.IsMatch(value, @"^(\+7|8|7)\d{10}$");
    }
    public static bool operator ==(PhoneNumber left, string right)
    {
        if (ReferenceEquals(left, null))
            return string.IsNullOrEmpty(right);
            
        return left.Value == right;
    }
    
    public static bool operator !=(PhoneNumber left, string right)
    {
        return !(left == right);
    }
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
}
