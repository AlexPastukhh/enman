using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IAgreementProposalExchangeRepository
{
    void Add(AgreementProposalExchange exchange);

    Task<AgreementProposalExchange?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken);

    Task<AgreementProposalExchange?> GetByRequestIdAsync(
        long requestId,
        CancellationToken cancellationToken);
}
