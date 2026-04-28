using CSharpFunctionalExtensions;
using FluentAssertions;
using Hospital.proj.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests.Unit.UserTests
{
    public class PasswordTest
    {
        [Theory]
        [InlineData("Valid1wewe234")]  // Valid password
        [InlineData("MySecurerrrrPass1")]  // Another valid password
        [InlineData("gfgfdgfgdfgdfgPass12")]  // Valid password with minimum length
        public void Returns_PasswordHash_When_Valid_Password_Is_Provided(string password)
        {
            // Act
            var result = Password.HashPassword(password);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Split("-").Length.Should().Be(2); // Ensure there are two parts: hash and salt
        }

        // Test for valid passwords for VerifyPassword method
        [Theory]
        [InlineData("Valid1wewe234")]
        [InlineData("MySecurerrrrPass1")]
        [InlineData("gfgfdgfgdfgdfgPass12")]
        public void Verify_Correct_Password(string providedPassword)
        {
            // Arrange
            var passwordHash = Password.HashPassword(providedPassword).Value;
            
            // Act
            var verifyResult = Password.VerifyPassword(
                passwordHash,
                providedPassword);
            // Assert
            verifyResult.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Cant_Verify_Password_With_Null_Password_Hash_Argument()
        {
            
            //Act
            Action act = () => Password.VerifyPassword(
                null!,
                "some32424dfsdPassword");
            //Arrange
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Verify_Password_With_Null_Password_String_Argument()
        {
            //Arrange
            var passwordHash = Password.HashPassword("some32424dfsdPassword").Value;

            //Act
            Action act = () => Password.VerifyPassword(
                passwordHash,
                null!);
            //Arrange
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Verify_Password_With_Null_Arguments()
        {
            
            //Act
            Action act = () => Password.VerifyPassword(
                null!,
                null!);
            //Arrange
            act.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("Valid1wewe234", "Vali1wee234drgreg",
            "account.passwords.don't.match")]
        [InlineData("MySecurerrrrPass1", "MySecurerrrrPass",
            "account.passwords.don't.match")]
        [InlineData("gfgfdgfgdfgdfgPass12", "fdgfgdfgdfgPass12",
            "account.passwords.don't.match")]
        public void Fails_To_Verify_Password_That_Doesnt_Match(
            string correctPassword,
            string incorrectPassword,
            string errorCode)
        {
            // Arrange
            var hashResult = Password.HashPassword(correctPassword);
            var passwordHash = hashResult.Value;

            // Act
            var verifyResult = Password.VerifyPassword(
                passwordHash,
                incorrectPassword);
            // Assert
            verifyResult.IsSuccess.Should().BeFalse();
            verifyResult.Error.Code.Should().Be(errorCode);
        }

        [Theory]
        [InlineData("",
            "value.is.required")]

        [InlineData("short",
            "string.is.too.small")]

        [InlineData("bigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbigbig",
            "string.is.too.large")]
        public void Returns_Error_When_Invalid_Password_Is_Provided(
            string password, 
            string expectedErrorCode)
        {
            // Act
            var result = Password.HashPassword(password);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedErrorCode);
        }


        [Fact]
        public void Starts_Password_Change_Procedure()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            //Act
            user.Password.StartPasswordChange(currentTime);
            //Assert
            user.Password.CurrentPasswordChange.HasValue.Should().BeTrue();
            var currentChangeProcedure = user.Password.CurrentPasswordChange.Value;

            currentChangeProcedure.Secret.Should().NotBeNullOrWhiteSpace();
            currentChangeProcedure.SecretExpirationTime.Should().Be(currentTime.AddMinutes(5));
            currentChangeProcedure.SubmitExpirationTime.HasValue.Should().BeFalse();
            currentChangeProcedure.IsSubmitted.Should().BeFalse();
            currentChangeProcedure.IsAttemptedToSubmit.Should().BeFalse();

        }


        [Fact]
        public void Submits_Password_Change()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            user.Password.StartPasswordChange(currentTime);
            var secret = user.Password.CurrentPasswordChange.Value.Secret;
            //Act
            user.Password.SubmitPasswordChange(secret, currentTime);
            //Assert
            user.Password.CurrentPasswordChange.HasValue.Should().BeTrue();
            var currentChangeProcedure = user.Password.CurrentPasswordChange.Value;

            currentChangeProcedure.IsSubmitted.Should().BeTrue();
            currentChangeProcedure.IsAttemptedToSubmit.Should().BeTrue();

        }

        [Fact]
        public void Cant_Submit_Password_Change_With_Null_Secret_Argument()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            //Act
            Action act = () => user.Password.SubmitPasswordChange(null!, currentTime);
            //Arrange
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Submit_Password_Change_Without_Starting_Of_Procedure()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            //Act
            Action act =()=>user.Password.SubmitPasswordChange("secret",currentTime);
            //Arrange
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Submit_Password_Change_With_Invalid_Secret()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            user.Password.StartPasswordChange(currentTime);
            //Act
            var result =user.Password.SubmitPasswordChange("secret", currentTime);
            //Arrange
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("account.change.password.secret.is.invalid");
        }

        [Fact]
        public void Cant_Submit_Password_Change_With_Expired_Secret()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            user.Password.StartPasswordChange(currentTime);
            var secret = user.Password.CurrentPasswordChange.Value.Secret;
            //Act
            var result =user.Password.SubmitPasswordChange(secret, currentTime.AddMinutes(7));
            //Arrange
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("account.change.password.secret.has.expired");
        }


        [Fact]
        public void Changes_Password()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);

            user.Password.StartPasswordChange(currentTime);
            var secret = user.Password.CurrentPasswordChange.Value.Secret;

            user.Password.SubmitPasswordChange(secret, currentTime);

            var newPasswordStr = "Anothereword24sdfdf";
            var newPassword = Password.HashPassword(newPasswordStr).Value;
            //Act

            user.Password.ChangePasswordOrThrow(newPassword,currentTime);

            //Assert
            user.Password.Hash.Should().Be(newPassword);          
        }

        [Fact]
        public void Cant_Change_Password_With_Null_Password_Hash_Provided()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);

            user.Password.StartPasswordChange(currentTime);
            var secret = user.Password.CurrentPasswordChange.Value.Secret;

            user.Password.SubmitPasswordChange(secret, currentTime);

            PasswordHash newPassword = default!;
            //Act

            Action act =()=>user.Password.ChangePasswordOrThrow(newPassword, currentTime);
            //Assert

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Change_Password_Without_Starting_Of_Procedure()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);
            
            var newPasswordStr = "Anothereword24sdfdf";
            var newPassword = Password.HashPassword(newPasswordStr).Value;
            //Act

            Action act=()=>user.Password.ChangePasswordOrThrow(newPassword, currentTime);

            //Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Change_Password_Without_Submitting_Of_Procedure()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);

            user.Password.StartPasswordChange(currentTime);

            var newPasswordStr = "Anothereword24sdfdf";
            var newPassword = Password.HashPassword(newPasswordStr).Value;
            //Act

            Action act = () => user.Password.ChangePasswordOrThrow(newPassword, currentTime);

            //Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void Cant_Change_Password_Because_Submit_Has_Expired()
        { //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var user = UnitTestHelper.GetTestUser(currentTime);

            user.Password.StartPasswordChange(currentTime);
            var secret = user.Password.CurrentPasswordChange.Value.Secret;

            user.Password.SubmitPasswordChange(secret, currentTime);

            var newPasswordStr = "Anothereword24sdfdf";
            var newPassword = Password.HashPassword(newPasswordStr).Value;
            //Act

            var result =user.Password.ChangePasswordOrThrow(newPassword, currentTime.AddMinutes(20));

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("account.change.password.submit.has.expired");
        }
    }
}
