using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IApplicantPartyRepository
{
    void Add(ApplicantParty applicantParty);

    Task<ApplicantParty?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<ApplicantParty?> GetByAccountIdAsync(long accountId, CancellationToken cancellationToken);
}
