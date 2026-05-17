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

    Task<UnitResult<IReadOnlyList<Error>>> ClientAcceptActiveProposalAsync(
        long clientAccountId,
        long exchangeId,
        CancellationToken cancellationToken);

    Task<UnitResult<IReadOnlyList<Error>>> EmployeeFinalRefuseAgreementExchangeAsync(
        long employeeId,
        long exchangeId,
        string? reason,
        CancellationToken cancellationToken);
}

public sealed record AgreementDocumentRefInput(
    string? StorageKey,
    string? OriginalFileName,
    string? ContentType,
    long SizeBytes);
