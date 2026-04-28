using FluentAssertions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.proj.Tests.Unit.UserTests
{
    public class ActivationTest
    {
        [Fact]
        public void Generates_Account_Activation_Info_With_Correct_Properties()
        {
            //Arrange
            var currentTime = DateTimeOffset.UtcNow;
            //Act
            var user = UnitTestHelper.GetTestUser(currentTime);
            //Assert
            var account = user.AccountActivation;

            account.SecurityCode.Should().NotBeNullOrEmpty();

            var expectedExpirationTime = currentTime.AddHours(1);
            account.CodeExpirationTime.Should().Be(expectedExpirationTime);

            account.IsActivated.Should().BeFalse();
            account.ActivatedAt.HasValue.Should().BeFalse();
            account.IsActivationAttempted.Should().BeFalse();

            Convert.FromHexString(account.SecurityCode).Length.Should().Be(128);
        }

        [Fact]
        public void Successfully_Activates_Account()
        {
            // Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var timeOfActivation = currentTime.AddMinutes(30);
            var user = UnitTestHelper.GetTestUser(currentTime);
            var account = user.AccountActivation;

            // Act
            var result = user.ActivateAccount(account.SecurityCode, timeOfActivation); // Attempt to activate with the valid code

            // Assert
            result.IsSuccess.Should().BeTrue(); // Activation should succeed
            account.IsActivationAttempted.Should().BeTrue(); // Activation was attempted
            account.IsActivated.Should().BeTrue(); // Account should be activated
        }

        [Fact]
        public void Cant_Activate_Account_When_Invalid_Activation_Code_Is_Provided()
        {
            // Arrange

            var currentTime = DateTimeOffset.UtcNow;
            var timeOfActivation = currentTime.AddMinutes(30);
            var user = UnitTestHelper.GetTestUser(currentTime);
            var account = user.AccountActivation;

            // Act
            var result = user.ActivateAccount(account.SecurityCode + "a", currentTime);

            // Assert
            result.IsSuccess.Should().BeFalse();
            account.IsActivationAttempted.Should().BeTrue();
            account.IsActivated.Should().BeFalse();

            result.Error.Should().BeEquivalentTo(Errors.Account.InValidActivationCode);


        }

        [Fact]
        public void Cant_Activate_Account_When_Activation_Code_Has_Expired()
        {
            // Arrange

            var currentTime = DateTimeOffset.UtcNow;
            var timeOfActivation = currentTime.AddMinutes(30);
            var user = UnitTestHelper.GetTestUser(currentTime);
            var account = user.AccountActivation;

            // Act
            var result = user.ActivateAccount(account.SecurityCode, currentTime.AddHours(2));

            // Assert
            result.IsSuccess.Should().BeFalse();
            account.IsActivationAttempted.Should().BeTrue();
            account.IsActivated.Should().BeFalse();

            result.Error.Should().BeEquivalentTo(Errors.Account.ActivationCodeHasExpired);
        }

        [Fact]
        public void Cant_Activate_Account_With_Null_Security_Code_Argument_Passed()
        {
            // Arrange
            var currentTime = DateTimeOffset.UtcNow;
            var timeOfActivation = currentTime.AddMinutes(30);
            var user = UnitTestHelper.GetTestUser(currentTime);

            // Act
            Action act=()=>user.ActivateAccount(null!, timeOfActivation);
            //Arrange
            act.Should().Throw<Exception>();
        }
    }
}
