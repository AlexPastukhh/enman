using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

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
