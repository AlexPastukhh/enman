using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IApplicantPartyMockVerificationService
{
    Task<ApplicantPartyMockVerificationResult> VerifyAsync(
        ApplicantParty applicantParty,
        CancellationToken cancellationToken);
}

public sealed record ApplicantPartyMockVerificationResult(
    bool IsPassed,
    string Result,
    string? Message);
