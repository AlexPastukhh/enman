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
    public class UserTest
    {
        

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@example.co.uk")]
        [InlineData("simple.email@example.com")]
        [InlineData("valid_email123@example.org")]
        [InlineData("email.with-numbers123@example.edu")]
        public void Creates_Email_WhenValidEmailIsProvided(string email)
        {
            // Act
            var result = Email.Create(email);

            // Assert
            result.IsSuccess.Should().BeTrue($"Expected success but got failure for email: {email}");
            var CreatedEmail = result.Value;
            CreatedEmail.Value.Should().Be(email);
        }

        [Theory]
        [InlineData(null,
            "value.is.required")]

        [InlineData("",
            "value.is.required")]

        [InlineData("invaliddomain",
            "value.is.invalid")]

        [InlineData("a_very_long_email_address_that_exceeds_the_maximum_allowed_length_of_100_characters1234567890123@example.com",
            "string.is.too.large")]
        public void Returns_Error_WhenInvalidEmailIsProvided(string email, string expectedErrorCode)
        {
            // Act
            var result = Email.Create(email);

            // Assert
            result.IsFailure.Should().BeTrue($"because email '{email}' should be invalid");
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be(expectedErrorCode);
        }

        [Theory]
        [InlineData("John", "Paul", "Doe")]
        [InlineData("Jane", "Marie", "Smith")]
        [InlineData("Michael", "David", "Johnson")]
        public void Creates_FullName_When_Valid_Inputs_Are_Provided(string firstName, string middleName, string lastName)
        {
            // Act
            var result = FullName.Create(firstName, middleName, lastName);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.FirstName.Should().Be(firstName);
            result.Value.MiddleName.Should().Be(middleName);
            result.Value.LastName.Should().Be(lastName);
        }

        // 5 failed test cases with the same validation rules
        [Theory]
        [InlineData(null, "Paul", "Doe",
            "account.firstName.is.required")]

        [InlineData("John", null, "Smith",
            "account.middleName.is.required")]

        [InlineData("John", "Paul", null,
            "account.lastName.is.required")]

        [InlineData("", "Marie", "Smith",
            "account.firstName.is.required")]

        [InlineData("John", "", "Smith",
            "account.middleName.is.required")]

        [InlineData("John", "Marie", "",
            "account.lastName.is.required")]

        [InlineData("DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe", "Paul", "Doe",
            "account.firstName.is.too.large")]

        [InlineData("John", "DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe", "Doe",
            "account.middleName.is.too.large")]

        [InlineData("John", "Paul", "DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe",
            "account.lastName.is.too.large")]

        public void Cant_Create_FullName_Because_Of_One_Invalid_Input(
            string firstName,
            string middleName,
            string lastName,
            string expectedErrorCode)
        {
            // Act
            var result = FullName.Create(firstName, middleName, lastName);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().HaveCount(1); // One error is expected
            result.Error[0].Code.Should().Be(expectedErrorCode);
        }

        [Theory]
        [InlineData(null, "", "Doe",
           "account.firstName.is.required", "account.middleName.is.required", null)]

        [InlineData("John", null, "DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe",
           "account.middleName.is.required", "account.lastName.is.too.large", null)]

        [InlineData(null, null, null,
            "account.firstName.is.required", "account.middleName.is.required", "account.lastName.is.required")]

        [InlineData("", "", "",
            "account.firstName.is.required", "account.middleName.is.required", "account.lastName.is.required")]


        public void Cant_Create_FullName_Because_Of_Multiple_Invalid_Inputs(
           string firstName,
           string middleName,
           string lastName,
           string expectedErrorCode1,
           string expectedErrorCode2,
           string expectedErrorCode3)
        {
            // Act
            var result = FullName.Create(firstName, middleName, lastName);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error[0].Code.Should().Be(expectedErrorCode1);
            result.Error[1].Code.Should().Be(expectedErrorCode2);

            if (expectedErrorCode3 is not null)
            {
                result.Error[2].Code.Should().Be(expectedErrorCode3);
            }
        }


        [Theory]
        [InlineData("", "value.is.required")]
        [InlineData(null, "value.is.required")]
        [InlineData("avelimit", "string.is.too.small")]
        public void Return_Errors_When_Invalid_Input_Was_Provided(string password, string expectedErrorCode)
        {
            // Act
            var result = Password.HashPassword(password);


            // Assert 

            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedErrorCode);

        }


        

        [Fact]
        public void Creates_User_Account()
        {
            //Act
            var result = UnitTestHelper.GetTestUser(DateTimeOffset.UtcNow);

        }




    }
}
