using System.Security.Claims;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using EnergyManagement.Server.L1.Application.Queries;
using EnergyManagement.Server.L1.Application.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.L1.Controllers;

[ApiController]
[Route("api/l1")]
public sealed class L1Controller : ProjectController
{
    private readonly ISender _sender;
    private readonly ILogger<L1Controller> _logger;

    public L1Controller(
        ISender sender,
        ILogger<L1Controller> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpPost("auth/register", Name = "L1RegisterClientAccount")]
    [ProducesResponseType(typeof(L1RegisterClientAccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(
        [FromBody] L1RegisterClientAccountDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sender.Send(
                new L1RegisterClientAccountCommand(dto.Email, dto.Password),
                cancellationToken);

            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 register client account failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [HttpPost("auth/login", Name = "L1LoginClientAccount")]
    [ProducesResponseType(typeof(L1CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(
        [FromBody] L1LoginRequest dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sender.Send(
                new L1LoginClientAccountCommand(dto.Email, dto.Password),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            await SignInL1AccountAsync(result.Value);

            return Ok(ToCurrentUserResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 login failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("auth/current-user", Name = "L1GetCurrentUser")]
    [ProducesResponseType(typeof(L1CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new L1GetCurrentUserQuery(accountId), cancellationToken);
            if (result.IsFailure)
            {
                return Unauthorized();
            }

            return Ok(ToCurrentUserResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 current-user failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("auth/logout", Name = "L1Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return NoContent();
    }

    [Authorize]
    [HttpPost("applicant-parties/individual", Name = "L1CreateIndividualApplicantParty")]
    [ProducesResponseType(typeof(L1CreateIndividualApplicantPartyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateIndividualApplicantParty(
        [FromBody] L1CreateIndividualApplicantPartyDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new L1CreateIndividualApplicantPartyCommand(
                    accountId,
                    dto.FullName.FirstName,
                    dto.FullName.MiddleName,
                    dto.FullName.LastName,
                    dto.Email,
                    dto.PhoneNumber),
                cancellationToken);

            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 create individual applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("applicant-parties/current-individual", Name = "L1GetCurrentIndividualApplicantParty")]
    [ProducesResponseType(typeof(L1CurrentIndividualApplicantPartyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentIndividualApplicantParty(CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new L1GetCurrentIndividualApplicantPartyQuery(accountId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized();
            }

            return Ok(ToCurrentIndividualApplicantPartyResponse(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 get current individual applicant party failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpGet("requests", Name = "L1ListMyRequests")]
    [ProducesResponseType(typeof(IReadOnlyList<L1MyRequestSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListMyRequests(
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new L1ListMyRequestsQuery(accountId, status),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(result.Value.Select(ToMyRequestSummaryDto).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 list my requests failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize]
    [HttpPost("requests", Name = "L1CreateConnectionRequest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateConnectionRequest(
        [FromBody] L1CreateConnectionRequestDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new L1CreateConnectionRequestCommand(
                    accountId,
                    dto.Details,
                    dto.Address.PostalCode,
                    dto.Address.Region,
                    dto.Address.City,
                    dto.Address.Street,
                    dto.Address.House,
                    dto.Address.Building,
                    dto.Address.Apartment),
                cancellationToken);

            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "L1 create connection request failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    private bool TryGetCurrentL1AccountId(out long accountId)
    {
        accountId = default;

        var authModel = User.FindFirstValue(L1AuthClaimTypes.AuthModel);
        if (authModel != L1AuthClaimTypes.AuthModelValue)
        {
            return false;
        }

        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return long.TryParse(claimValue, out accountId);
    }

    private async Task SignInL1AccountAsync(L1LoginClientAccountResponse account)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
            new(ClaimTypes.Email, account.Email),
            new(ClaimTypes.Role, account.Role),
            new(L1AuthClaimTypes.AuthModel, L1AuthClaimTypes.AuthModelValue)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = false });
    }

    private static L1CurrentUserResponse ToCurrentUserResponse(L1LoginClientAccountResponse account)
    {
        return new L1CurrentUserResponse(
            account.AccountId,
            account.Email,
            account.Role,
            account.IsActive,
            IsAuthenticated: true);
    }

    private static L1CurrentUserResponse ToCurrentUserResponse(L1GetCurrentUserResponse account)
    {
        return new L1CurrentUserResponse(
            account.AccountId,
            account.Email,
            account.Role,
            account.IsActive,
            IsAuthenticated: true);
    }

    private static L1CurrentIndividualApplicantPartyResponse ToCurrentIndividualApplicantPartyResponse(
        L1GetCurrentIndividualApplicantPartyResponse response)
    {
        if (!response.Exists || response.ApplicantParty is null)
        {
            return new L1CurrentIndividualApplicantPartyResponse(false, null);
        }

        return new L1CurrentIndividualApplicantPartyResponse(
            true,
            new L1IndividualApplicantPartyDto(
                new L1FullNameDto(
                    response.ApplicantParty.FirstName,
                    response.ApplicantParty.MiddleName,
                    response.ApplicantParty.LastName),
                response.ApplicantParty.Email,
                response.ApplicantParty.PhoneNumber,
                response.ApplicantParty.VerificationStatus));
    }

    private static L1MyRequestSummaryDto ToMyRequestSummaryDto(
        L1MyRequestSummaryResponse request)
    {
        return new L1MyRequestSummaryDto(
            request.RequestId,
            request.RequestType.ToString(),
            request.Status.ToString(),
            request.CreatedAt,
            request.Summary,
            new L1AddressDto(
                request.ObjectAddress.PostalCode,
                request.ObjectAddress.Region,
                request.ObjectAddress.City,
                request.ObjectAddress.Street,
                request.ObjectAddress.House,
                request.ObjectAddress.Building,
                request.ObjectAddress.Apartment));
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
