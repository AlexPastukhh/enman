using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.Persistence.Repositories;

public sealed class AgreementProposalExchangeRepository : IAgreementProposalExchangeRepository
{
    private readonly EnergyManagementDbContext _context;

    public AgreementProposalExchangeRepository(EnergyManagementDbContext context)
    {
        _context = context;
    }

    public void Add(AgreementProposalExchange exchange)
    {
        _context.AgreementProposalExchanges.Add(exchange);
    }

    public Task<AgreementProposalExchange?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        return _context.AgreementProposalExchanges
            .Include(exchange => exchange.Proposals)
            .FirstOrDefaultAsync(exchange => exchange.Id == id, cancellationToken);
    }

    public Task<AgreementProposalExchange?> GetByRequestIdAsync(
        long requestId,
        CancellationToken cancellationToken)
    {
        return _context.AgreementProposalExchanges
            .Include(exchange => exchange.Proposals)
            .FirstOrDefaultAsync(exchange => exchange.RequestId == requestId, cancellationToken);
    }
}
