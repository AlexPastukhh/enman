using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Repositories;
using MediatR;

namespace EnergyManagement.Server.Commands
{
    public class RegisterIndividualClientHandler : IRequestHandler<RegisterIndividualClient, UnitResult<IReadOnlyList<Error>>>
    {
        private readonly IClientRepository _client;
        public RegisterIndividualClientHandler(IClientRepository clientRepository)
        {
            _client = clientRepository;
            
        }
        public async Task<UnitResult<IReadOnlyList<Error>>> Handle(
            RegisterIndividualClient request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.email).Value;
            var password = Password.Create(request.password).Value;
            var client = IndividualClient.Create(email, password).Value;
            var AddIndividual =_client.AddNewIndividualClient(client);
            if (AddIndividual.IsFailure)
            {
                return await Task.FromResult(UnitResult.Failure<IReadOnlyList<Error>>(AddIndividual.Error));
            }
            return await Task.FromResult(UnitResult.Success<IReadOnlyList<Error>>());
        }
    }
}