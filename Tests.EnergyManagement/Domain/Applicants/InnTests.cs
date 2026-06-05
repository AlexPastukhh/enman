using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class InnTests
{
    [Theory]
    [InlineData("1234567890")]
    [InlineData("123456789012")]
    public void Create_succeeds_and_trims_valid_value(string value)
    {
        var result = Inn.Create($" {value} ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(value);
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = Inn.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.InnIsRequired);
    }

    [Fact]
    public void Create_fails_for_non_digit_value()
    {
        var result = Inn.Create("123456789A");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.InnIsInvalid);
    }

    [Fact]
    public void Create_fails_for_invalid_length()
    {
        var result = Inn.Create("12345678901");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.InnIsInvalid);
    }
}
