using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class FinalRefusalReason : ValueObject
{
    public const int MaxLength = 2000;

    public string Value { get; private set; }

    private FinalRefusalReason(string value)
    {
        Value = value;
    }

    private FinalRefusalReason()
    {
        Value = null!;
    }

    public static Result<FinalRefusalReason, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.FinalRefusalReasonIsRequired);
        }
        else if (value.Length > MaxLength)
        {
            errors.Add(Errors.L1Domain.FinalRefusalReasonIsTooLong);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<FinalRefusalReason, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<FinalRefusalReason, IReadOnlyList<Error>>(
            new FinalRefusalReason(value.Trim()));
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
