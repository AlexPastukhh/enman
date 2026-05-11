using System.Security.Claims;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using MediatR;
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

    [HttpPost("auth/register")]
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

    [Authorize]
    [HttpPost("applicant-parties/individual")]
    public async Task<IActionResult> CreateIndividualApplicantParty(
        [FromBody] L1CreateIndividualApplicantPartyDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var accountId = GetCurrentAccountId();
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
    [HttpPost("requests")]
    public async Task<IActionResult> CreateConnectionRequest(
        [FromBody] L1CreateConnectionRequestDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var accountId = GetCurrentAccountId();
            var result = await _sender.Send(
                new L1CreateConnectionRequestCommand(
                    accountId,
                    dto.ApplicantPartyId,
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

    private long GetCurrentAccountId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(claimValue, out var accountId))
        {
            throw new InvalidOperationException("Authenticated user does not have a valid NameIdentifier claim.");
        }

        return accountId;
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
}
