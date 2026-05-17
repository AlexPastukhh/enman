using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class AgreementProposalExchange : L1Entity
{
    public long RequestId { get; private set; }

    public long ClientAccountId { get; private set; }

    public AgreementExchangeStatus Status { get; private set; }

    public AgreementProposalVersion ActiveProposalVersion { get; private set; }

    private readonly List<AgreementProposal> _proposals = new();

    public IReadOnlyCollection<AgreementProposal> Proposals => _proposals.AsReadOnly();

    public long? FinalRefusedByEmployeeId { get; private set; }

    public DateTimeOffset? FinalRefusedAt { get; private set; }

    public FinalRefusalReason? FinalRefusalReason { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private AgreementProposalExchange()
    {
    }

    public static Result<AgreementProposalExchange, IReadOnlyList<Error>> StartByEmployee(
        ConnectionRequest approvedRequest,
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset startedAt)
    {
        var errors = new List<Error>();

        if (approvedRequest is null)
        {
            errors.Add(Errors.L1Domain.RequestIsRequired);
        }
        else if (approvedRequest.Id <= 0)
        {
            errors.Add(Errors.L1Domain.RequestIsRequired);
        }
        else if (approvedRequest.Status != RequestStatus.Approved)
        {
            errors.Add(Errors.L1Domain.AgreementExchangeRequiresApprovedRequest);
        }

        var clientAccountId = approvedRequest?.ClientAccountId ?? 0;
        if (clientAccountId <= 0)
        {
            errors.Add(Errors.L1Domain.ClientAccountIsRequired);
        }

        if (document is null)
        {
            errors.Add(Errors.L1Domain.AgreementDocumentIsRequired);
        }

        if (employee is null)
        {
            errors.Add(Errors.L1Domain.EmployeeIsRequired);
        }
        else if (employee.Id <= 0)
        {
            errors.Add(Errors.L1Domain.EmployeeIsRequired);
        }

        if (employee is not null)
        {
            var canStart = employee.EnsureCanStartAgreementExchange();
            if (canStart.IsFailure)
            {
                errors.AddRange(canStart.Error);
            }
        }

        if (errors.Count > 0)
        {
            return Result.Failure<AgreementProposalExchange, IReadOnlyList<Error>>(errors);
        }

        var firstVersion = AgreementProposalVersion.First;

        var exchange = new AgreementProposalExchange
        {
            RequestId = approvedRequest!.Id,
            ClientAccountId = clientAccountId,
            Status = AgreementExchangeStatus.AwaitingClientConfirmation,
            ActiveProposalVersion = firstVersion,
            CreatedAt = startedAt
        };

        var proposal = AgreementProposal.EmployeeProposal(
            firstVersion,
            document!,
            comment,
            employee!,
            startedAt);

        exchange._proposals.Add(proposal);

        return Result.Success<AgreementProposalExchange, IReadOnlyList<Error>>(exchange);
    }

    private UnitResult<IReadOnlyList<Error>> EnsureClientCanAct(ClientAccount client)
    {
        if (client is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientAccountIsRequired]);
        }

        if (client.Id <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientAccountIsRequired]);
        }

        if (client.Id != ClientAccountId)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientCannotActOnThisAgreementExchange]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ClientAcceptActiveProposal(
        ClientAccount client,
        DateTimeOffset acceptedAt)
    {
        var canAct = EnsureClientCanAct(client);
        if (canAct.IsFailure)
        {
            return canAct;
        }

        if (Status != AgreementExchangeStatus.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.OnlyAwaitingClientConfirmationCanBeAccepted]);
        }

        var activeProposal = GetActiveProposal();

        var accept = activeProposal.MarkAccepted();
        if (accept.IsFailure)
        {
            return accept;
        }

        Status = AgreementExchangeStatus.Accepted;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> ClientSendOwnVersion(
        AgreementDocumentRef document,
        ProposalComment? comment,
        ClientAccount client,
        DateTimeOffset createdAt)
    {
        if (Status != AgreementExchangeStatus.AwaitingClientConfirmation)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientVersionCanBeSentOnlyWhenAwaitingClientConfirmation]);
        }

        if (document is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementDocumentIsRequired]);
        }

        var canAct = EnsureClientCanAct(client);
        if (canAct.IsFailure)
        {
            return canAct;
        }

        var activeProposal = GetActiveProposal();

        if (activeProposal.Author.Sender != AgreementProposalSender.Employee)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.ClientCanRespondOnlyToEmployeeProposal]);
        }

        var supersede = activeProposal.MarkSupersededByCounterProposal();
        if (supersede.IsFailure)
        {
            return supersede;
        }

        var nextVersion = GetNextVersion();

        var clientProposal = AgreementProposal.ClientProposal(
            nextVersion,
            document,
            comment,
            client,
            createdAt);

        _proposals.Add(clientProposal);

        ActiveProposalVersion = nextVersion;
        Status = AgreementExchangeStatus.AwaitingEmployeeResponse;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> EmployeeSendNewVersion(
        AgreementDocumentRef document,
        ProposalComment? comment,
        Employee employee,
        DateTimeOffset createdAt)
    {
        if (Status != AgreementExchangeStatus.AwaitingEmployeeResponse)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeCanRespondOnlyWhenAwaitingEmployeeResponse]);
        }

        if (document is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementDocumentIsRequired]);
        }

        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        if (employee.Id <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canSend = employee.EnsureCanSendAgreementProposal();
        if (canSend.IsFailure)
        {
            return canSend;
        }

        var activeProposal = GetActiveProposal();

        if (activeProposal.Author.Sender != AgreementProposalSender.Client)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeCanSupersedeOnlyClientProposal]);
        }

        var supersede = activeProposal.MarkSupersededByCounterProposal();
        if (supersede.IsFailure)
        {
            return supersede;
        }

        var nextVersion = GetNextVersion();

        var employeeProposal = AgreementProposal.EmployeeProposal(
            nextVersion,
            document,
            comment,
            employee,
            createdAt);

        _proposals.Add(employeeProposal);

        ActiveProposalVersion = nextVersion;
        Status = AgreementExchangeStatus.AwaitingClientConfirmation;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    public UnitResult<IReadOnlyList<Error>> FinalRefuseProposal(
        Employee employee,
        FinalRefusalReason? reason,
        DateTimeOffset refusedAt)
    {
        if (employee is null)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        if (employee.Id <= 0)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        var canRefuse = employee.EnsureCanFinalRefuseAgreement();
        if (canRefuse.IsFailure)
        {
            return canRefuse;
        }

        if (Status == AgreementExchangeStatus.Accepted)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AcceptedAgreementExchangeCannotBeRefused]);
        }

        if (Status == AgreementExchangeStatus.FinallyRefused)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementExchangeAlreadyFinallyRefused]);
        }

        if (Status is not AgreementExchangeStatus.AwaitingClientConfirmation
            and not AgreementExchangeStatus.AwaitingEmployeeResponse)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AgreementExchangeCannotBeFinallyRefusedNow]);
        }

        Status = AgreementExchangeStatus.FinallyRefused;
        FinalRefusedByEmployeeId = employee.Id;
        FinalRefusedAt = refusedAt;
        FinalRefusalReason = reason;

        return UnitResult.Success<IReadOnlyList<Error>>();
    }

    private AgreementProposalVersion GetNextVersion()
    {
        if (_proposals.Count == 0)
        {
            return AgreementProposalVersion.First;
        }

        var maxVersion = _proposals.Max(x => x.Version.Value);

        return new AgreementProposalVersion(maxVersion + 1);
    }

    private AgreementProposal GetActiveProposal()
    {
        return _proposals.Single(x => x.Version == ActiveProposalVersion);
    }
}
