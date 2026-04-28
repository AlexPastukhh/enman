using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server;
using EnergyManagement.Server.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Storage;
using Tests.EnergyManagement.TestHelpers;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;
using Xunit.Sdk;

namespace Tests.EnergyManagement.Integration
{
    [Order(2)]
    public class ClientRequestsTests :  ClientRequestsTestsBase, IClassFixture<WebAppFactory>
    {
        private RequestsTestIndividual _testClient = new RequestsTestIndividual();
        public ClientRequestsTests(WebAppFactory factory, ITestOutputHelper output)
            : base(factory,output)
        {
        }

        [Fact,Order(1)]
        public void ClearDatabaseBeforeTests()
        {
            ClearDatabase();
        }
        
        [Fact,Order(2)]
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
            var response = await client.PostAsJsonAsync("/api/ClientRequest/IndivCreateConnectionRequest", requestDto);

            // Assert
            await HttpResponseAssertions.For(response).ShouldBeStatusCode(401);
            
        }
        [Theory,Order(4)]
        [MemberData(nameof(GetInvalidConnectionRequestData))]
        public async Task IndivClientCantsCreateConnectionRequestWithInvalidData(CreateIndividualRequestDto requestDto, List<ServerValidationError> expectedErrors){
            // Arrange
            var (testIndividual, password) = ValidTestData.GetIndividualWithAllDataAndPassword();
            await DatabaseHelpers.AddIndividual(_factory,testIndividual.Email.Value,password);
            var addedIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory,testIndividual.Email.Value);
            
            _testClient.SetRegisteredIndividual(addedIndividual.Value,password,_output);

            var claims = _testClient.GetClaims();
            var client = _factory.AuthenticatedInstanceWithClaims([..claims]).CreateClient();
            
            // Act
            var response = await client.PostAsJsonAsync("/api/ClientRequest/IndivCreateConnectionRequest", requestDto);

            // Assert
            await HttpResponseAssertions.For(response).ShouldBeStatusCode(SharedConst.GeneralConstants.ValidationErrorStatusCode);
            
            var problemDetails = await IntegrationTestHelper.GetProblemDetailsAsync(response);
            var actualErrors = IntegrationTestHelper.GetValidationErrors(problemDetails);
            var allErrorsContained=expectedErrors.All(e=>actualErrors.Contains(e));
            if(!allErrorsContained)
            {
                LogTwoErrorCollections(expectedErrors,actualErrors);
            }
            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory,testIndividual.Email.Value);
            var updatedIndividual = getIndividual.Value;
            _testClient.UpdateRegisteredIndividual(updatedIndividual);
            
            var lastRequest = _testClient.Requests.Value.LastOrDefault();
            lastRequest.Should().BeNull("No request should have been created for invalid input, but a request was found.");
            
            
        }
        
        [Fact,Order(3)]
        public async Task IndivClientCreatesConnectionRequestSuccessfully(){
            // Arrange
            

            var claims = _testClient.GetClaims();
            var client = _factory.AuthenticatedInstanceWithClaims([..claims]).CreateClient();

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
            var response = await client.PostAsJsonAsync("/api/ClientRequest/IndivCreateConnectionRequest", requestDto);

            // Assert
            await HttpResponseAssertions.For(response).ShouldBeSuccess();
            
            var getIndividual = await DatabaseHelpers.GetIndividualByEmailAsync(_factory,_testClient.EmailOrThrow);
            var updatedIndividual = getIndividual.Value;
            _testClient.UpdateRegisteredIndividual(updatedIndividual);
            
            var lastRequest = _testClient.Requests.Value.LastOrDefault();
            if(lastRequest == null) throw new XunitException("No requests found for the individual after creating a request.");
            
            _testClient.AssertRequestFromDto(lastRequest, requestDto).IsSuccess.Should().BeTrue();
        }
       
        
        
    }
}