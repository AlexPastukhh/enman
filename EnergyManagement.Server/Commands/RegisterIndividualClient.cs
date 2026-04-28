using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Commands
{
    public record RegisterIndividualClient(
        string password,
        string email)
        :IRequest<UnitResult<IReadOnlyList<Error>>>;
    
    
}