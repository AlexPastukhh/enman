using System.Security.Claims;
using EnergyManagement.Server.Data;

namespace Tests.EnergyManagement.TestHelpers;

public sealed record TestIndividualActor(
    long Id,
    string Email,
    string Password,
    IReadOnlyList<Claim> Claims)
{
    public LoginDto LoginDto => new(Email, Password);
}
