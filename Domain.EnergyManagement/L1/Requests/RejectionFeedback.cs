using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class RejectionFeedback : ValueObject
{
    public const int MaxLength = 1000;

    public string Value { get; }

    private RejectionFeedback(string value)
    {
        Value = value;
    }

    private RejectionFeedback()
    {
        Value = null!;
    }

    public static Result<RejectionFeedback, IReadOnlyList<Error>> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<RejectionFeedback, IReadOnlyList<Error>>(
                [Errors.General.ValueIsRequired]);
        }

        if (value.Length > MaxLength)
        {
            return Result.Failure<RejectionFeedback, IReadOnlyList<Error>>(
                [Errors.L1Domain.RejectionFeedbackIsTooLong]);
        }

        return Result.Success<RejectionFeedback, IReadOnlyList<Error>>(
            new RejectionFeedback(value));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}
