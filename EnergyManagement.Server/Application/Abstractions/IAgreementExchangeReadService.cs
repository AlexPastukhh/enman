using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

public interface IAgreementExchangeReadService
{
    Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForClientAsync(
        long clientAccountId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken);

    Task<Result<AgreementExchangeListResponse, IReadOnlyList<Error>>> ListForEmployeeAsync(
        long employeeId,
        AgreementExchangeStatus? status,
        CancellationToken cancellationToken);
}

public sealed record AgreementExchangeListResponse(
    IReadOnlyList<AgreementExchangeListItemResponse> Exchanges);

public sealed record AgreementExchangeListItemResponse(
    long ExchangeId,
    long RequestId,
    string ExchangeStatus,
    int ActiveProposalVersion,
    string ActiveProposalSender,
    long ActiveProposalSenderId,
    string RequestDisplayName,
    string ObjectAddress,
    DateTimeOffset CreatedAt,
    DateTimeOffset? LastActivityAt);
