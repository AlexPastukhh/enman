using Domain.EnergyManagement.L1;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.AgreementProposals;

public class FinalRefusalReasonTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = FinalRefusalReason.Create(" Agreement cannot be concluded. ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("Agreement cannot be concluded.");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = FinalRefusalReason.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.FinalRefusalReasonIsRequired);
    }

    [Fact]
    public void Create_fails_when_value_is_too_long()
    {
        var result = FinalRefusalReason.Create(new string('a', FinalRefusalReason.MaxLength + 1));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.FinalRefusalReasonIsTooLong);
    }
}
