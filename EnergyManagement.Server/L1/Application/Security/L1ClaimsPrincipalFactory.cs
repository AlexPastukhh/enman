using System.Security.Claims;
using Domain.EnergyManagement.L1;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EnergyManagement.Server.L1.Application.Security;

public sealed class L1ClaimsPrincipalFactory
{
    public ClaimsPrincipal CreatePrincipal(Account account, string? windowsLogin = null)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Email, account.Email.Value),
            new(ClaimTypes.Role, account.Role.ToString()),
            new(L1AuthClaimTypes.AuthModel, L1AuthClaimTypes.AuthModelValue)
        };

        if (account is Employee employee)
        {
            claims.Add(new(ClaimTypes.Name, employee.FullName.Value));
        }

        if (!string.IsNullOrWhiteSpace(windowsLogin))
        {
            claims.Add(new(L1AuthClaimTypes.WindowsName, windowsLogin.Trim()));
            claims.Add(new(L1AuthClaimTypes.AuthSource, L1AuthClaimTypes.AuthSourceWindows));
        }
        else
        {
            claims.Add(new(L1AuthClaimTypes.AuthSource, L1AuthClaimTypes.AuthSourcePassword));
        }

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}
