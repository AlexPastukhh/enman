using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error.Errors;
using Tests.EnergyManagement.TestHelpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Legacy.Domain;

public abstract class PasswordWrapperTestsBase
{
    protected ITestOutputHelper? _output;

    public static IEnumerable<object[]> GetInvalidPasswordsWithoutSpecialCharacters()
    {
        yield return new object[]
        {
            "NoSpecialChar1",
            new List<Error> { Account.PasswordLacksSpecialCharacters }
        };
        yield return new object[]
        {
            "AnotOne2",
            new List<Error> { Account.PasswordLacksSpecialCharacters, Account.PasswordIsTooShort }
        };
        yield return new object[]
        {
            InvalidTestData.LongPasswordWithoutSpecialChars,
            new List<Error> { Account.PasswordLacksSpecialCharacters, Account.PasswordIsTooLong }
        };
    }

    public void LogTwoErrorCollections(
        IEnumerable<Error> expected,
        IEnumerable<Error> actual)
    {
        _output?.WriteLine("Errors from expected collection:");
        foreach (var error in expected)
        {
            _output?.WriteLine($"Code: {error.Code}");
        }

        _output?.WriteLine("Errors from actual collection:");
        foreach (var error in actual)
        {
            _output?.WriteLine($"Code: {error.Code}");
        }
    }
}

public class PasswordWrapperTests : PasswordWrapperTestsBase
{
    public PasswordWrapperTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void CreatePasswordSuccessfullyWithValidData()
    {
        // Act
        var result = Password.Create(ValidTestData.ValidPassword);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<Password>();
    }

    [Theory]
    [InlineData(ValidTestData.ValidPassword)]
    [InlineData("AnotherValidPass456$")]
    [InlineData("Valid123!@#$%^")]
    [InlineData("Minimum12chars@")]
    [InlineData("AnotherLongButValidPassword1234567890!@#$%^&*()ABC")]
    public void CreatePasswordSuccessfully(string validPassword)
    {
        // Act
        var result = Password.Create(validPassword);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CantCreatePasswordWithoutString(string invalidPassword)
    {
        // Act
        var result = Password.Create(invalidPassword);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Contains(Account.PasswordIsRequired).Should().BeTrue();
    }

    [Fact]
    public void CantCreateWithLongPassword()
    {
        // Act
        var result = Password.Create(InvalidTestData.LongPassword);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Contains(Account.PasswordIsTooLong).Should().BeTrue();
    }

    [Theory]
    [InlineData("11chars!!!")]
    [InlineData("10chars!!")]
    [InlineData("1")]
    [InlineData("323")]
    public void CantCreateWithShortPassword(string shortPassword)
    {
        // Act
        var result = Password.Create(shortPassword);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Contains(Account.PasswordIsTooShort).Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(GetInvalidPasswordsWithoutSpecialCharacters))]
    public void CantCreatePasswordWithoutSpecialCharacters(
        string invalidPassword,
        List<Error> expectedErrors)
    {
        // Act
        var result = Password.Create(invalidPassword);

        // Assert
        result.IsFailure.Should().BeTrue();

        var allErrorsContained = expectedErrors.All(e => result.Error.Contains(e));
        if (!allErrorsContained)
        {
            LogTwoErrorCollections(expectedErrors, result.Error);
        }

        allErrorsContained.Should().BeTrue();
    }

    [Fact]
    public void SuccessfullyVerifiesPassword()
    {
        // Arrange
        var passwordResult = Password.Create(ValidTestData.ValidPassword);
        var password = passwordResult.Value;

        // Act
        var verifyResult = password.VerifyPassword(ValidTestData.ValidPassword);

        // Assert
        verifyResult.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(ValidTestData.ValidPassword, ValidTestData.DifferentValidPassword)]
    [InlineData(ValidTestData.ValidPassword, "")]
    [InlineData(ValidTestData.ValidPassword, " ")]
    [InlineData(ValidTestData.ValidPassword, null)]
    public void CantVerifyDifferentPassword(string validPassword, string differentPassword)
    {
        // Arrange
        var passwordResult = Password.Create(validPassword);
        var password = passwordResult.Value;

        // Act
        var verifyResult = password.VerifyPassword(differentPassword);

        // Assert
        verifyResult.IsFailure.Should().BeTrue();
        verifyResult.Error.Should().Be(Account.PasswordConfirmationDoesntMatch);
    }
}
