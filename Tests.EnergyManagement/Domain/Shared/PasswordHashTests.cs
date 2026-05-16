using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error.Errors;
using Tests.EnergyManagement.TestHelpers;
using Xunit;

namespace Tests.EnergyManagement.Domain.Shared;

public class PasswordHashTests
{
    [Fact]
    public void PasswordHashValidatePlainTextPassword_ReturnsPasswordRuleErrors()
    {
        // Act
        var result = PasswordHash.ValidatePlainTextPassword("short");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Account.PasswordIsTooShort);
        result.Error.Should().Contain(Account.PasswordLacksSpecialCharacters);
    }

    [Fact]
    public void PasswordHashCreateFromPlainTextPassword_ReturnsHashForValidPassword()
    {
        // Act
        var result = PasswordHash.CreateFromPlainTextPassword(ValidTestData.ValidPassword);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Contain("-");
        result.Value.Value.Should().NotBe(ValidTestData.ValidPassword);
    }

    [Fact]
    public void PasswordHashVerifyPlainTextPassword_SucceedsForCorrectPassword()
    {
        // Arrange
        var hash = PasswordHash.CreateFromPlainTextPassword(ValidTestData.ValidPassword).Value;

        // Act
        var result = PasswordHash.VerifyPlainTextPassword(hash, ValidTestData.ValidPassword);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(ValidTestData.DifferentValidPassword)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void PasswordHashVerifyPlainTextPassword_FailsForWrongOrBlankPassword(string differentPassword)
    {
        // Arrange
        var hash = PasswordHash.CreateFromPlainTextPassword(ValidTestData.ValidPassword).Value;

        // Act
        var result = PasswordHash.VerifyPlainTextPassword(hash, differentPassword);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Account.PasswordConfirmationDoesntMatch);
    }

    [Fact]
    public void PasswordHashConvertFromString_RestoresExistingHash()
    {
        // Arrange
        var hash = PasswordHash.CreateFromPlainTextPassword(ValidTestData.ValidPassword).Value;

        // Act
        var restored = PasswordHash.ConvertFromString(hash.Value);

        // Assert
        restored.Value.Should().Be(hash.Value);
    }
}
