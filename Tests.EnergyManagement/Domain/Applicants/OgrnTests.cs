using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class OgrnTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = Ogrn.Create(" 1234567890123 ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("1234567890123");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = Ogrn.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnIsRequired);
    }

    [Fact]
    public void Create_fails_for_non_digit_value()
    {
        var result = Ogrn.Create("123456789012A");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnIsInvalid);
    }

    [Fact]
    public void Create_fails_for_invalid_length()
    {
        var result = Ogrn.Create("123456789012");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OgrnIsInvalid);
    }
}
