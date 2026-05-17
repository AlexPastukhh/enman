using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IApplicantPartyRepository
{
    void Add(ApplicantParty applicantParty);

    Task<ApplicantParty?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<ApplicantParty?> GetOwnedByIdAsync(
        long applicantPartyId,
        long clientAccountId,
        CancellationToken cancellationToken);

    Task<bool> ExistsByClientAccountIdAndTypeAsync(
        long clientAccountId,
        ApplicantPartyType applicantPartyType,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ApplicantParty>> ListOwnedByAccountIdAsync(
        long clientAccountId,
        CancellationToken cancellationToken);

    Task<IndividualApplicantParty?> GetCurrentActiveIndividualByClientAccountIdAsync(
        long clientAccountId,
        CancellationToken cancellationToken);
}
