using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class ProposalComment : ValueObject
{
    public const int MaxLength = 2000;

    public string Value { get; private set; }

    private ProposalComment(string value)
    {
        Value = value;
    }

    private ProposalComment()
    {
        Value = null!;
    }

    public static Result<ProposalComment, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.ProposalCommentIsRequired);
        }
        else if (value.Length > MaxLength)
        {
            errors.Add(Errors.L1Domain.ProposalCommentIsTooLong);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<ProposalComment, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ProposalComment, IReadOnlyList<Error>>(
            new ProposalComment(value.Trim()));
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
