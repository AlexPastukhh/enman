using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.L1.Persistence.Repositories;

public sealed class ClientRequestRepository : IClientRequestRepository
{
    private readonly L1DbContext _context;

    public ClientRequestRepository(L1DbContext context)
    {
        _context = context;
    }

    public void Add(ClientRequest request)
    {
        _context.ClientRequests.Add(request);
    }

    public Task<ClientRequest?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _context.ClientRequests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
