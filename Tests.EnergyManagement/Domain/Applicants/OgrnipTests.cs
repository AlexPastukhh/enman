using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class OgrnipTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = Ogrnip.Create(" 123456789012345 ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("123456789012345");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = Ogrnip.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnipIsRequired);
    }

    [Fact]
    public void Create_fails_for_non_digit_value()
    {
        var result = Ogrnip.Create("12345678901234A");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnipIsInvalid);
    }

    [Fact]
    public void Create_fails_for_invalid_length()
    {
        var result = Ogrnip.Create("12345678901234");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnipIsInvalid);
    }
}
