using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server;
using Hospital.proj.Server.Contracts;
using Hospital.proj.Server.Infrastructure;
using Hospital.proj.Server.Utils;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;
using static Hospital.proj.Domain.Common.Errors;
using User = Hospital.proj.Domain.Users.User;

namespace Hospital.proj.Tests.Integration
{

    public class AccountTests : IntegrationTest,IClassFixture<WebAppFactory>
    {
        private readonly WebAppFactory _factory;
        private readonly Mock<IEmailService> _emailMock;

        private string _activationCode;
        private string _passwordChangeSecret;

        private  List<Claim> _authClaims;

        private const string FirstPassword = "eword24sdfdf";
        private const string SecondPassword = "Anothereword24sdfdf";
        public AccountTests(WebAppFactory factory)
        {
            _factory = factory;

            _activationCode = _factory.ActivationCode;
            _passwordChangeSecret = _factory.PasswordChangeSecret;
            _emailMock = _factory.EmailMock;


            _authClaims = _factory.AuthClaims;

        }

        public User GetTestUser()
        {
            var currentTime = DateTimeOffset.UtcNow;
            var fullName = FullName.Create("First", "Middle", "Last").Value;
            var email = Email.Create("email@provider.com").Value;

            var passwordHash = Password.HashPassword(FirstPassword).Value;

            var user = User.CreateAccount(fullName, email, passwordHash, currentTime);

            return user;
        }

        public async Task<User> GetUserFromDbAsync()
        {
            User user = default!;

            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
                user = await db.Users.FirstOrDefaultAsync();
            }

            return user!;
        }
        //private string GetLink(string action,
        //    string controller,
        //    string code,
        //    string scheme = "https")
        //{
        //    string link = default!;
        //    using(var scope = _factory.Services.CreateScope())
        //    {
        //        var httpContext = scope.ServiceProvider.GetRequiredService
        //            <IHttpContextAccessor>();
        //        var linkGenerator = scope.ServiceProvider.GetRequiredService
        //            <LinkGenerator>();
        //        var linkFactory = new VerificationLinkFactory(httpContext, linkGenerator);


        //        link = linkFactory.CreateLink(action, controller, code, scheme);
        //    }

        //    return link;
        //}

