using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Routes;
using EnergyManagement.Server.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Tests.EnergyManagement.TestHelpers;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.Integration;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Legacy.Integration
{
    
    [Collection("IntegrationTestCollection")]
    public class AuthTests:AuthTestsBase
    {
        private readonly TestIndividualActor _clientActor;
        
        public static Expression<Func<IAuthenticationService, Task>> ValidSignInFor(TestIndividualActor actor) =>
            authService=>authService
                    .SignInAsync(
                        It.IsAny<HttpContext>(),
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        It.Is<ClaimsPrincipal>(cp=>
                            actor.Claims.All(c=>
                                cp.HasClaim(c.Type,c.Value))),
                        It.Is<AuthenticationProperties>(ap=>ap.IsPersistent==false));

        public static Expression<Func<IAuthenticationService, Task>> AnySignIn =
            authService=>authService
                    .SignInAsync(
                        It.IsAny<HttpContext>(),
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        It.IsAny<ClaimsPrincipal>(),
                        It.IsAny<AuthenticationProperties>());
                        
        public AuthTests(IntegrationTestFixture fixure, ITestOutputHelper output):base(fixure.Factory, output)
        {
            _clientActor = fixure.Client;
        }
        
        [Theory]
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
            
            var errors =ProblemDetailsTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();
            
            ProblemDetailsTestHelper.ShouldHaveValidationErrorsEquivalentTo(errors, expectedErrors);
            
            authServiceMock
                .Verify(AnySignIn,Times.Never);
            
            if(emailIsRegistered)
            {
                await DatabaseHelpers.DeleteIndividual(email, _factory);
            }
        }
        
        [Theory]
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
            
            var errors =ProblemDetailsTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();
            
            ProblemDetailsTestHelper.ShouldHaveValidationErrorsEquivalentTo(errors, expectedErrors);
        }
          
        [Fact]
        public async Task IndividualClientOnlyRegistersNoLogin()
        {
            //Arrange
            
            var client = _factory.CheckingAuthentication(out var authServiceMock).CreateClient();
            var email = $"register-{Guid.NewGuid():N}@example.com";
            
            var dto = new RegisterClientDto(
                email,ValidTestData.ValidPassword,ValidTestData.ValidPassword);
                        
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

            await DatabaseHelpers.DeleteIndividual(dto.Email, _factory);
            
        }
        
        [Fact]
        public async Task IndividualClientLogins()
        {
            //Arrange
            var testClient = _clientActor;
            var identity = new ClaimsIdentity(
                testClient.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            var client = _factory.CheckingAuthentication(out var authServiceMock).CreateClient();
            
            var dto = testClient.LoginDto;
                
            
            
            //Act
            
            var register = await client.PostAsJsonAsync(AuthRoutes.LoginPath,dto);
            

            //Assert 
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeSuccess();
            
            
            
            authServiceMock
                .Verify(ValidSignInFor(testClient),Times.Once);
        }
        
        [Fact]
        public async Task UnAuthenticatedIndividualCantProvideData()
        {
            //Arrange
            var testClient = _clientActor;

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
                            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, testClient.Email);
            getIndividual.IsSuccess.Should().BeTrue();
            
            var individual = getIndividual.Value;
            individual.PhoneNumber.HasValue.Should().BeFalse();
            
            individual.FullName.HasValue.Should().BeFalse();
                      
        }
        
        [Fact]
        public async Task AuthenticatedIndividualProvidesMoreData()
        {
            
            //Arrange
            var testClient = await DatabaseHelpers.CreateRegisteredIndividualAsync(_factory);
            try
            {
                var client = _factory.AuthenticatedInstanceWithClaims([..testClient.Claims]).CreateClient();
                
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
                
                var provideData = await client.PostAsJsonAsync(AuthRoutes.ProvideIndividualClientsDataPath,dto);          
                
                //Assert 
                await HttpResponseAssertions.For(provideData, _output)
                    .ShouldBeSuccess();
                                
                var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, testClient.Email);
                getIndividual.IsSuccess.Should().BeTrue();
                
                var individual = getIndividual.Value;
                
                individual.PhoneNumber.HasValue.Should().BeTrue();
                individual.FullName.HasValue.Should().BeTrue();
                individual.PhoneNumber.Value.Equals(createNumber.Value).Should().BeTrue();
                individual.FullName.Value.Equals(createFullName.Value).Should().BeTrue();
            }
            finally
            {
                await DatabaseHelpers.DeleteIndividual(testClient.Email, _factory);
            }
           
        }
        
        [Fact]
        public async Task AuthenticatedIndividualFetchesUserData()
        {
            
            //Arrange
            var testClient = _clientActor;
            
            var client = _factory.AuthenticatedInstanceWithClaims([..testClient.Claims]).CreateClient();
            
            //Act
            
            var getUser = await client.GetAsync(AuthRoutes.GetUserPath);          
            
            //Assert 
            await HttpResponseAssertions.For(getUser, _output)
                .ShouldBeSuccess();
                            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory, testClient.Email);
            getIndividual.IsSuccess.Should().BeTrue();
            
            var userDto =await getUser.Content.ReadFromJsonAsync<UserDto>();
            userDto.Should().NotBeNull();
            
            userDto!.Email.Should().Be(testClient.Email);
            userDto.Id.Should().Be(testClient.Id);      
        }
        
        [Fact]
        public async Task CantRegisterWithSameEmail()
        {
            //Arrange
            var testClient = _clientActor;
            var client = _factory.CreateClient();
            var dto = new RegisterClientDto(
                testClient.Email,testClient.Password,testClient.Password);
            
            var initCount = DatabaseHelpers.GetCountOfIndividuals(_factory);
            
            //Act
            var register = await client.PostAsJsonAsync(AuthRoutes.RegisterIndividualPath,dto);          
            //Assert 
            
            await HttpResponseAssertions.For(register, _output)
                .ShouldBeStatusCode(
                    ProblemDetailsContract.ValidationStatusCode);
            
            var problemDetails=await register.Content.ReadFromJsonAsync<ProblemDetails>();
            problemDetails.Should().NotBeNull();
            
            var errors = ProblemDetailsTestHelper.GetValidationErrors(problemDetails);
            errors.Should().NotBeNullOrEmpty();

                
            ProblemDetailsTestHelper.ShouldHaveValidationErrorsEquivalentTo(
                errors,
                [ExpectedValidationErrors.EmailIsRegisteredAlready]);
            
            var countAfterAttempt = DatabaseHelpers.GetCountOfIndividuals(_factory);
            countAfterAttempt.Should().Be(initCount);
            
                        
        }
        
  
        
        
        
        
        
        
        
    }
}
