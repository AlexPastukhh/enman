using System.Security.Claims;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Security;
using FluentValidation;
using MediatR;
using EnergyManagement.Server.L1.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.L1.Controllers;

[ApiController]
[Route("api/agreement-exchanges")]
public sealed class AgreementExchangesController : ProjectController
{
    private readonly ISender _sender;
    private readonly IAgreementExchangeReadService _readService;
    private readonly IValidator<AgreementExchangeListQueryDto> _listQueryValidator;
    private readonly ILogger<AgreementExchangesController> _logger;

    public AgreementExchangesController(
        ISender sender,
        IAgreementExchangeReadService readService,
        IValidator<AgreementExchangeListQueryDto> listQueryValidator,
        ILogger<AgreementExchangesController> logger)
    {
        _sender = sender;
        _readService = readService;
        _listQueryValidator = listQueryValidator;
        _logger = logger;
    }

    [Authorize(Roles = "Client,Employee")]
    [HttpGet(Name = "ListAgreementExchanges")]
    [ProducesResponseType(typeof(AgreementExchangeListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List(
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryDto = new AgreementExchangeListQueryDto(status);
            var validationResult = await _listQueryValidator.ValidateAsync(queryDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

             AgreementExchangeStatus? parsedStatus = string.IsNullOrWhiteSpace(status)
                ? null
                : Enum.Parse<AgreementExchangeStatus>(status);

            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role == "Client")
            {
                var result = await _readService.ListForClientAsync(
                    accountId,
                    parsedStatus,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return ProblemDetailsFromValidation(result.Error);
                }

                return Ok(ToDto(result.Value));
            }

            if (role == "Employee")
            {
                var result = await _readService.ListForEmployeeAsync(
                    accountId,
                    parsedStatus,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return ProblemDetailsFromValidation(result.Error);
                }

                return Ok(ToDto(result.Value));
            }

            return Forbid();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement exchange list failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Client,Employee")]
    [HttpGet("{exchangeId:long:min(1)}", Name = "GetAgreementExchangeDetails")]
    [ProducesResponseType(typeof(AgreementExchangeDetailsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetails(
        long exchangeId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role is not "Client" and not "Employee")
            {
                return Forbid();
            }

            var result = await _sender.Send(
                new AgreementExchangeDetailsQuery(accountId, role, exchangeId),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            if (result.Value.Details is null)
            {
                return NotFound();
            }

            return Ok(ToDto(result.Value.Details));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement exchange details failed for exchange {ExchangeId}.", exchangeId);
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

    private static AgreementExchangeListResponseDto ToDto(AgreementExchangeListResponse response)
    {
        return new AgreementExchangeListResponseDto(
            response.Exchanges.Select(ToDto).ToList());
    }

    private static AgreementExchangeListItemDto ToDto(AgreementExchangeListItemResponse item)
    {
        return new AgreementExchangeListItemDto(
            item.ExchangeId,
            item.RequestId,
            item.ExchangeStatus,
            item.ActiveProposalVersion,
            item.ActiveProposalSender,
            item.ActiveProposalSenderId,
            item.RequestDisplayName,
            item.ObjectAddress,
            item.CreatedAt,
            item.LastActivityAt);
    }

    private static AgreementExchangeDetailsResponseDto ToDto(AgreementExchangeDetailsResponse response)
    {
        return new AgreementExchangeDetailsResponseDto(
            response.ExchangeId,
            response.RequestId,
            response.ExchangeStatus,
            response.ActiveProposalVersion,
            new AgreementExchangeRequestSummaryDto(
                response.Request.RequestId,
                response.Request.RequestStatus,
                response.Request.RequestDisplayName,
                response.Request.ObjectAddress),
            ToDto(response.ActiveProposal),
            response.Proposals.Select(ToDto).ToList(),
            response.CurrentActorSide,
            response.CreatedAt,
            response.LastActivityAt);
    }

    private static AgreementProposalDetailsDto ToDto(AgreementProposalDetailsResponse proposal)
    {
        return new AgreementProposalDetailsDto(
            proposal.ProposalId,
            proposal.Version,
            proposal.Sender,
            proposal.SenderId,
            proposal.State,
            new AgreementDocumentRefDto(
                proposal.Document.StorageKey,
                proposal.Document.OriginalFileName,
                proposal.Document.ContentType,
                proposal.Document.SizeBytes),
            proposal.Comment,
            proposal.CreatedAt);
    }
}
