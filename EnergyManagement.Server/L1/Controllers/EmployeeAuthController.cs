using System.Security.Claims;
using EnergyManagement.Server.Api.Auth;
using EnergyManagement.Server.Controllers;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyManagement.Server.L1.Controllers;

[ApiController]
[Route("api/employee/auth")]
public sealed class EmployeeAuthController : ProjectController
{
    private const string WindowsIdentityMissingCode = "auth.employee.windows.identity.missing";
    private const string WindowsEmployeeNotRegisteredCode = "auth.employee.windows.not_registered";
    private const string EmployeeInactiveCode = "auth.employee.inactive";

    private readonly IEmployeeRepository _employees;
    private readonly L1ClaimsPrincipalFactory _claimsPrincipalFactory;
    private readonly ILogger<EmployeeAuthController> _logger;

    public EmployeeAuthController(
        IEmployeeRepository employees,
        L1ClaimsPrincipalFactory claimsPrincipalFactory,
        ILogger<EmployeeAuthController> logger)
    {
        _employees = employees;
        _claimsPrincipalFactory = claimsPrincipalFactory;
        _logger = logger;
    }

    [Authorize(AuthenticationSchemes = EmployeeAuthSchemes.EmployeeWindows)]
    [HttpGet("windows-signin", Name = "EmployeeWindowsSignIn")]
    [ProducesResponseType(typeof(L1CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WindowsSignIn(CancellationToken cancellationToken)
    {
        try
        {
            var windowsLogin = User.Identity?.Name ?? User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrWhiteSpace(windowsLogin))
            {
                return AuthProblem(
                    StatusCodes.Status401Unauthorized,
                    "Windows identity is missing",
                    "Windows identity is required to sign in as employee.",
                    WindowsIdentityMissingCode,
                    "https://enman.local/problems/auth/employee-windows-identity-missing");
            }

            windowsLogin = windowsLogin.Trim();
            var employee = await _employees.GetByWindowsLoginAsync(windowsLogin, cancellationToken);
            if (employee is null)
            {
                return AuthProblem(
                    StatusCodes.Status403Forbidden,
                    "Employee is not registered",
                    "The Windows identity is authenticated but is not registered as an active employee.",
                    WindowsEmployeeNotRegisteredCode,
                    "https://enman.local/problems/auth/employee-windows-not-registered");
            }

            var canReview = employee.EnsureCanReview();
            if (canReview.IsFailure)
            {
                return AuthProblem(
                    StatusCodes.Status403Forbidden,
                    "Employee is inactive",
                    "The employee account is inactive.",
                    EmployeeInactiveCode,
                    "https://enman.local/problems/auth/employee-inactive");
            }

            var principal = _claimsPrincipalFactory.CreatePrincipal(employee, windowsLogin);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = false });

            return Ok(new L1CurrentUserResponse(
                employee.Id,
                employee.Email.Value,
                employee.Role.ToString(),
                employee.IsActive,
                IsAuthenticated: true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Employee Windows sign-in failed.");
            return ProblemDetailsWithExceptionDev(ex);
        }
    }

    private static ObjectResult AuthProblem(
        int status,
        string title,
        string detail,
        string code,
        string type)
    {
        var problemDetails = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Type = type
        };

        problemDetails.Extensions["code"] = code;

        return new ObjectResult(problemDetails)
        {
            StatusCode = status
        };
    }
}
