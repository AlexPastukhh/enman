using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class Ogrnip : ValueObject
{
    public string Value { get; private set; }

    private Ogrnip(string value)
    {
        Value = value;
    }

    private Ogrnip()
    {
        Value = null!;
    }

    public static Result<Ogrnip, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.OgrnipIsRequired);
        }
        else
        {
            var normalized = value.Trim();

            if (normalized.Length != 15 || !normalized.All(char.IsDigit))
            {
                errors.Add(Errors.L1Domain.OgrnipIsInvalid);
            }
            else
            {
                return Result.Success<Ogrnip, IReadOnlyList<Error>>(
                    new Ogrnip(normalized));
            }
        }

        return Result.Failure<Ogrnip, IReadOnlyList<Error>>(errors);
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
