using FluentAssertions;
using Tests.EnergyManagement.Legacy.TestHelpers;
using Tests.EnergyManagement.TestHelpers;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error.Errors.ClientRequestErrors;

namespace Tests.EnergyManagement.Legacy.Domain
{
    public class ClientRequestTests:ClientRequestUnitBase
    {
        [Theory] 
        [MemberData(nameof(GetValidRequestData))]
        public void CreatesAndAddsIndividualConnectionRequestSuccessfully1(string requestDetails, Address address) 
        { 
            //Arrange 
            var client = ValidTestData.GetIndividualWithoutFullData(); 
            //Act 
            var createRequest = client.CreateConnectionRequest(requestDetails, address); 
            var request = createRequest.Value;
            
            client.AddRequestOrThrow(request);
            
            //Assert
            
            request.Should().BeOfType<IndividualRequest>();
            request.Client.Should().Be(client);
            request.Type.Should().Be(RequestType.Connection); 
            request.RequestDateTime.AddMicroseconds(-1).Should().BeBefore(DateTimeOffset.UtcNow);
            request.RequestDateTime.AddMinutes(5).Should().BeAfter(DateTimeOffset.UtcNow);
            client.ClientRequests.Should().ContainSingle()
                .Which.Should().BeSameAs(request);
        }
        
        
        
        [Theory] 
        [StringTestData(3001,3500)]
        public void CantCreateIndividualConnectionRequestWithTooLongRequestDetails(string requestDetails) 
        { 
            //Arrange 
            var client = ValidTestData.GetIndividualWithoutFullData();
            var clientRequests = client.ClientRequests; 
            var address = ValidTestData.GetAddressWithApartment();
            
            //Act 
            var createRequest = client.CreateConnectionRequest(requestDetails, address); 
            
            //Assert
            
            createRequest.IsFailure.Should().BeTrue();
            createRequest.Error.Contains(ClientRequestTextIsTooLong).Should().BeTrue();

            clientRequests.Should().BeEquivalentTo(client.ClientRequests);
        }
        
        [Theory] 
        [StringTestData(0)]
        public void CantCreateIndividualConnectionRequestWithoutRequestDetails(string requestDetails) 
        { 
            //Arrange 
            var client = ValidTestData.GetIndividualWithoutFullData();
            var clientRequests = client.ClientRequests; 
            var address = ValidTestData.GetAddressWithApartment();
            //Act 
            var createRequest = client.CreateConnectionRequest(requestDetails, address); 
            
            //Assert
            
            createRequest.IsFailure.Should().BeTrue();
            createRequest.Error.Contains(ClientRequestTextIsRequired).Should().BeTrue();

            clientRequests.Should().BeEquivalentTo(client.ClientRequests);
        }
    }
}

