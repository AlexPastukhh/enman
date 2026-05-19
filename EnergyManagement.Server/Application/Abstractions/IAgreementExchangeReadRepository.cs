namespace EnergyManagement.Server.Application.Abstractions;

public interface IAgreementExchangeReadRepository
{
    Task<AgreementExchangeDetailsResponse?> GetDetailsForClientAsync(
        long clientAccountId,
        long exchangeId,
        CancellationToken cancellationToken);

    Task<AgreementExchangeDetailsResponse?> GetDetailsForEmployeeAsync(
        long employeeId,
        long exchangeId,
        CancellationToken cancellationToken);
}

public sealed record AgreementExchangeDetailsResponse(
    long ExchangeId,
    long RequestId,
    string ExchangeStatus,
    int ActiveProposalVersion,
    AgreementExchangeRequestSummaryResponse Request,
    AgreementProposalDetailsResponse ActiveProposal,
    IReadOnlyList<AgreementProposalDetailsResponse> Proposals,
    string CurrentActorSide,
    DateTimeOffset CreatedAt,
    DateTimeOffset? LastActivityAt);

public sealed record AgreementExchangeRequestSummaryResponse(
    long RequestId,
    string RequestStatus,
    string RequestDisplayName,
    string ObjectAddress);

public sealed record AgreementProposalDetailsResponse(
    long ProposalId,
    int Version,
    string Sender,
    long SenderId,
    string State,
    AgreementDocumentRefResponse Document,
    string? Comment,
    DateTimeOffset CreatedAt);

public sealed record AgreementDocumentRefResponse(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
