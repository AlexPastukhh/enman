using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IClientRequestRepository
{
    void Add(ClientRequest request);

    Task<ClientRequest?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<IReadOnlyList<ClientRequestSummaryReadModel>> ListByClientAccountIdAsync(
        long clientAccountId,
        RequestStatus? status,
        CancellationToken cancellationToken);
}

public sealed record ClientRequestSummaryReadModel(
    long RequestId,
    ClientRequestType RequestType,
    RequestStatus Status,
    DateTimeOffset CreatedAt,
    string Details,
    string PostalCode,
    string Region,
    string City,
    string Street,
    string House,
    string? Building,
    string? Apartment);
