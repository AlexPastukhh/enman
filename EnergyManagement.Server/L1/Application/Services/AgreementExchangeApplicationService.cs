using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;

namespace EnergyManagement.Server.L1.Application.Services;

public sealed class AgreementExchangeApplicationService : IAgreementExchangeApplicationService
{
    private readonly IAccountRepository _accounts;
    private readonly IEmployeeRepository _employees;
    private readonly IAgreementProposalExchangeRepository _agreementExchanges;
    private readonly L1DbContext _context;

    public AgreementExchangeApplicationService(
        IAccountRepository accounts,
        IEmployeeRepository employees,
        IAgreementProposalExchangeRepository agreementExchanges,
        L1DbContext context)
    {
        _accounts = accounts;
        _employees = employees;
        _agreementExchanges = agreementExchanges;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> SendClientProposalVersionAsync(
        long clientAccountId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken)
    {
        var client = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (client is not ClientAccount clientAccount)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.ClientAccountIsRequired]);
        }

        var exchange = await _agreementExchanges.GetByRequestIdAsync(requestId, cancellationToken);
        if (exchange is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        var documentRef = CreateDocumentRef(document);
        if (documentRef.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(documentRef.Error);
        }

        var proposalComment = CreateOptionalComment(comment);
        if (proposalComment.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(proposalComment.Error);
        }

        var send = exchange.ClientSendOwnVersion(
            documentRef.Value,
            proposalComment.Value,
            clientAccount,
            DateTimeOffset.UtcNow);

        if (send.IsFailure)
        {
            return send;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> SendEmployeeProposalVersionAsync(
        long employeeId,
        long requestId,
        AgreementDocumentRefInput document,
        string? comment,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(employeeId, cancellationToken);
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.EmployeeIsRequired]);
        }

        var exchange = await _agreementExchanges.GetByRequestIdAsync(requestId, cancellationToken);
        if (exchange is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        var documentRef = CreateDocumentRef(document);
        if (documentRef.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(documentRef.Error);
        }

        var proposalComment = CreateOptionalComment(comment);
        if (proposalComment.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(proposalComment.Error);
        }

        var send = exchange.EmployeeSendNewVersion(
            documentRef.Value,
            proposalComment.Value,
            employee,
            DateTimeOffset.UtcNow);

        if (send.IsFailure)
        {
            return send;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private static Result<AgreementDocumentRef, IReadOnlyList<Error>> CreateDocumentRef(
        AgreementDocumentRefInput document)
    {
        return AgreementDocumentRef.Create(
            document.StorageKey ?? string.Empty,
            document.OriginalFileName ?? string.Empty,
            document.ContentType ?? string.Empty,
            document.SizeBytes);
    }

    private static Result<ProposalComment?, IReadOnlyList<Error>> CreateOptionalComment(string? comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            return Result.Success<ProposalComment?, IReadOnlyList<Error>>(null);
        }

        var proposalComment = ProposalComment.Create(comment);
        if (proposalComment.IsFailure)
        {
            return Result.Failure<ProposalComment?, IReadOnlyList<Error>>(proposalComment.Error);
        }

        return Result.Success<ProposalComment?, IReadOnlyList<Error>>(proposalComment.Value);
    }
}
