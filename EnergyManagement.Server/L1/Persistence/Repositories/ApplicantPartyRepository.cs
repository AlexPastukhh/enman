using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.L1.Persistence.Repositories;

public sealed class ApplicantPartyRepository : IApplicantPartyRepository
{
    private readonly L1DbContext _context;

    public ApplicantPartyRepository(L1DbContext context)
    {
        _context = context;
    }

    public void Add(ApplicantParty applicantParty)
    {
        _context.ApplicantParties.Add(applicantParty);
    }

    public Task<ApplicantParty?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _context.ApplicantParties.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<ApplicantParty?> GetOwnedByIdAsync(
        long applicantPartyId,
        long clientAccountId,
        CancellationToken cancellationToken)
    {
        return _context.ApplicantParties.FirstOrDefaultAsync(
            x => x.Id == applicantPartyId
                && x.ClientAccountId == clientAccountId,
            cancellationToken);
    }

    public Task<bool> ExistsByClientAccountIdAndTypeAsync(
        long clientAccountId,
        ApplicantPartyType applicantPartyType,
        CancellationToken cancellationToken)
    {
        return _context.ApplicantParties.AnyAsync(
            x => x.ClientAccountId == clientAccountId
                && x.ApplicantPartyType == applicantPartyType,
            cancellationToken);
    }

    public async Task<IReadOnlyList<ApplicantParty>> ListOwnedByAccountIdAsync(
        long clientAccountId,
        CancellationToken cancellationToken)
    {
        return await _context.ApplicantParties
            .Where(x => x.ClientAccountId == clientAccountId)
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public Task<IndividualApplicantParty?> GetCurrentActiveIndividualByClientAccountIdAsync(
        long clientAccountId,
        CancellationToken cancellationToken)
    {
        return _context.ApplicantParties
            .OfType<IndividualApplicantParty>()
            .FirstOrDefaultAsync(
                x => x.ClientAccountId == clientAccountId
                    && x.IsCurrentActiveVersion
                    && x.ApplicantPartyType == ApplicantPartyType.Individual,
                cancellationToken);
    }
}
