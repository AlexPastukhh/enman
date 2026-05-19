using System.Security.Claims;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Api.Validation;
using EnergyManagement.Server.Application.Commands;
using EnergyManagement.Server.Application.Queries;
using EnergyManagement.Server.Application.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.Controllers;

[ApiController]
[Route("api/employee/requests")]
public sealed class EmployeeRequestsController : ProjectController
{
    private readonly ISender _sender;
    private readonly ILogger<EmployeeRequestsController> _logger;
    private readonly IValidator<EmployeeRequestListQueryDto> _listQueryValidator;
    private readonly IValidator<EmployeeRejectRequestReviewDto> _rejectReviewValidator;

    public EmployeeRequestsController(
        ISender sender,
        ILogger<EmployeeRequestsController> logger,
        IValidator<EmployeeRequestListQueryDto> listQueryValidator,
        IValidator<EmployeeRejectRequestReviewDto> rejectReviewValidator)
    {
        _sender = sender;
        _logger = logger;
        _listQueryValidator = listQueryValidator;
        _rejectReviewValidator = rejectReviewValidator;
    }

    [Authorize(Roles = "Employee")]
    [HttpGet(Name = "EmployeeListRequests")]
    [ProducesResponseType(typeof(EmployeeRequestListResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListRequests(
        [FromQuery] string? status,
        [FromQuery] string? reviewState,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryDto = new EmployeeRequestListQueryDto(status, reviewState);
            var validationResult = await _listQueryValidator.ValidateAsync(queryDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new EmployeeRequestListQuery(employeeId, status, reviewState),
                cancellationToken);

            if (result.IsFailure)
            {
                return ProblemDetailsFromValidation(result.Error);
            }

            return Ok(ToDto(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee request list failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Employee")]
    [HttpGet("{requestId:long}", Name = "EmployeeGetRequestDetails")]
    [ProducesResponseType(typeof(EmployeeRequestDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRequestDetails(
        long requestId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new EmployeeRequestDetailsQuery(employeeId, requestId),
                cancellationToken);

            if (result.HasNoValue)
            {
                return NotFound();
            }

            return Ok(ToDto(result.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee request details failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("{requestId:long:min(1)}/review/start", Name = "EmployeeStartRequestReview")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartReview(
        long requestId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new EmployeeStartRequestReviewCommand(employeeId, requestId),
                cancellationToken);

            return result.Status switch
            {
                EmployeeStartRequestReviewCommandStatus.Started => NoContent(),
                EmployeeStartRequestReviewCommandStatus.NotFound => NotFound(),
                EmployeeStartRequestReviewCommandStatus.Forbidden => Forbid(),
                EmployeeStartRequestReviewCommandStatus.Invalid => ProblemDetailsFromValidation(result.Errors),
                _ => ProblemDetailsFromInternalServerError(Domain.EnergyManagement.Common.Error.Errors.General.InternalServerError)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee start request review failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("{requestId:long:min(1)}/review/approve", Name = "EmployeeApproveRequestReview")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApproveReview(
        long requestId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new EmployeeApproveRequestReviewCommand(employeeId, requestId),
                cancellationToken);

            return result.Status switch
            {
                EmployeeApproveRequestReviewCommandStatus.Approved => NoContent(),
                EmployeeApproveRequestReviewCommandStatus.NotFound => NotFound(),
                EmployeeApproveRequestReviewCommandStatus.Forbidden => Forbid(),
                EmployeeApproveRequestReviewCommandStatus.Invalid => ProblemDetailsFromValidation(result.Errors),
                _ => ProblemDetailsFromInternalServerError(Domain.EnergyManagement.Common.Error.Errors.General.InternalServerError)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee approve request review failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }


    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("{requestId:long:min(1)}/applicant-party/verification/run", Name = "EmployeeRunApplicantPartyVerification")]
    [ProducesResponseType(typeof(RunApplicantPartyVerificationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RunApplicantPartyVerification(
        long requestId,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new RunApplicantPartyVerificationFromRequestCommand(employeeId, requestId),
                cancellationToken);

            return result.Status switch
            {
                RunApplicantPartyVerificationFromRequestCommandStatus.Verified => Ok(ToDto(result.Response!)),
                RunApplicantPartyVerificationFromRequestCommandStatus.NotFound => NotFound(),
                RunApplicantPartyVerificationFromRequestCommandStatus.Forbidden => Forbid(),
                RunApplicantPartyVerificationFromRequestCommandStatus.Invalid => ProblemDetailsFromValidation(result.Errors),
                _ => ProblemDetailsFromInternalServerError(Domain.EnergyManagement.Common.Error.Errors.General.InternalServerError)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee run applicant party verification failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }





    [Authorize(Roles = "Employee")]
    [RequireAntiforgeryToken]
    [HttpPost("{requestId:long:min(1)}/review/reject", Name = "EmployeeRejectRequestReview")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RejectReview(
        long requestId,
        [FromBody] EmployeeRejectRequestReviewDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _rejectReviewValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ProblemDetailsFromValidation(validationResult.Errors);
            }

            if (!TryGetCurrentEmployeeId(out var employeeId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(
                new EmployeeRejectRequestReviewCommand(employeeId, requestId, dto.Feedback ?? string.Empty),
                cancellationToken);

            return result.Status switch
            {
                EmployeeRejectRequestReviewCommandStatus.Rejected => NoContent(),
                EmployeeRejectRequestReviewCommandStatus.NotFound => NotFound(),
                EmployeeRejectRequestReviewCommandStatus.Forbidden => Forbid(),
                EmployeeRejectRequestReviewCommandStatus.Invalid => ProblemDetailsFromValidation(result.Errors),
                _ => ProblemDetailsFromInternalServerError(Domain.EnergyManagement.Common.Error.Errors.General.InternalServerError)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee reject request review failed for request {RequestId}.", requestId);
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    private bool TryGetCurrentEmployeeId(out long employeeId)
    {
        employeeId = default;

        var authModel = User.FindFirstValue(AuthClaimTypes.AuthModel);
        if (authModel != AuthClaimTypes.AuthModelValue)
        {
            return false;
        }

        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return long.TryParse(claimValue, out employeeId);
    }


    private static RunApplicantPartyVerificationResponseDto ToDto(
        RunApplicantPartyVerificationResponse response)
    {
        return new RunApplicantPartyVerificationResponseDto(
            response.RequestId,
            response.ApplicantPartyId,
            response.VerificationStatus,
            response.MockResult,
            response.Message);
    }

    private static EmployeeRequestListResponseDto ToDto(EmployeeRequestListResponse response)
    {
        return new EmployeeRequestListResponseDto(
            response.Requests.Select(ToDto).ToList());
    }

    private static EmployeeRequestListItemDto ToDto(EmployeeRequestListItemResponse item)
    {
        return new EmployeeRequestListItemDto(
            item.RequestId,
            item.RequestType,
            item.Status,
            item.ApplicantDisplayName,
            item.ObjectAddress,
            item.CreatedAt,
            item.ReviewState,
            ToDto(item.ApplicantVerification));
    }


    private static EmployeeRequestDetailsDto ToDto(EmployeeRequestDetailsResponse response)
    {
        return new EmployeeRequestDetailsDto(
            response.RequestId,
            response.RequestType,
            response.Status,
            new EmployeeRequestApplicantSummaryDto(
                response.Applicant.ApplicantPartyId,
                response.Applicant.ApplicantPartyType,
                response.Applicant.DisplayName,
                response.Applicant.Email,
                response.Applicant.PhoneNumber),
            response.ObjectAddress,
            response.Details,
            response.CreatedAt,
            response.ReviewState,
            ToDto(response.ApplicantVerification));
    }

    private static EmployeeRequestApplicantVerificationDto? ToDto(
        EmployeeRequestApplicantVerificationResponse? response)
    {
        return response is null
            ? null
            : new EmployeeRequestApplicantVerificationDto(
                response.Required,
                response.Status,
                response.CanRun,
                response.Message);
    }

}
