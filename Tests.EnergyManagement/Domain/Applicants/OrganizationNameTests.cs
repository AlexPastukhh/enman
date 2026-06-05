using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class OrganizationNameTests
{
    [Fact]
    public void Create_succeeds_and_trims_value()
    {
        var result = OrganizationName.Create(" Test Organization ");

        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be("Test Organization");
    }

    [Fact]
    public void Create_fails_for_blank_value()
    {
        var result = OrganizationName.Create(" ");

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OrganizationNameIsRequired);
    }

    [Fact]
    public void Create_fails_when_value_is_too_long()
    {
        var result = OrganizationName.Create(new string('a', OrganizationName.MaxLength + 1));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OrganizationNameIsTooLong);
    }
}
