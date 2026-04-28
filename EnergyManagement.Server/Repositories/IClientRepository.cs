using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;

namespace EnergyManagement.Server.Repositories
{
    public interface IClientRepository
    {
        public UnitResult<IReadOnlyList<Error>> AddNewIndividualClient(IndividualClient clientToAdd);
        public bool IsClientExist(string email);
        public Task<Result<IndividualClient,Error>> GetClientById(long id);
        public Task<Result<IndividualClient,Error>> GetClientByEmail(string email);
    }
}