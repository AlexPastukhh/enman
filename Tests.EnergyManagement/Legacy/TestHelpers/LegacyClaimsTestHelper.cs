using System.Security.Claims;
using Domain.EnergyManagement.DocumentManaging;

namespace Tests.EnergyManagement.Legacy.TestHelpers;

public static class LegacyClaimsTestHelper
{
    public static IReadOnlyList<Claim> GetClaimsForIndividual(IndividualClient individual)
    {
        return new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, individual.Id.ToString()),
            new(ClaimTypes.Email, individual.Email.Value)
        };
    }
}
