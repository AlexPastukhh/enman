using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Queries
{
    public record GetUser(long userId):IRequest<Result<UserDto,Error>>;
    
    
    
}