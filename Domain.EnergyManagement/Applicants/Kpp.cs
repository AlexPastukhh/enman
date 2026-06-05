using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class Kpp : ValueObject
{
    public string Value { get; private set; }

    private Kpp(string value)
    {
        Value = value;
    }

    private Kpp()
    {
        Value = null!;
    }

    public static Result<Kpp, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.KppIsRequired);
        }
        else
        {
            var normalized = value.Trim();

            if (normalized.Length != 9 || !normalized.All(char.IsDigit))
            {
                errors.Add(Errors.L1Domain.KppIsInvalid);
            }
            else
            {
                return Result.Success<Kpp, IReadOnlyList<Error>>(
                    new Kpp(normalized));
            }
        }

        return Result.Failure<Kpp, IReadOnlyList<Error>>(errors);
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
