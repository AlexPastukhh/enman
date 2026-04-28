using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EnergyManagement.Server.Commands;
using EnergyManagement.Server.Data;
using EnergyManagement.Server.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnergyManagement.Server.Data;

namespace EnergyManagement.Server.Controllers
{
    [ApiController]
    [Route(SharedConst.AppRoutes.AuthControllerRoute)]
    public class AuthController : ProjectController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IValidator<RegisterClientDto>_registerValidator;
        private readonly IValidator<LoginDto>_loginValidator;
        private readonly IValidator<ProvideIndividualClientsDataDto>_provideIndDataValidator;

        private readonly ISender _sender;
        public AuthController(
            AppDbContext context,
            ISender sender,
            IValidator<RegisterClientDto> registerValidator,
            IValidator<ProvideIndividualClientsDataDto> provideIndDataValidator,
            ILogger<AuthController> logger,
            IValidator<LoginDto> loginValidator)
        {
            _context = context;
            _sender = sender;
            _registerValidator = registerValidator;
            _provideIndDataValidator = provideIndDataValidator;
            _logger = logger;
            _loginValidator = loginValidator;
        }
        [HttpPost(SharedConst.AppRoutes.RegisterIndividual,Name =SharedConst.AppRoutes.RegisterIndividual)]
        public async Task<ActionResult> Register([FromBody]RegisterClientDto dto)
        {   
            try
            {
                var validationResult = await _registerValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return ProblemDetailsFromValidation(validationResult.Errors);
                }
                
                var registerClient =await _sender.Send(new RegisterIndividualClient(dto.Password,dto.Email));
                if (registerClient.IsFailure)
                {
                    return ProblemDetailsFromInternalServerError(registerClient.Error);
                }
                
                await _context.SaveChangesAsync();
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register ERROR");
                return ProblemDetailsWithExceptionDev(ex);
                
            }
            
           
        }
        [Authorize]
        [HttpPost(SharedConst.AppRoutes.ProvideIndividualClientsData)]
        public async Task<ActionResult> ProvideIndividualClientData([FromBody]ProvideIndividualClientsDataDto dto)
        {   
            try
            {
                var validationResult = await _provideIndDataValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return ProblemDetailsFromValidation(validationResult.Errors);
                }
                                
                var provideData =
                    await _sender.Send(
                        new ProvideIndClientData(
                            dto.PhoneNumber,
                            dto.FullNameDto.FirstName,
                            dto.FullNameDto.MiddleName,
                            dto.FullNameDto.LastName));
                if (provideData.IsFailure)
                {
                    return ProblemDetailsFromInternalServerError(provideData.Error);
                }
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProvideIndividualClientData)} ERROR");
                return ProblemDetailsWithExceptionDev(ex);
                throw;
            }
            
            return Ok();
        }
        [HttpPost(SharedConst.AppRoutes.Login)]
        public async Task<ActionResult> Login([FromBody]LoginDto dto)
        {   
            try
            {
                var validationResult = await _loginValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return ProblemDetailsFromValidation(validationResult.Errors);
                }
                                
                var signInClient =await _sender.Send(new SignInClient(dto.Email,dto.Password));
                if (signInClient.IsFailure)
                {
                    return ProblemDetailsFromValidation(signInClient.Error);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ProvideIndividualClientData)} ERROR");
                return ProblemDetailsWithExceptionDev(ex);
                
            }
            
            return Ok();
        }
        [Authorize]
        [HttpGet(SharedConst.AppRoutes.GetUser)]
        public async Task<ActionResult>GetUser()
        {
            try
            {
                var userId = Int64.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            
                var getUserResult = await _sender.Send(new GetUser(userId));
                if(getUserResult.IsFailure)
                {
                    return ProblemDetailsFromInternalServerError(getUserResult.Error);
                }
                
                return Ok(getUserResult.Value);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetUser)} ERROR");
                return ProblemDetailsWithExceptionDev(ex);
            }
            
        }
        
        
        
    }
}