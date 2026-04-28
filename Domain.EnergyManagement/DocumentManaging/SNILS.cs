using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error.Errors.Account;


namespace Domain.EnergyManagement.DocumentManaging
{
    public class SNILS : ValueObject
{
    private static readonly Regex DigitsOnlyRegex = new(@"^\d{11}$", RegexOptions.Compiled);

    public string Value { get; }

    private SNILS(string value)
    {
        Value = value;
    }

    public static Result<SNILS, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(SNILSIsRequired);
            return Result.Failure<SNILS, IReadOnlyList<Error>>(errors);
        }

        // Удаляем все нецифровые символы
        string digitsOnly = Regex.Replace(value, @"\D", "");

        // Проверяем, что осталось 11 цифр
        if (!DigitsOnlyRegex.IsMatch(digitsOnly))
        {
            errors.Add(SNILSIsInvalid);
            return Result.Failure<SNILS, IReadOnlyList<Error>>(errors);
        }

        // Валидация контрольной суммы
        if (!IsValidChecksum(digitsOnly))
        {
            errors.Add(SNILSIsInvalid);
            return Result.Failure<SNILS, IReadOnlyList<Error>>(errors);
        }

        // Сохраняем в нормализованном виде (только цифры)
        return Result.Success<SNILS, IReadOnlyList<Error>>(new SNILS(digitsOnly));
    }

    private static bool IsValidChecksum(string digits)
    {
        if (digits.Length != 11)
            return false;

        // Берем первые 9 цифр для вычисления контрольной суммы
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            int digit = digits[i] - '0';
            int position = 9 - i; // Позиция с конца (9, 8, 7, ..., 1)
            sum += digit * position;
        }

        // Вычисляем контрольное число
        int checkNumber;
        if (sum < 100)
        {
            checkNumber = sum;
        }
        else if (sum == 100 || sum == 101)
        {
            checkNumber = 0;
        }
        else
        {
            int remainder = sum % 101;
            checkNumber = remainder == 100 ? 0 : remainder;
        }

        // Получаем контрольное число из СНИЛС (последние 2 цифры)
        int providedCheckNumber = int.Parse(digits.Substring(9, 2));

        return checkNumber == providedCheckNumber;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    // Опционально: форматированный вывод
    public string ToFormattedString()
    {
        return $"{Value.Substring(0, 3)}-{Value.Substring(3, 3)}-{Value.Substring(6, 3)} {Value.Substring(9, 2)}";
    }

    public static implicit operator string(SNILS snils) => snils.Value;
}
}