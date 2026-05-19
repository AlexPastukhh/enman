using System.Globalization;

namespace Domain.EnergyManagement;

public readonly record struct AgreementProposalVersion
    : IComparable<AgreementProposalVersion>
{
    public int Value { get; }

    public AgreementProposalVersion(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Agreement proposal version must be positive.");
        }

        Value = value;
    }

    public static AgreementProposalVersion First => new(1);

    public AgreementProposalVersion Next()
    {
        return new AgreementProposalVersion(Value + 1);
    }

    public int CompareTo(AgreementProposalVersion other)
    {
        return Value.CompareTo(other.Value);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.InvariantCulture);
    }
}
