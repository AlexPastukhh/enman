using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

public interface IClientRequestRepository
{
    void Add(ClientRequest request);

    Task<ClientRequest?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<IReadOnlyList<ClientRequestSummaryReadModel>> ListByClientAccountIdAsync(
        long clientAccountId,
        RequestStatus? status,
        CancellationToken cancellationToken);

    Task<ClientRequestDetailsReadModel?> GetDetailsByIdAndClientAccountIdAsync(
        long requestId,
        long clientAccountId,
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

public sealed record ClientRequestDetailsReadModel(
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
    string? Apartment,
    RequestReviewStatus? ReviewStatus,
    DateTimeOffset? ReviewCompletedAt,
    string? RejectionReason);
