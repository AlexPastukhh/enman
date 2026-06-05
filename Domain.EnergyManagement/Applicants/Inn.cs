using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class Inn : ValueObject
{
    public string Value { get; private set; }

    private Inn(string value)
    {
        Value = value;
    }

    private Inn()
    {
        Value = null!;
    }

    public static Result<Inn, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.InnIsRequired);
        }
        else
        {
            var normalized = value.Trim();

            if ((normalized.Length != 10 && normalized.Length != 12) || !normalized.All(char.IsDigit))
            {
                errors.Add(Errors.L1Domain.InnIsInvalid);
            }
            else
            {
                return Result.Success<Inn, IReadOnlyList<Error>>(
                    new Inn(normalized));
            }
        }

        return Result.Failure<Inn, IReadOnlyList<Error>>(errors);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
