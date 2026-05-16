using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Routes;
using EnergyManagement.Server.Data;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.Integration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.EnergyManagement.Legacy.Integration
{
    [Collection("IntegrationTestCollection")]
    public class ClientRequestsTests :  ClientRequestsTestsBase
    {
        public ClientRequestsTests(IntegrationTestFixture fixure, ITestOutputHelper output)
            : base(fixure.Factory, output)
        {
        }
        
        [Fact]
        public async Task UnauthorizedUserCantCreateConnectionRequest(){
            // Arrange
            var client = _factory.CreateClient();

            var requestDto = new CreateIndividualRequestDto(
                ValidTestData.RequestDetails,
                new AddressDto(
                    ValidTestData.PostalCode,
                    ValidTestData.Region,
                    ValidTestData.City,
                    ValidTestData.Street,
                    ValidTestData.House,
                    ValidTestData.Building,
                    ValidTestData.Apartment)
                );
            

            // Act
            var response = await client.PostAsJsonAsync(ClientRequestRoutes.IndivCreateConnectionRequestPath, requestDto);

            // Assert
            await HttpResponseAssertions.For(response).ShouldBeStatusCode(401);
            
        }
        [Theory]
        [MemberData(nameof(GetInvalidConnectionRequestData))]
        public async Task IndivClientCantsCreateConnectionRequestWithInvalidData(CreateIndividualRequestDto requestDto, List<ServerValidationError> expectedErrors){
            // Arrange
            var testClient = await DatabaseHelpers.CreateRegisteredIndividualAsync(_factory);
            try
            {
                var client = _factory.AuthenticatedInstanceWithClaims([..testClient.Claims]).CreateClient();
                
                // Act
                var response = await client.PostAsJsonAsync(ClientRequestRoutes.IndivCreateConnectionRequestPath, requestDto);

                // Assert
                await HttpResponseAssertions.For(response).ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
                
                var problemDetails = await IntegrationTestHelper.GetProblemDetailsAsync(response);
                var actualErrors = IntegrationTestHelper.GetValidationErrors(problemDetails);
                IntegrationTestHelper.ShouldHaveValidationErrorsEquivalentTo(actualErrors, expectedErrors);
                
                var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory,testClient.Email);
                var updatedIndividual = getIndividual.Value;
                
                var lastRequest = updatedIndividual.ClientRequests.LastOrDefault();
                lastRequest.Should().BeNull("No request should have been created for invalid input, but a request was found.");
            }
            finally
            {
                await DatabaseHelpers.DeleteIndividual(testClient.Email, _factory);
            }
            
            
        }
        
        [Fact]
        public async Task IndivClientCreatesConnectionRequestSuccessfully(){
            // Arrange
            var testClient = await DatabaseHelpers.CreateRegisteredIndividualAsync(_factory);
            try
            {
                var client = _factory.AuthenticatedInstanceWithClaims([..testClient.Claims]).CreateClient();

                var requestDto = new CreateIndividualRequestDto(
                    ValidTestData.RequestDetails,
                    new AddressDto(
                        ValidTestData.PostalCode,
                        ValidTestData.Region,
                        ValidTestData.City,
                        ValidTestData.Street,
                        ValidTestData.House,
                        ValidTestData.Building,
                        ValidTestData.Apartment)
                    );
                

                // Act
                var response = await client.PostAsJsonAsync(ClientRequestRoutes.IndivCreateConnectionRequestPath, requestDto);

                // Assert
                await HttpResponseAssertions.For(response).ShouldBeSuccess();
                
                var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory,testClient.Email);
                var updatedIndividual = getIndividual.Value;
                
                var lastRequest = updatedIndividual.ClientRequests.LastOrDefault();
                if(lastRequest == null) throw new XunitException("No requests found for the individual after creating a request.");
                
                RequestAssertions.MatchDto(lastRequest, requestDto).IsSuccess.Should().BeTrue();
            }
            finally
            {
                await DatabaseHelpers.DeleteIndividual(testClient.Email, _factory);
            }
        }
       
        
        
    }
}


