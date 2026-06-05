using System.Security.Claims;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Api.Validation;
using EnergyManagement.Server.Application.Commands;
using EnergyManagement.Server.Application.Queries;
using EnergyManagement.Server.Application.Security;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Controllers;

[ApiController]
[Route("api")]
public sealed class AppController : ProjectController
{
    private readonly ISender _sender;
    private readonly ILogger<AppController> _logger;
    private readonly IValidator<RegisterClientAccountDto> _registerValidator;
    private readonly IValidator<LoginRequestDto> _loginValidator;
    private readonly IValidator<CreateIndividualApplicantPartyDto> _createIndividualApplicantPartyValidator;
    private readonly IValidator<CreateIndividualEntrepreneurApplicantPartyDto> _createIndividualEntrepreneurApplicantPartyValidator;
    private readonly IValidator<CreateLegalEntityApplicantPartyDto> _createLegalEntityApplicantPartyValidator;
    private readonly IValidator<CreateConnectionRequestDto> _createConnectionRequestValidator;
    private readonly IValidator<ListMyRequestsQueryDto> _listMyRequestsQueryValidator;
    private readonly ClaimsPrincipalFactory _claimsPrincipalFactory;

    public AppController(
        ISender sender,
        ILogger<AppController> logger,
        IValidator<RegisterClientAccountDto> registerValidator,
        IValidator<LoginRequestDto> loginValidator,
        IValidator<CreateIndividualApplicantPartyDto> createIndividualApplicantPartyValidator,
        IValidator<CreateIndividualEntrepreneurApplicantPartyDto> createIndividualEntrepreneurApplicantPartyValidator,
        IValidator<CreateLegalEntityApplicantPartyDto> createLegalEntityApplicantPartyValidator,
        IValidator<CreateConnectionRequestDto> createConnectionRequestValidator,
        IValidator<ListMyRequestsQueryDto> listMyRequestsQueryValidator,
        ClaimsPrincipalFactory claimsPrincipalFactory)
    {
        _sender = sender;
        _logger = logger;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _createIndividualApplicantPartyValidator = createIndividualApplicantPartyValidator;
        _createIndividualEntrepreneurApplicantPartyValidator = createIndividualEntrepreneurApplicantPartyValidator;
        _createLegalEntityApplicantPartyValidator = createLegalEntityApplicantPartyValidator;
        _createConnectionRequestValidator = createConnectionRequestValidator;
        _listMyRequestsQueryValidator = listMyRequestsQueryValidator;
        _claimsPrincipalFactory = claimsPrincipalFactory;
    }

