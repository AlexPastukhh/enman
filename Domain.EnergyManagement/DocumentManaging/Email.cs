using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Account;

namespace Domain.EnergyManagement.DocumentManaging
{
public class Email:ValueObject
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }
    
    private Email(string value)
    {
        Value = value;
    }
    private Email( )
    {
    }
    
    public static Result<Email, IReadOnlyList<Error>> Create(string userEmail)
    {
        var errors = new List<Error>();
        
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            errors.Add(EmailIsRequired);
            return Result.Failure<Email, IReadOnlyList<Error>>(errors);
        }

        if (!IsValidEmail(userEmail))
        {
            errors.Add(EmailIsInvalid);
            return Result.Failure<Email, IReadOnlyList<Error>>(errors);
        }
         
        return Result.Success<Email, IReadOnlyList<Error>>(new Email(userEmail));
    }

    private static bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
    }

     public static bool operator ==(Email left, string right)
    {
        if (ReferenceEquals(left, null))
            return string.IsNullOrEmpty(right);
            
        return left.Value == right;
    }
    
    public static bool operator !=(Email left, string right)
    {
        return !(left == right);
    }
    
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
}