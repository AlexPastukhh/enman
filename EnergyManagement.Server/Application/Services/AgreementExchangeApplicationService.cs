using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;

namespace EnergyManagement.Server.Application.Services;

public sealed class AgreementExchangeApplicationService : IAgreementExchangeApplicationService
{
    private readonly IAccountRepository _accounts;
    private readonly IEmployeeRepository _employees;
    private readonly IAgreementProposalExchangeRepository _agreementExchanges;
    private readonly IClientRequestRepository _clientRequests;
    private readonly EnergyManagementDbContext _context;

    public AgreementExchangeApplicationService(
        IAccountRepository accounts,
        IEmployeeRepository employees,
        IAgreementProposalExchangeRepository agreementExchanges,
        IClientRequestRepository clientRequests,
        EnergyManagementDbContext context)
    {
        _accounts = accounts;
        _employees = employees;
        _agreementExchanges = agreementExchanges;
        _clientRequests = clientRequests;
        _context = context;
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> StartAgreementExchangeByEmployeeAsync(
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

        var request = await _clientRequests.GetByIdAsync(requestId, cancellationToken);
        if (request is not ConnectionRequest connectionRequest)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.RequestIsRequired]);
        }

        var existingExchange = await _agreementExchanges.GetByRequestIdAsync(requestId, cancellationToken);
        if (existingExchange is not null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.AgreementProposalExchangeAlreadyStarted]);
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

        var exchange = AgreementProposalExchange.StartByEmployee(
            connectionRequest,
            documentRef.Value,
            proposalComment.Value,
            employee,
            DateTimeOffset.UtcNow);

        if (exchange.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(exchange.Error);
        }

        _agreementExchanges.Add(exchange.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
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

    public async Task<UnitResult<IReadOnlyList<Error>>> ClientAcceptActiveProposalAsync(
        long clientAccountId,
        long exchangeId,
        CancellationToken cancellationToken)
    {
        var client = await _accounts.GetByIdAsync(clientAccountId, cancellationToken);
        if (client is not ClientAccount clientAccount)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.ClientAccountIsRequired]);
        }

        var exchange = await _agreementExchanges.GetByIdAsync(exchangeId, cancellationToken);
        if (exchange is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        var accept = exchange.ClientAcceptActiveProposal(
            clientAccount,
            DateTimeOffset.UtcNow);

        if (accept.IsFailure)
        {
            return accept;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public async Task<UnitResult<IReadOnlyList<Error>>> EmployeeFinalRefuseAgreementExchangeAsync(
        long employeeId,
        long exchangeId,
        string? reason,
        CancellationToken cancellationToken)
    {
        var employee = await _employees.GetByIdAsync(employeeId, cancellationToken);
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.EmployeeIsRequired]);
        }

        var exchange = await _agreementExchanges.GetByIdAsync(exchangeId, cancellationToken);
        if (exchange is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.AgreementProposalExchangeIsRequired]);
        }

        var request = await _clientRequests.GetByIdAsync(exchange.RequestId, cancellationToken);
        if (request is not ConnectionRequest connectionRequest)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Error.Errors.L1Domain.RequestIsRequired]);
        }

        var finalRefusalReason = CreateOptionalFinalRefusalReason(reason);
        if (finalRefusalReason.IsFailure)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(finalRefusalReason.Error);
        }

        var now = DateTimeOffset.UtcNow;

        var refuse = exchange.FinalRefuseProposal(
            employee,
            finalRefusalReason.Value,
            now);

        if (refuse.IsFailure)
        {
            return refuse;
        }

        var markRequestFailed = connectionRequest.MarkAgreementExchangeFailed(exchange.Id, now);
        if (markRequestFailed.IsFailure)
        {
            return markRequestFailed;
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

    private static Result<FinalRefusalReason?, IReadOnlyList<Error>> CreateOptionalFinalRefusalReason(string? reason)
    {
        if (reason is null)
        {
            return Result.Success<FinalRefusalReason?, IReadOnlyList<Error>>(null);
        }

        var finalRefusalReason = FinalRefusalReason.Create(reason);
        if (finalRefusalReason.IsFailure)
        {
            return Result.Failure<FinalRefusalReason?, IReadOnlyList<Error>>(finalRefusalReason.Error);
        }

        return Result.Success<FinalRefusalReason?, IReadOnlyList<Error>>(finalRefusalReason.Value);
    }

}
