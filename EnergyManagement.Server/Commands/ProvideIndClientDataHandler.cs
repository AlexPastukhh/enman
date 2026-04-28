using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EnergyManagement.Server.Commands
{
    public class ProvideIndClientDataHandler : IRequestHandler<ProvideIndClientData, UnitResult<Error>>
    {
        private readonly IClientRepository _client;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ProvideIndClientDataHandler> _logger;
        public ProvideIndClientDataHandler(IClientRepository clientRepository, IHttpContextAccessor contextAccessor, ILogger<ProvideIndClientDataHandler> logger)
        {
            _client = clientRepository;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }
        public async Task<UnitResult<Error>> Handle(
            ProvideIndClientData request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var userId = GetAuthenticatedClientId();
                var getClient = await _client.GetClientById(userId);
                if(getClient.IsFailure)
                {
                    return UnitResult.Failure<Error>(getClient.Error);
                }
                var phone = PhoneNumber.Create(request.phone).Value;
                var fullName = FullName.Create(request.firstName, request.middleName, request.lastName).Value;
                getClient.Value.ProvideDataOrThrow(phone, fullName);
                
                return UnitResult.Success<Error>();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(nameof(ProvideIndClientData));
                _logger.LogError(ex.Message);
               
                throw;
            }
            
            
        }
        
        public long GetAuthenticatedClientId()
        {
            try
            {
                return long.Parse(_contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex, "User Possible unauthorized");
                throw;
            }
        }
    }
}