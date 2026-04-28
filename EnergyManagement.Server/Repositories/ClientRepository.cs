using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Microsoft.EntityFrameworkCore;
using static Domain.EnergyManagement.Common.Error.Errors;

namespace EnergyManagement.Server.Repositories
{
    public class ClientRepository:IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public bool IsClientExist(string email)
            => _context.Set<IndividualClient>().Any(x => x.Email.Value == email);
        public UnitResult<IReadOnlyList<Error>> AddNewIndividualClient(IndividualClient clientToAdd)
        {
            Guard.IsNotNull(clientToAdd);
            
            var errors = new List<Error>();
            
            var isExistWithEmail=IsClientExist(clientToAdd.Email.Value);
            if(isExistWithEmail)
            {
                errors.Add(Account.EmailIsRegisteredAlready);
            }
            
            if(errors.Any())
            {
                return UnitResult.Failure<IReadOnlyList<Error>>(errors);
            }
            
            _context.Attach(clientToAdd);
            return UnitResult.Success<IReadOnlyList<Error>>();
        }
        
        public async Task<Result<IndividualClient,Error>> GetClientById(long id)
        {
            Guard.IsNotNull(id);
            Guard.IsGreaterThan(id, 0);
            
            var client = await _context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return Result.Failure<IndividualClient,Error>(ClientErrors.ClientNotFound);
            }
            
            return Result.Success<IndividualClient, Error>(client);
            
        }
       

        public async Task<Result<IndividualClient, Error>> GetClientByEmail(string email)
        {
            Guard.IsNotNull(email);
            
            var client = await _context.Set<IndividualClient>().FirstOrDefaultAsync(x => x.Email.Value == email);
            if(client is null)
            {
                return Result.Failure<IndividualClient, Error>(ClientErrors.ClientNotFound);
            }
            else
            {
                return Result.Success<IndividualClient, Error>(client);
            }
        }
    }
}