using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Commands
{
    public record ProvideIndClientData
        (string phone,
        string firstName,
        string middleName,
        string lastName):IRequest<UnitResult<Error>>;
    
}