using CSharpFunctionalExtensions;
using FluentValidation;
using Hospital.proj.Domain.Common;
using Hospital.proj.Server.Application.Commands;
using Hospital.proj.Server.Application.Queryes;
using Hospital.proj.Server.Contracts;
using Hospital.proj.Server.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Hospital.proj.Server.Application.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IValidator<RegisterDto> _regValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<ChangePasswordDto> _changePwdValidator;
        private readonly ISender _sender;
        private readonly HospitalDbContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IDataProtectionProvider _protProvider;
        public AccountController(IValidator<RegisterDto> regValidator,
            ISender sender,
            HospitalDbContext context,
            ILogger<AccountController> logger,
            IValidator<LoginDto> loginValidator,
            IValidator<ChangePasswordDto> changePwdValidator,
            IDataProtectionProvider protProvider)
        {
            _regValidator = regValidator;
            _sender = sender;
            _context = context;
            _logger = logger;
            _loginValidator = loginValidator;
            _changePwdValidator = changePwdValidator;
            _protProvider = protProvider;
        }

        /// <summary>
        /// Registers user and sends activation link on email
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Register(
            [FromBody] RegisterDto dto)
        {
            var validationResult = _regValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BodyValidationProblems(
                    validationResult.Errors);
            }

            var registerCommand = new RegisterCommand(
                dto.Name.FirstName,
                dto.Name.MiddleName,
                dto.Name.LastName,
                dto.Email,
                dto.Password);

            var register = await _sender.Send(
                registerCommand);
            if (register.IsFailure)
            {
                return FromErrorToProblem(register.Error);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.FailedToSaveChanges(ex);
                throw;
            }

            return Created();
        }

        /// <summary>
        /// Activates registered account. Being hit when user click on link in email
        /// </summary>
        /// <param name="code">Security code in link query</param>
        /// <returns></returns>
        [HttpGet("activate/{code}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<ActionResult> ActivateAccountAsync(
            string code)
        {
            var activateAccountCommand = 
                new ActivateAccountCommand(code);

            var result = await _sender.Send(
                activateAccountCommand);
            if (result.IsFailure)
            {
                return FromErrorToProblem(result.Error);
            }


            return Redirect(Routes.AccountActivatedPage);
        }

        /// <summary>
        /// Logins user into account
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login(
            [FromBody] LoginDto login)
        {
            var validationResult = 
                await _loginValidator.ValidateAsync(login);
            if (!validationResult.IsValid)
            {
                return BodyValidationProblems(
                    validationResult.Errors);
            }

            var loginQuery = new LoginQuery(
                login.Email, login.Password);

            var result = await _sender.Send(loginQuery);
            if (result.IsFailure)
            {
                return FromErrorToProblem(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Starts the process of password changing.Sends verification email with link
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("password/change/start")]
        public async Task<ActionResult> StartPasswordChangeAndSendLink()
        {
           
            var startChangeCommand = new StartPasswordChangeCommand();

            var result =await _sender.Send(startChangeCommand);
            if (result.IsFailure)
            {
                return FromErrorToProblem(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Accepts code from query of link ,sent though email, to continue changing of password
        /// </summary>
        /// <param name="code">Code from link query</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("password/change/{code}")]
        public async Task<ActionResult> SubmitPasswordReset(string code)
        {
            if(code is null)
            {
                return MissingRouteValueProblem(nameof(code));
            }

            var command= new SubmitPasswordResetCommand(code);

            var submitReset = await _sender.Send(command);
            if (submitReset.IsFailure)
            {
                return FromErrorToProblem(submitReset.Error);
            }

            return Redirect(Routes.ChangePasswordPage);

        }

        /// <summary>
        /// Changes password
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("password/change")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordDto changePassword)
        {
            var validationResult = await _changePwdValidator.ValidateAsync(
                changePassword);
            if (!validationResult.IsValid)
            {
                return BodyValidationProblems(validationResult.Errors);
            }

            var changePwdCommand = new ChangePasswordCommand(
                changePassword.Password);

            var result = await _sender.Send(changePwdCommand);
            if (result.IsFailure)
            {
                return FromErrorToProblem(result.Error);
            }

            return NoContent();
        }

    }
}
