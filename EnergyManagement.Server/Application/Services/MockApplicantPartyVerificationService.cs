using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;

namespace EnergyManagement.Server.Application.Services;

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
