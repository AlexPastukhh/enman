using Microsoft.AspNetCore.Authentication.Negotiate;

namespace EnergyManagement.Server.Api.Auth;

public static class EmployeeAuthSchemes
{
    public const string EmployeeWindows = NegotiateDefaults.AuthenticationScheme;
}
