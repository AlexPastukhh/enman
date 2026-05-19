using System.Security.Claims;
using Domain.EnergyManagement;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Application.Security;
using FluentValidation;
using MediatR;
using EnergyManagement.Server.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Controllers;

[ApiController]
[Route("api/agreement-exchanges")]
public sealed class AgreementExchangesController : ProjectController
{
    private readonly ISender _sender;
    private readonly IAgreementExchangeReadService _readService;
    private readonly IAgreementExchangeApplicationService _applicationService;
    private readonly IDocumentStorage _documentStorage;
    private readonly IValidator<AgreementExchangeListQueryDto> _listQueryValidator;
    private readonly IValidator<SendAgreementProposalVersionDto> _sendProposalValidator;
    private readonly IValidator<StartAgreementExchangeDto> _startExchangeValidator;
    private readonly IValidator<FinalRefuseAgreementExchangeDto> _finalRefuseValidator;
    private readonly ILogger<AgreementExchangesController> _logger;

    public AgreementExchangesController(
        ISender sender,
        IAgreementExchangeReadService readService,
        IAgreementExchangeApplicationService applicationService,
        IDocumentStorage documentStorage,
        IValidator<AgreementExchangeListQueryDto> listQueryValidator,
        IValidator<SendAgreementProposalVersionDto> sendProposalValidator,
        IValidator<StartAgreementExchangeDto> startExchangeValidator,
        IValidator<FinalRefuseAgreementExchangeDto> finalRefuseValidator,
        ILogger<AgreementExchangesController> logger)
    {
        _sender = sender;
        _readService = readService;
        _applicationService = applicationService;
        _documentStorage = documentStorage;
        _listQueryValidator = listQueryValidator;
        _sendProposalValidator = sendProposalValidator;
        _startExchangeValidator = startExchangeValidator;
        _finalRefuseValidator = finalRefuseValidator;
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


    [Authorize(Roles = "Client,Employee")]
    [HttpGet("{exchangeId:long:min(1)}/proposals/{proposalId:long:min(1)}/document/download", Name = "DownloadAgreementProposalDocument")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadProposalDocument(
        long exchangeId,
        long proposalId,
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

            var details = result.Value.Details;
            if (details is null)
            {
                return NotFound();
            }

            var proposal = details.Proposals.FirstOrDefault(x => x.ProposalId == proposalId);
            proposal ??= details.ActiveProposal.ProposalId == proposalId
                ? details.ActiveProposal
                : null;

            if (proposal is null)
            {
                return NotFound();
            }

            var document = proposal.Document;
            Stream stream;
            try
            {
                stream = await _documentStorage.OpenReadAsync(
                    document.StorageKey,
                    cancellationToken);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound();
            }

            var contentType = string.IsNullOrWhiteSpace(document.ContentType)
                ? "application/octet-stream"
                : document.ContentType;
            var fileName = string.IsNullOrWhiteSpace(document.OriginalFileName)
                ? $"agreement-proposal-{proposal.ProposalId}"
                : document.OriginalFileName;

            return File(
                stream,
                contentType,
                fileName,
                enableRangeProcessing: true);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Agreement proposal document download failed for exchange {ExchangeId}, proposal {ProposalId}.",
                exchangeId,
                proposalId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize(Roles = "Client")]
    [RequireAntiforgeryToken]
    [HttpPost("{exchangeId:long:min(1)}/accept", Name = "ClientAcceptActiveAgreementProposal")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AcceptActiveProposal(
        long exchangeId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _applicationService.ClientAcceptActiveProposalAsync(
                accountId,
                exchangeId,
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement proposal accept failed for exchange {ExchangeId}.", exchangeId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("{exchangeId:long:min(1)}/final-refuse", Name = "EmployeeFinalRefuseAgreementExchange")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FinalRefuse(
        long exchangeId,
        [FromBody] FinalRefuseAgreementExchangeDto? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (dto is not null)
            {
                var validationResult = await _finalRefuseValidator.ValidateAsync(dto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ProblemDetailsFromValidation(validationResult.Errors);
                }
            }

            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _applicationService.EmployeeFinalRefuseAgreementExchangeAsync(
                accountId,
                exchangeId,
                dto?.Reason,
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement exchange final refusal failed for exchange {ExchangeId}.", exchangeId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("/api/employee/requests/{requestId:long:min(1)}/agreement-exchange/start", Name = "EmployeeStartAgreementExchange")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartExchange(
        long requestId,
        [FromBody] StartAgreementExchangeDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _startExchangeValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var result = await _applicationService.StartAgreementExchangeByEmployeeAsync(
                accountId,
                requestId,
                ToInput(dto.Document!),
                dto.Comment,
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement exchange start failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Client,Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("/api/requests/{requestId:long:min(1)}/agreement-exchange/proposals", Name = "SendAgreementProposalVersion")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendProposal(
        long requestId,
        [FromBody] SendAgreementProposalVersionDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _sendProposalValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            if (!TryGetCurrentL1AccountId(out var accountId))
            {
                return Unauthorized();
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role == "Client")
            {
                var result = await _applicationService.SendClientProposalVersionAsync(
                    accountId,
                    requestId,
                    ToInput(dto.Document!),
                    dto.Comment,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return ProblemDetailsFromValidation(result.Error);
                }

                return NoContent();
            }

            if (role == "Employee")
            {
                var result = await _applicationService.SendEmployeeProposalVersionAsync(
                    accountId,
                    requestId,
                    ToInput(dto.Document!),
                    dto.Comment,
                    cancellationToken);

                if (result.IsFailure)
                {
                    return ProblemDetailsFromValidation(result.Error);
                }

                return NoContent();
            }

            return Forbid();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Agreement proposal version send failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    private static AgreementDocumentRefInput ToInput(AgreementDocumentRefDto dto)
    {
        return new AgreementDocumentRefInput(
            dto.StorageKey,
            dto.OriginalFileName,
            dto.ContentType,
            dto.SizeBytes);
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
