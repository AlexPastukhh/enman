using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dapper;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Configuration;
using EnergyManagement.Server.Data;
using EnergyManagement.Server.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;

namespace EnergyManagement.Server.Queries
{
    public class GetUserHandler : IRequestHandler<GetUser, Result<UserDto,Error>>
    {
        private readonly IClientRepository _client;
        private readonly IConfiguration _configuration;
        public GetUserHandler(IClientRepository clientRepository, IConfiguration configuration)
        {
            _client = clientRepository;
            _configuration = configuration;
        }

        public async Task<Result<UserDto,Error>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(
                _configuration.GetConnectionString(ConnectionStringNames.ManagementDb));
            var sql = @"SELECT c.ClientId, c.Email as Email
                        FROM Clients c
                        WHERE c.ClientId = @ClientId";
            
            var row = await connection.QueryFirstOrDefaultAsync(sql, new { ClientId = request.userId });
            if(row is null)
            {
                throw new Exception($"Authenticated User was not found with id {request.userId}");
            }
            
            var userDto = new UserDto(request.userId, row.Email);
            
            return Result.Success<UserDto, Error>(userDto);
            
        }
    }
}
