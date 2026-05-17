using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IAgreementExchangeApplicationService
{
    Task<UnitResult<IReadOnlyList<Error>>> SendClientProposalVersionAsync(
        long clientAccountId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken);

    Task<UnitResult<IReadOnlyList<Error>>> SendEmployeeProposalVersionAsync(
        long employeeId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken);
}

public sealed record AgreementDocumentRefInput(
    string? StorageKey,
    string? OriginalFileName,
    string? ContentType,
    long SizeBytes);
