using System.Security.Claims;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Application.Security;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using EnergyManagement.Server.Application.Queries;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnergyManagement.Server.Controllers;

[ApiController]
[Route("api/applicant-parties")]
public sealed class ApplicantPartiesController : ProjectController
{
    private readonly ISender _sender;
    private readonly ILogger<ApplicantPartiesController> _logger;
    private readonly IValidator<CreateIndividualApplicantPartyDto> _createIndividualApplicantPartyValidator;
    private readonly IValidator<CreateIndividualEntrepreneurApplicantPartyDto> _createIndividualEntrepreneurApplicantPartyValidator;
    private readonly IValidator<CreateLegalEntityApplicantPartyDto> _createLegalEntityApplicantPartyValidator;

    public ApplicantPartiesController(
        ISender sender,
        ILogger<ApplicantPartiesController> logger,
        IValidator<CreateIndividualApplicantPartyDto> createIndividualApplicantPartyValidator,
        IValidator<CreateIndividualEntrepreneurApplicantPartyDto> createIndividualEntrepreneurApplicantPartyValidator,
        IValidator<CreateLegalEntityApplicantPartyDto> createLegalEntityApplicantPartyValidator)
    {
        _sender = sender;
        _logger = logger;
        _createIndividualApplicantPartyValidator = createIndividualApplicantPartyValidator;
        _createIndividualEntrepreneurApplicantPartyValidator = createIndividualEntrepreneurApplicantPartyValidator;
        _createLegalEntityApplicantPartyValidator = createLegalEntityApplicantPartyValidator;
    }

    [Authorize]
    [HttpPost("individual", Name = "CreateIndividualApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateApplicantPartyResponseDto), StatusCodes.Status200OK)]
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

            return Ok(new CreateApplicantPartyResponseDto(
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
    [HttpPost("individual-entrepreneur", Name = "CreateIndividualEntrepreneurApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateApplicantPartyResponseDto), StatusCodes.Status200OK)]
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

            return Ok(new CreateApplicantPartyResponseDto(
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
    [HttpPost("legal-entity", Name = "CreateLegalEntityApplicantParty")]
    [RequireAntiforgeryToken]
    [ProducesResponseType(typeof(CreateApplicantPartyResponseDto), StatusCodes.Status200OK)]
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

            return Ok(new CreateApplicantPartyResponseDto(
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
    [HttpGet(Name = "GetAccountApplicantParties")]
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
    [HttpPost("{applicantPartyId:long}/make-current-default", Name = "MakeApplicantPartyCurrentDefault")]
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
    [HttpGet("current-individual", Name = "GetCurrentIndividualApplicantParty")]
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