    [HttpPost("auth/register", Name = "RegisterClientAccount")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(RegisterClientAccountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterClientAccountDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(dto, _registerValidator, cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            var result = await _sender.Send(
                new RegisterClientAccountCommand(dto!.Email!, dto.Password!),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(new RegisterClientAccountResponseDto(
                result.Value.AccountId,
                result.Value.Email));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Register client account failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [HttpPost("auth/login", Name = "LoginClientAccount")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CurrentUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(dto, _loginValidator, cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            var result = await _sender.Send(
                new LoginClientAccountCommand(dto!.Email!, dto.Password!),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            await SignInAccountAsync(result.Value);

            return Ok(ToCurrentUserResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("auth/current-user", Name = "GetCurrentUser")]
    [ProducesResponseType(typeof(CurrentUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new GetCurrentUserQuery(accountId), cancellationToken);
            if (result.IsFailure)
            {
                return Unauthorized();
            }

            return Ok(ToCurrentUserResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Current-user failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("auth/logout", Name = "Logout")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    [Authorize]
    [HttpPost("applicant-parties/individual", Name = "CreateIndividualApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateIndividualApplicantPartyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateIndividualApplicantParty(
        [FromBody] CreateIndividualApplicantPartyDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(
                dto,
                _createIndividualApplicantPartyValidator,
                cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new CreateIndividualApplicantPartyCommand(
                    accountId,
                    dto!.FullName!.FirstName!,
                    dto.FullName.MiddleName!,
                    dto.FullName.LastName!,
                    dto.Email!,
                    dto.PhoneNumber!),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(new CreateIndividualApplicantPartyResponseDto(
                result.Value.ApplicantPartyId,
                result.Value.ClientAccountId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create individual applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("applicant-parties/individual-entrepreneur", Name = "CreateIndividualEntrepreneurApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateIndividualApplicantPartyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateIndividualEntrepreneurApplicantParty(
        [FromBody] CreateIndividualEntrepreneurApplicantPartyDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(
                dto,
                _createIndividualEntrepreneurApplicantPartyValidator,
                cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new CreateIndividualEntrepreneurApplicantPartyCommand(
                    accountId,
                    dto!.FullName!.FirstName!,
                    dto.FullName.MiddleName!,
                    dto.FullName.LastName!,
                    dto.Inn!,
                    dto.Ogrnip!,
                    dto.Email!,
                    dto.PhoneNumber!),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(new CreateIndividualApplicantPartyResponseDto(
                result.Value.ApplicantPartyId,
                result.Value.ClientAccountId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create individual entrepreneur applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("applicant-parties/legal-entity", Name = "CreateLegalEntityApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateIndividualApplicantPartyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateLegalEntityApplicantParty(
        [FromBody] CreateLegalEntityApplicantPartyDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(
                dto,
                _createLegalEntityApplicantPartyValidator,
                cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new CreateLegalEntityApplicantPartyCommand(
                    accountId,
                    dto!.OrganizationName!,
                    dto.Inn!,
                    dto.Kpp!,
                    dto.Ogrn!,
                    dto.Email!,
                    dto.PhoneNumber!),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(new CreateIndividualApplicantPartyResponseDto(
                result.Value.ApplicantPartyId,
                result.Value.ClientAccountId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create legal entity applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize]
    [HttpGet("applicant-parties", Name = "GetAccountApplicantParties")]
    [ProducesResponseType(typeof(AccountApplicantPartiesResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListAccountApplicantParties(CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new GetAccountApplicantPartiesQuery(accountId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized();
            }

            return Ok(ToAccountApplicantPartiesResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List account applicant parties failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("applicant-parties/{applicantPartyId:long}/make-current-default", Name = "MakeApplicantPartyCurrentDefault")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> MakeApplicantPartyCurrentDefault(
        long applicantPartyId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new MakeApplicantPartyCurrentDefaultCommand(accountId, applicantPartyId),
                cancellationToken);

            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Make applicant party current/default failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("applicant-parties/current-individual", Name = "GetCurrentIndividualApplicantParty")]
    [ProducesResponseType(typeof(CurrentIndividualApplicantPartyResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentIndividualApplicantParty(CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new GetCurrentIndividualApplicantPartyQuery(accountId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized();
            }

            return Ok(ToCurrentIndividualApplicantPartyResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get current individual applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("requests", Name = "ListMyRequests")]
    [ProducesResponseType(typeof(IReadOnlyList<MyRequestSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListMyRequests(
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateAsync(
                new ListMyRequestsQueryDto(status),
                _listMyRequestsQueryValidator,
                cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new ListMyRequestsQuery(accountId, status),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(result.Value.Select(ToMyRequestSummaryDto).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "List my requests failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("requests/{requestId:long}", Name = "GetMyRequestDetails")]
    [ProducesResponseType(typeof(MyRequestDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMyRequestDetails(
        long requestId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new GetMyRequestDetailsQuery(accountId, requestId),
                cancellationToken);

            if (result.HasNoValue)
            {
                return NotFound();
            }

            return Ok(ToMyRequestDetailsDto(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get my request details failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("requests", Name = "CreateConnectionRequest")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateConnectionRequest(
        [FromBody] CreateConnectionRequestDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationProblem = await ValidateBodyAsync(
                dto,
                _createConnectionRequestValidator,
                cancellationToken);
            if (validationProblem is not null)
            {
                return validationProblem;
            }

            if (!TryGetCurrentAccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new CreateConnectionRequestCommand(
                    accountId,
                    dto!.ApplicantContextType!,
                    dto.ExistingApplicantPartyId,
                    dto.NewApplicantParty is null
                        ? null
                        : new CreateConnectionRequestNewApplicant(
                            dto.NewApplicantParty.ApplicantPartyType!,
                            dto.NewApplicantParty.FullName?.FirstName,
                            dto.NewApplicantParty.FullName?.MiddleName,
                            dto.NewApplicantParty.FullName?.LastName,
                            dto.NewApplicantParty.OrganizationName,
                            dto.NewApplicantParty.Inn,
                            dto.NewApplicantParty.Kpp,
                            dto.NewApplicantParty.Ogrn,
                            dto.NewApplicantParty.Ogrnip,
                            dto.NewApplicantParty.Email!,
                            dto.NewApplicantParty.PhoneNumber!),
                    dto.Details!,
                    dto.Address!.PostalCode!,
                    dto.Address.Region!,
                    dto.Address.City!,
                    dto.Address.Street!,
                    dto.Address.House!,
                    dto.Address.Building,
                    dto.Address.Apartment),
                cancellationToken);

            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Create connection request failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    private bool TryGetCurrentAccountId(out long accountId)
    {
        accountId = default;

        var authModel = User.FindFirstValue(AuthClaimTypes.AuthModel);
        if (authModel != AuthClaimTypes.AuthModelValue)
        {
            return false;
        }

        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return long.TryParse(claimValue, out accountId);
    }

    private async Task<ActionResult?> ValidateBodyAsync<TDto>(
        TDto? dto,
        IValidator<TDto> validator,
        CancellationToken cancellationToken)
        where TDto : class
    {
        if (dto is null)
        {
            return ProblemDetailsFromValidation(
                new[]
                {
                    new ValidationFailure(
                        "body",
                        Error.Errors.General.RequestBodyIsNull.Code)
                });
        }

        return await ValidateAsync(dto, validator, cancellationToken);
    }

    private async Task<ActionResult?> ValidateAsync<TDto>(
        TDto dto,
        IValidator<TDto> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ProblemDetailsFromValidation(validationResult.Errors);
        }

        return null;
    }

    private async Task SignInAccountAsync(LoginClientAccountResponse account)
    {
        var principal = _claimsPrincipalFactory.CreatePrincipal(account.Account);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = false });
    }

    private static CurrentUserResponseDto ToCurrentUserResponse(LoginClientAccountResponse account)
    {
        return new CurrentUserResponseDto(
            account.AccountId,
            account.Email,
            account.Role,
            account.IsActive,
            IsAuthenticated: true);
    }

    private static CurrentUserResponseDto ToCurrentUserResponse(GetCurrentUserResponse account)
    {
        return new CurrentUserResponseDto(
            account.AccountId,
            account.Email,
            account.Role,
            account.IsActive,
            IsAuthenticated: true);
    }


    private static AccountApplicantPartiesResponseDto ToAccountApplicantPartiesResponse(
        GetAccountApplicantPartiesResponse response)
    {
        return new AccountApplicantPartiesResponseDto(
            response.ApplicantParties.Select(ToApplicantPartySummaryDto).ToList());
    }

    private static ApplicantPartySummaryDto ToApplicantPartySummaryDto(
        ApplicantPartySummaryResponse applicantParty)
    {
        return new ApplicantPartySummaryDto(
            applicantParty.ApplicantPartyId,
            applicantParty.ApplicantPartyType.ToString(),
            applicantParty.DisplayName,
            applicantParty.FullName is null
                ? null
                : new FullNameDto(
                    applicantParty.FullName.FirstName,
                    applicantParty.FullName.MiddleName,
                    applicantParty.FullName.LastName),
            applicantParty.OrganizationName,
            applicantParty.Inn,
            applicantParty.Kpp,
            applicantParty.Ogrn,
            applicantParty.Ogrnip,
            applicantParty.Email,
            applicantParty.PhoneNumber,
            applicantParty.VerificationStatus.ToString(),
            applicantParty.IsCurrentDefault,
            applicantParty.CreatedAt);
    }

    private static CurrentIndividualApplicantPartyResponseDto ToCurrentIndividualApplicantPartyResponse(
        GetCurrentIndividualApplicantPartyResponse response)
    {
        if (!response.Exists || response.ApplicantParty is null)
        {
            return new CurrentIndividualApplicantPartyResponseDto(false, null);
        }

        return new CurrentIndividualApplicantPartyResponseDto(
            true,
            new IndividualApplicantPartyDto(
                new FullNameDto(
                    response.ApplicantParty.FirstName,
                    response.ApplicantParty.MiddleName,
                    response.ApplicantParty.LastName),
                response.ApplicantParty.Email,
                response.ApplicantParty.PhoneNumber,
                response.ApplicantParty.VerificationStatus));
    }

    private static MyRequestSummaryDto ToMyRequestSummaryDto(
        MyRequestSummaryResponse request)
    {
        return new MyRequestSummaryDto(
            request.RequestId,
            request.RequestType.ToString(),
            request.Status.ToString(),
            request.CreatedAt,
            request.Summary,
            new AddressDto(
                request.ObjectAddress.PostalCode,
                request.ObjectAddress.Region,
                request.ObjectAddress.City,
                request.ObjectAddress.Street,
                request.ObjectAddress.House,
                request.ObjectAddress.Building,
                request.ObjectAddress.Apartment));
    }

    private static MyRequestDetailsDto ToMyRequestDetailsDto(
        MyRequestDetailsResponse request)
    {
        return new MyRequestDetailsDto(
            request.RequestId,
            request.RequestType.ToString(),
            request.Status.ToString(),
            request.CreatedAt,
            new SubmittedRequestDto(
                request.SubmittedRequest.Details,
                new AddressDto(
                    request.SubmittedRequest.ObjectAddress.PostalCode,
                    request.SubmittedRequest.ObjectAddress.Region,
                    request.SubmittedRequest.ObjectAddress.City,
                    request.SubmittedRequest.ObjectAddress.Street,
                    request.SubmittedRequest.ObjectAddress.House,
                    request.SubmittedRequest.ObjectAddress.Building,
                    request.SubmittedRequest.ObjectAddress.Apartment)),
            request.ReviewResult is null
                ? null
                : new MyRequestReviewResultDto(
                    request.ReviewResult.Decision.ToString(),
                    request.ReviewResult.DecidedAt,
                    request.ReviewResult.Rejection is null
                        ? null
                        : new MyRequestRejectionDto(
                            request.ReviewResult.Rejection.Reason)));
    }

    private ActionResult ToActionResult<TValue>(
        Result<TValue, IReadOnlyList<Error>> result)
    {
        if (result.IsFailure)
        {
            return ProblemDetailsFromValidation(result.Error);
        }

        return Ok(result.Value);
    }

    private ActionResult ToActionResult(UnitResult<IReadOnlyList<Error>> result)
    {
        if (result.IsFailure)
        {
            return ProblemDetailsFromValidation(result.Error);
        }

        return Ok();
    }
}
