using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class OrganizationName : ValueObject
{
    public const int MaxLength = 250;

    public string Value { get; private set; }

    private OrganizationName(string value)
    {
        Value = value;
    }

    private OrganizationName()
    {
        Value = null!;
    }

    public static Result<OrganizationName, IReadOnlyList<Error>> Create(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(Errors.L1Domain.OrganizationNameIsRequired);
        }
        else if (value.Trim().Length > MaxLength)
        {
            errors.Add(Errors.L1Domain.OrganizationNameIsTooLong);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<OrganizationName, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<OrganizationName, IReadOnlyList<Error>>(
            new OrganizationName(value.Trim()));
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
