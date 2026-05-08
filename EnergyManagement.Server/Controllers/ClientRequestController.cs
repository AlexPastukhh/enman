using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyManagement.Server.Commands;
using EnergyManagement.Server.Api.Routes;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EnergyManagement.Server.Data;
using Microsoft.AspNetCore.Authorization;

namespace EnergyManagement.Server.Controllers
{
    [ApiController]
    [Route(ClientRequestRoutes.Controller)]
    public class ClientRequestController : ProjectController
    {
        private readonly AppDbContext _context;
        private readonly ISender _sender;
        private readonly IValidator<CreateIndividualRequestDto> _createIndivRequestValidator;
        public ClientRequestController(AppDbContext context, ISender sender, IValidator<CreateIndividualRequestDto> createIndivRequestValidator)
        {
            _context = context;
            _sender = sender;
            _createIndivRequestValidator = createIndivRequestValidator;
        }
        [Authorize]
        [HttpPost(ClientRequestRoutes.IndivCreateConnectionRequest)]
        public async Task<IActionResult> IndivCreateConnectionRequest(CreateIndividualRequestDto dto)
        {
            try
            {

                var validationResult = await _createIndivRequestValidator.ValidateAsync(dto);
                if (validationResult.IsValid is false)
                {
                    return ProblemDetailsFromValidation(validationResult.Errors);
                }

                var clientId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (clientId == null)
                {
                    throw new Exception("Authenticated user does not have a NameIdentifier claim.");
                }

                var command = new CreateIndividualRequest(
                    clientId: long.Parse(clientId),
                    RequestDetails: dto.RequestDetails,
                    PostalCode: dto.Address.PostalCode,
                    Region: dto.Address.Region,
                    City: dto.Address.City,
                    Street: dto.Address.Street,
                    House: dto.Address.House,
                    Building: dto.Address.Building,
                    Apartment: dto.Address.Apartment
                );


                await _sender.Send(command);
                _context.SaveChanges();
                
                return Ok();
            }
            catch (Exception ex)
            {
                return ProblemDetailsWithExceptionDev(ex);
            }

        }
    }
}
