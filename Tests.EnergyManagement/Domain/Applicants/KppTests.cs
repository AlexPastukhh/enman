using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class KppTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = Kpp.Create(" 123456789 ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("123456789");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = Kpp.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.KppIsRequired);
    }

    [Fact]
    public void Create_fails_for_non_digit_value()
    {
        var result = Kpp.Create("12345678A");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.KppIsInvalid);
    }

    [Fact]
    public void Create_fails_for_invalid_length()
    {
        var result = Kpp.Create("12345678");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.KppIsInvalid);
    }
}
