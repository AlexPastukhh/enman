using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IClientRequestRepository
{
    void Add(ClientRequest request);

    Task<ClientRequest?> GetByIdAsync(long id, CancellationToken cancellationToken);
}
