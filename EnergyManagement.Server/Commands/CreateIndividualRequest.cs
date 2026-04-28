using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using MediatR;

namespace EnergyManagement.Server.Commands
{
    public record CreateIndividualRequest(
        long clientId,  
        string RequestDetails,
        string PostalCode,
        string Region,
        string City,
        string Street,
        string House,
        string? Building,
        string? Apartment):IRequest;
    
        


}