using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class Ogrn : ValueObject
{
    public string Value { get; private set; }

    private Ogrn(string value)
    {
        Value = value;
    }

    private Ogrn()
    {
        Value = null!;
    }

    public static Result<Ogrn, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.OgrnIsRequired);
        }
        else
        {
            var normalized = value.Trim();

            if (normalized.Length != 13 || !normalized.All(char.IsDigit))
            {
                errors.Add(Errors.L1Domain.OgrnIsInvalid);
            }
            else
            {
                return Result.Success<Ogrn, IReadOnlyList<Error>>(
                    new Ogrn(normalized));
            }
        }

        return Result.Failure<Ogrn, IReadOnlyList<Error>>(errors);
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
