using System.Security.Claims;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Api.Validation;
using EnergyManagement.Server.L1.Application.Queries;
using EnergyManagement.Server.L1.Application.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.L1.Controllers;

[ApiController]
[Route("api/employee/requests")]
public sealed class EmployeeRequestsController : ProjectController
{
    private readonly ISender _sender;
    private readonly ILogger<EmployeeRequestsController> _logger;
    private readonly IValidator<EmployeeRequestListQueryDto> _listQueryValidator;

    public EmployeeRequestsController(
        ISender sender,
        ILogger<EmployeeRequestsController> logger,
        IValidator<EmployeeRequestListQueryDto> listQueryValidator)
    {
        _sender = sender;
        _logger = logger;
        _listQueryValidator = listQueryValidator;
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

    private bool TryGetCurrentEmployeeId(out long employeeId)
    {
        employeeId = default;

        var authModel = User.FindFirstValue(L1AuthClaimTypes.AuthModel);
        if (authModel != L1AuthClaimTypes.AuthModelValue)
        {
            return false;
        }

        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return long.TryParse(claimValue, out employeeId);
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
            item.ReviewState);
    }
}
