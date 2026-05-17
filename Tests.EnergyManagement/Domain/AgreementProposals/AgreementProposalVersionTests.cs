using Domain.EnergyManagement.L1;
using FluentAssertions;

namespace Tests.EnergyManagement.L1Domain.AgreementProposals;

public class AgreementProposalVersionTests
{
    [Fact]
    public void First_starts_from_one()
    {
        AgreementProposalVersion.First.Value.Should().Be(1);
    }

    [Fact]
    public void Next_increments_version()
    {
        var next = AgreementProposalVersion.First.Next();

        next.Value.Should().Be(2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_rejects_non_positive_value(int value)
    {
        var act = () => new AgreementProposalVersion(value);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void CompareTo_orders_by_value()
    {
        var first = new AgreementProposalVersion(1);
        var second = new AgreementProposalVersion(2);

        first.CompareTo(second).Should().BeNegative();
        second.CompareTo(first).Should().BePositive();
        first.CompareTo(new AgreementProposalVersion(1)).Should().Be(0);
    }
}
