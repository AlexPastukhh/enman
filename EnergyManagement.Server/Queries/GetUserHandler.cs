using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Dapper;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Repositories;
using MediatR;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Queries
{
    public class GetUserHandler : IRequestHandler<GetUser, Result<UserDto,Error>>
    {
        private readonly IClientRepository _client;
        private readonly IConfiguration _configuration;
        private readonly IOptions<DbNameOptions> _dbName;
        public GetUserHandler(IClientRepository clientRepository, IConfiguration configuration, IOptions<DbNameOptions> dbName)
        {
            _client = clientRepository;
            _configuration = configuration;
            _dbName = dbName;
        }

        public async Task<Result<UserDto,Error>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString(_dbName.Value.Name));
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