using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Account;

namespace Domain.EnergyManagement.DocumentManaging
{
public class FullName :ValueObject
{
    public const int MaxPartLength = 50;

    /// <summary>Имя.</summary>
    public string FirstName { get; }
    /// <summary>Отчество.</summary>
    public string MiddleName { get; }
    /// <summary>Фамилия.</summary>
    public string LastName { get; }
    
    private FullName(
        string firstName,
        string middleName,
        string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }
    private FullName(){}
    
    /// <summary>
    /// Создаёт объект Ф.И.О. с базовой валидацией полей.
    /// Возвращает ошибки при некорректных значениях.
    /// </summary>
    public static Result<FullName, IReadOnlyList<Error>> Create(
        string firstName,
        string middleName,
        string lastName)
    {
        var errors = new List<Error>();

        ValidateFirsteName(firstName, errors);
        ValidateMiddleName(middleName, errors);
        ValidateLastName(lastName, errors);

        if (errors.Count > 0)
        {
            return Result.Failure<FullName, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<FullName, IReadOnlyList<Error>>(new FullName(firstName, middleName, lastName));
    }

     private static void ValidateFirsteName(
        string firstName,
        List<Error> errors)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add(FirstNameIsRequired);
        }
        else if (firstName.Length > MaxPartLength)
        {
            errors.Add(FirstNameIsTooLarge);
        }
    }
        
        private static void ValidateMiddleName(
        string middleName,
        List<Error> errors)
    {
        if (string.IsNullOrWhiteSpace(middleName))
        {
            errors.Add(MiddleNameIsRequired);
        }
        else if (middleName.Length > MaxPartLength)
        {
            errors.Add(MiddleNameIsTooLarge);
        }
    }
        
        private static void ValidateLastName(
        string lastName,
        List<Error> errors)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add(LastNameIsRequired);
        }
        else if (lastName.Length > MaxPartLength)
        {
            errors.Add(LastNameIsTooLarge);
        }
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return FirstName;
        yield return MiddleName;
        yield return LastName;
    }
}
}