        //[Fact, Order(1)]
        //private void ClearDatabase()
        //{
        //    string query =
        //    "DELETE FROM dbo.Users;";
        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        var command = new SqlCommand(query, connection)
        //        {
        //            CommandType = CommandType.Text
        //        };
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}



        [Fact,Order(2)]
        public async Task A_Creates_Account()
        {
            //Arrange 
            var testUser = GetTestUser();

            var nameDto = new NameDto()
            {
                FirstName = testUser.FullName.FirstName,
                MiddleName = testUser.FullName.MiddleName,
                LastName = testUser.FullName.LastName
            };

            var registerDto = new RegisterDto()
            {
                Name = nameDto,
                Email = testUser.Email,
                Password = FirstPassword,
                PasswordConfirm = FirstPassword
            };
            
            


            var client = _factory
                .CreateClient();

            //Act
            var response = await client.PostAsJsonAsync("api/Account/register", registerDto);

            // Read the body of the response
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.Should().BeSuccessful($"{response.StatusCode}, " +
                $"{responseBody}, " +
                $"{response.RequestMessage}");

            var createdUser = await GetUserFromDbAsync();

            createdUser.Should().NotBeNull();
            createdUser!.Email.Should().Be(testUser.Email);

            var expecFullName = FullName.Create(registerDto.Name.FirstName,
                registerDto.Name.MiddleName,
                registerDto.Name.LastName).Value;

            createdUser.FullName.Should().Be(expecFullName);

            _factory.ActivationCode = createdUser!.AccountActivation.SecurityCode.ToString();

            _emailMock.Verify(e => e.SendEmailAsync(testUser.Email,
                "Activate your account",
                It.Is<string>(body => body.Contains(EmailHelpers.ActivateAccountMessage(
                    $"https://localhost/api/Account/activate/{_factory.ActivationCode}"))),
                It.IsAny<CancellationToken>()), Times.Once);


            _factory.AuthClaims=new List<Claim>()
            {
                new Claim(ClaimTypes.Email,createdUser.Email),
                new Claim(ClaimTypes.NameIdentifier,createdUser.Id.ToString()),
                new Claim(ClaimTypes.Name,createdUser.FullName)
            };
        }
        [Fact,Order(3)]
        public async Task B_Actinvates_Account()
        {
            //Arrange
            
            var client = _factory.CreateClient();
            //Act
            var response = await client.GetAsync(
                $"api/Account/activate/{_factory.ActivationCode}");
            //Assert
            var responseBody = await response.Content.ReadAsStringAsync();

            var userAfterAct =await GetUserFromDbAsync();

            userAfterAct!.AccountActivation.IsActivated.Should().BeTrue();
            userAfterAct.AccountActivation.ActivatedAt.Value.Should()
                .BeCloseTo(DateTimeOffset.UtcNow,TimeSpan.FromSeconds(10));
            userAfterAct.AccountActivation.IsActivationAttempted.Should().BeTrue();
            

            
        }

        [Fact,Order(4)]
        public async Task Logins_Into_Account()
        {
            //Arrage

            var testUser = GetTestUser();
            var loginDto = new LoginDto()
            {
                Password = FirstPassword,
                Email = testUser.Email
            };

            var client = _factory
                .AuthenticatingInstance( out var authServiceMock)
                .CreateClient();

            //Act

            var response = await client.PostAsJsonAsync("api/Account/login", loginDto);

            //Assert
            var responseBody = await response.Content.ReadAsStringAsync();

            response.Should().BeSuccessful($"{response.StatusCode}, " +
                $"{responseBody}, " +
                $"{response.RequestMessage}");
            
            var claimTypes =new List<string>{
                ClaimTypes.Email,
                ClaimTypes.NameIdentifier,
                ClaimTypes.Name};

            authServiceMock.Verify(a=>a.SignInAsync(
                It.IsAny<HttpContext>(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                It.Is<ClaimsPrincipal>(principal=>
                    principal.Claims.Count() ==3 && 
                    principal.Claims.Select(c=>c.Type)
                        .All(type=>claimTypes
                            .Any(ct=>ct==type))),
                It.IsAny<AuthenticationProperties>()
            ),Times.Once);
        }

        [Fact, Order(5)]
        public async Task Start_Password_Change_And_Send_Email_With_Reset_Password_Link()
        {            
            //Act

            var client = _factory
                .AuthenticatedInstance(_authClaims.ToArray())
                .CreateClient();

            var response = await client.GetAsync("api/Account/password/change/start");

            //Assert
            var responseBody = await response.Content.ReadAsStringAsync();

            response.Should().BeSuccessful($"{response.StatusCode}, " +
                $"{responseBody}, " +
                $"{response.RequestMessage}");

            var user = await GetUserFromDbAsync();

            _factory.PasswordChangeSecret = user!.Password
                .CurrentPasswordChange
                .Value
                .Secret
                .ToString();

            _emailMock.Verify(
                e=>e.SendEmailAsync(
                    user.Email,
                    "Change password",
                    It.Is<string>(body => body.Contains(
                        EmailHelpers.ChangePasswordMessage(
                            $"https://localhost/api/Account/password/change/{_factory.PasswordChangeSecret}"))),
                    It.IsAny<CancellationToken>()
                ),Times.Once);
        }

        [Fact, Order(6)]
        public async Task Submits_Password_Reset_And_Redirect_To_Frontend_Password_Change_Page()
        {
            //Arrage

            var client = _factory
                .AuthenticatedInstance(_authClaims.ToArray())
                .CreateClient();
            //Act

            var response = await client.GetAsync(
                $"api/Account/password/change/{_passwordChangeSecret}");

            //Assert
            var responseBody = await response.Content.ReadAsStringAsync();

            var user = await GetUserFromDbAsync();

            var passwordChange = user.Password.CurrentPasswordChange.Value;

            passwordChange.IsSubmitted.Should().BeTrue();
            passwordChange.IsAttemptedToSubmit.Should().BeTrue();
        }

        [Fact, Order(7)]
        public async Task Changes_Password()
        {
            //Arrage
            var changePwdDto = new ChangePasswordDto()
            {
                Password = SecondPassword,
                PasswordConfirm = SecondPassword
            };
            var client = _factory
                .AuthenticatedInstance(_authClaims.ToArray())
                .CreateClient();
            //Act

            var response = await client.PostAsJsonAsync(
                "api/Account/password/change", changePwdDto);

            //Assert
            var responseBody = await response.Content.ReadAsStringAsync();

            response.Should().BeSuccessful($"{response.StatusCode}, " +
                $"{responseBody}, " +
                $"{response.RequestMessage}");
        }


        //private HttpClient CreateClientWithMocks(params Mock<object>[] args)
        //{
        //    var types = new List<Type>();

        //    foreach (var arg in args)
        //    {
        //        var type = arg.GetType();
        //        types.Add(arg.GetType().GetGenericArguments()[0]);
        //    }

        //    if (types.Count() != args.Length)
        //    {
        //        throw new ArgumentException("Mocks of same type provided");
        //    }

        //    var client = webAppFactory.WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureServices(services =>
        //        {
        //            for (int i = 0; i < types.Count(); i++)
        //            {
        //                var descriptor = services.SingleOrDefault(d => d.ServiceType == types[i]);
        //                if (descriptor is null)
        //                {
        //                    throw new ArgumentException("Service to mock is null by default");
        //                }
        //                services.Remove(descriptor);

        //                services.AddTransient(provider => args[i].Object);

        //            }
        //        });
        //    }).CreateClient();

        //    return client;
        //}
    }
}
