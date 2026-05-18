using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;

namespace EnergyManagement.Server.L1.Application.Services;

public sealed class MockApplicantPartyVerificationService : IApplicantPartyMockVerificationService
{
    public Task<ApplicantPartyMockVerificationResult> VerifyAsync(
        ApplicantParty applicantParty,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new ApplicantPartyMockVerificationResult(
            IsPassed: true,
            Result: "Passed",
            Message: "Mock verification passed."));
    }
}
