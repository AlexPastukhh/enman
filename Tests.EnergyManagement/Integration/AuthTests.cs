using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Routes;
using EnergyManagement.Server.Data;
using EnergyManagement.Server.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Integration
{
    
    [Order(1)]
    public class AuthTests:AuthTestsBase,IClassFixture<WebAppFactory>
    {
        public static AuthTestsIndividual TestIndividual = new AuthTestsIndividual();
        
        public static Expression<Func<IAuthenticationService, Task>> ValidSignIn =
            authService=>authService
                    .SignInAsync(
                        It.IsAny<HttpContext>(),
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        It.Is<ClaimsPrincipal>(cp=>
                            TestIndividual.GetClaims().All(c=>
                                cp.HasClaim(c.Type,c.Value))),
                        It.Is<AuthenticationProperties>(ap=>ap.IsPersistent==false));
        public static Expression<Func<IAuthenticationService, Task>> AnySignIn =
            authService=>authService
                    .SignInAsync(
                        It.IsAny<HttpContext>(),
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<AuthenticationProperties>());
                        
        public AuthTests(WebAppFactory factory, ITestOutputHelper output):base(factory, output)
        {   
        }
        
        // +  + need e2e + some js tests + finally 
        // clients request + manager things + some css
        [Fact,Order(0)]
        public async Task Chech()
        {
            //Arrange
            
            //Act
            //Assert
            
        }
        
        // [Fact,Order(0)]
        // public async Task ConstantsDoctorReturnsHealthy()
        // {
        //     //Arrange
        //     var client = _factory.CreateClient();
        //     //Act
        //     var response =  await client.GetAsync("/health");
        //     //Assert
        //     await HttpResponseAssertions.For(response, _output)
        //         .ShouldBeSuccess();
        // }
        
        [Fact,Order(1)]
        public void ClearDatabaseBeforeTests()
        {
            ClearDatabase();
        }
        [Theory,Order(3)]
        [MemberData(nameof(GetInvalidLoginData))]
        public async Task CantLogin(
            string email,
            string password,
            bool emailIsRegistered,
            List<ServerValidationError> expectedErrors)
        {
            //Arrange
            var client = _factory.CheckingAuthentication(out var authServiceMock).CreateClient();
            var dto = new LoginDto(
                email,password);
            if(emailIsRegistered)
            {
                await DatabaseHelpers.AddValidIndividual(_factory);
            }
            
            
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.LoginPath,dto);
            
            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
            
            var problemDetails=await register.Content.ReadFromJsonAsync<ProblemDetails>();
            problemDetails.Should().NotBeNull();
            
            var errors =IntegrationTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();
            
            var allErrorsContained = errors.All(e=>expectedErrors.Contains(e));
            if(allErrorsContained==false)
            {
                LogTwoErrorCollections(expectedErrors,errors!);
            }
            allErrorsContained.Should().BeTrue();
            
            authServiceMock
                .Verify(AnySignIn,Times.Never);
            
            if(emailIsRegistered)
            {
                await DatabaseHelpers.DeleteIndividual(email, _factory);
            }
        }
        
        [Theory,Order(5)]
        [MemberData(nameof(GetInvalidRegisterData))]
        public async Task CantRegisterIndividualWithInvalidData(
            string email,
            string password,
            string passwordConfirmation,
            List<ServerValidationError> expectedErrors)
        {
            //Arrange
            var client = _factory.CreateClient();
            var dto = new RegisterClientDto(
                email,password,passwordConfirmation);
            
            var initCount = DatabaseHelpers.GetCountOfIndividuals(_factory);
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.RegisterIndividualPath,dto);          
            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
            
            var countAfterAttempt = DatabaseHelpers.GetCountOfIndividuals(_factory);
            countAfterAttempt.Should().Be(initCount);
            
            var problemDetails=await register.Content.ReadFromJsonAsync<ProblemDetails>();
            problemDetails.Should().NotBeNull();
            
            var errors =IntegrationTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();
            
            var allErrorsContained = errors.All(e=>expectedErrors.Contains(e));
            if(allErrorsContained==false)
            {
                LogTwoErrorCollections(expectedErrors,errors!);
            }
            allErrorsContained.Should().BeTrue();
        }
          
        [Fact,Order(7)]
        public async Task IndividualClientOnlyRegistersNoLogin()
        {
            //Arrange
            
            var client = _factory.CheckingAuthentication(out var authServiceMock).CreateClient();
            
            var dto = new RegisterClientDto(
                ValidTestData.ValidEmail,ValidTestData.ValidPassword,ValidTestData.ValidPassword);
                        
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.RegisterIndividualPath,dto);          
            
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeSuccess();
            //Assert 
            
            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, dto.Email);
            getIndividual.IsSuccess.Should().BeTrue();
            
            var individual = getIndividual.Value;
            
            (individual.Email==dto.Email).Should().BeTrue();
            individual.Password.VerifyPassword(dto.Password).IsSuccess.Should().BeTrue();
            
            authServiceMock
                .Verify(AnySignIn,Times.Never);
                
            TestIndividual.SetRegisteredIndividual(individual,dto.Password,_output);
            
        }
        
        [Fact,Order(9)]
        public async Task IndividualClientLogins()
        {
            //Arrange
            var claims = TestIndividual.GetClaims();
            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            var client = _factory.CheckingAuthentication(out var authServiceMock).CreateClient();
            
            var dto = TestIndividual.LoginDto;
                
            
            
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.LoginPath,dto);
            

            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeSuccess();
            
            
            
            authServiceMock
                .Verify(ValidSignIn,Times.Once);
        }
        
        [Fact,Order(10)]
        public async Task UnAuthenticatedIndividualCantProvideData()
        {
            //Arrange

            var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                }
            );
            
            var dto = new ProvideIndividualClientsDataDto(
                ValidTestData.TestPhoneNumber,
                new FullNameDto(ValidTestData.FirstName,ValidTestData.MiddleName,ValidTestData.LastName)
            );
            
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.ProvideIndividualClientsDataPath,dto);          
            
            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
                            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, TestIndividual.EmailOrThrow);
            getIndividual.IsSuccess.Should().BeTrue();
            
            var individual = getIndividual.Value;
            individual.PhoneNumber.HasValue.Should().BeFalse();
            
            individual.FullName.HasValue.Should().BeFalse();
                      
        }
        
        [Fact,Order(12)]
        public async Task AuthenticatedIndividualProvidesMoreData()
        {
            
            //Arrange
            var claims = TestIndividual.GetClaims();
            
            var client = _factory.AuthenticatedInstanceWithClaims([..claims]).CreateClient();
            
            var dto = new ProvideIndividualClientsDataDto(
                ValidTestData.TestPhoneNumber,
                new FullNameDto(ValidTestData.FirstName,ValidTestData.MiddleName,ValidTestData.LastName)
            );
            
            var createFullName= FullName.Create(
                dto.FullNameDto.FirstName,
                dto.FullNameDto.MiddleName,
                dto.FullNameDto.LastName);
            var createNumber= PhoneNumber.Create(dto.PhoneNumber);
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.ProvideIndividualClientsDataPath,dto);          
            
            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeSuccess();
                            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, TestIndividual.EmailOrThrow);
            getIndividual.IsSuccess.Should().BeTrue();
            
            TestIndividual.UpdateRegisteredIndividual(getIndividual.Value);
            
            TestIndividual.HasPhoneNumber(createNumber.Value).Should().BeTrue();
            TestIndividual.HasFullName(createFullName.Value).Should().BeTrue();
           
        }
        
        [Fact,Order(15)]
        public async Task AuthenticatedIndividualFetchesUserData()
        {
            
            //Arrange
            var claims = TestIndividual.GetClaims();
            
            var client = _factory.AuthenticatedInstanceWithClaims([..claims]).CreateClient();
            
            //Act
            
            var register = await client.GetAsync(AuthRoutes.GetUserPath);          
            
            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeSuccess();
                            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, TestIndividual.EmailOrThrow);
            getIndividual.IsSuccess.Should().BeTrue();
            
            var userDto =await register.Content.ReadFromJsonAsync<UserDto>();
            userDto.Should().NotBeNull();
            
            userDto!.Email.Should().Be(TestIndividual.EmailOrThrow);
            userDto.Id.Should().Be(TestIndividual.Id);      
        }
        
        [Fact,Order(20)]
        public async Task CantRegisterWithSameEmail()
        {
            //Arrange
            var client = _factory.CreateClient();
            var dto = new RegisterClientDto(
                ValidTestData.ValidEmail,ValidTestData.ValidPassword,ValidTestData.ValidPassword);
            
            var initCount = DatabaseHelpers.GetCountOfIndividuals(_factory);
            
            var isIndividualExists = await DatabaseHelpers.IsIndividualExists(_factory, dto.Email);
            if (!isIndividualExists)
            {
                await DatabaseHelpers.AddValidIndividual(_factory);
            }
            
            //Act
            var register = await client.PostAsJsonAsync(AuthRoutes.RegisterIndividualPath,dto);          
            //Assert 
            
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeStatusCode(
                    ProblemDetailsContract.ValidationStatusCode);
            
            var problemDetails=await register.Content.ReadFromJsonAsync<ProblemDetails>();
            problemDetails.Should().NotBeNull();
            
            var errors = IntegrationTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();

                
            errors!.Contains(ServerValidationErrors.Register.EmailIsRegisteredAlready)
                .Should().BeTrue();
            
            var countAfterAttempt = DatabaseHelpers.GetCountOfIndividuals(_factory);
            countAfterAttempt.Should().Be(initCount);
            
                        
        }
        
  
        
        
        
        
        
        
        
    }
}
