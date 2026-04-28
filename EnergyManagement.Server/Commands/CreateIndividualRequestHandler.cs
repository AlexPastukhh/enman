using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Repositories;
using MediatR;
using static Domain.EnergyManagement.Common.Error.Errors;

namespace EnergyManagement.Server.Commands
{
    public class CreateIndividualRequestHandler : IRequestHandler<CreateIndividualRequest>
    {
        private readonly IClientRepository _clientRepository;
        public CreateIndividualRequestHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(CreateIndividualRequest command, CancellationToken cancellationToken)
        {
            var getClientResult = await _clientRepository.GetClientById(command.clientId);
            var client = getClientResult.Value;
            
            var createAddress = Address.Create(
                command.PostalCode,
                command.Region,
                command.City,
                command.Street,
                command.House,
                command.Building,
                command.Apartment);
            var address = createAddress.Value;
             
            var createRequest = client.CreateConnectionRequest(
                command.RequestDetails,
                address);
            var request = createRequest.Value;
            
            client.AddRequestOrThrow(request);
  
        }
        
    }
}