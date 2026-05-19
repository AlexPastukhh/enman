using System.Security.Claims;
using Domain.EnergyManagement;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EnergyManagement.Server.Application.Security;

public sealed class ClaimsPrincipalFactory
{
    public ClaimsPrincipal CreatePrincipal(Account account, string? windowsLogin = null)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email.Value),
            new(ClaimTypes.Role, account.Role.ToString()),
            new(AuthClaimTypes.AuthModel, AuthClaimTypes.AuthModelValue)
        };

        if (account is Employee employee)
        {
            claims.Add(new(ClaimTypes.Name, FormatFullName(employee.FullName)));
        }

        if (!string.IsNullOrWhiteSpace(windowsLogin))
        {
            claims.Add(new(AuthClaimTypes.WindowsName, windowsLogin.Trim()));
            claims.Add(new(AuthClaimTypes.AuthSource, AuthClaimTypes.AuthSourceWindows));
        }
        else
        {
            claims.Add(new(AuthClaimTypes.AuthSource, AuthClaimTypes.AuthSourcePassword));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private static string FormatFullName(Domain.EnergyManagement.DocumentManaging.FullName fullName)
    {
        return string.Join(" ", new[]
        {
            fullName.LastName,
            fullName.FirstName,
            fullName.MiddleName
        }.Where(part => !string.IsNullOrWhiteSpace(part)));
    }
}
