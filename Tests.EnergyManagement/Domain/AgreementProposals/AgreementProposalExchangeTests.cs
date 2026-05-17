using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.L1Domain;
using Tests.EnergyManagement.TestHelpers.L1;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.AgreementProposals;

public class AgreementProposalExchangeTests
{
    [Fact]
    public void StartByEmployee_creates_exchange_with_first_employee_proposal()
    {
        var request = CreateApprovedRequest();
        var document = CreateDocument();
        var employee = CreatePersistedEmployee(id: 7);
        var startedAt = DateTimeOffset.UtcNow;

        var result = AgreementProposalExchange.StartByEmployee(
            request,
            ClientAccountId,
            document,
            null,
            employee,
            startedAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.RequestId.Should().Be(request.Id);
        result.Value.ClientAccountId.Should().Be(ClientAccountId);
        result.Value.Status.Should().Be(AgreementExchangeStatus.AwaitingClientConfirmation);
        result.Value.ActiveProposalVersion.Should().Be(AgreementProposalVersion.First);
        result.Value.CreatedAt.Should().Be(startedAt);
        result.Value.Proposals.Should().ContainSingle();

        var activeProposal = result.Value.Proposals.Single();
        activeProposal.Version.Should().Be(AgreementProposalVersion.First);
        activeProposal.Author.Sender.Should().Be(AgreementProposalSender.Employee);
        activeProposal.Author.SenderId.Should().Be(employee.Id);
        activeProposal.State.Should().Be(AgreementProposalState.AwaitingClientConfirmation);
        activeProposal.Document.Should().Be(document);
    }

    [Fact]
    public void StartByEmployee_fails_when_request_is_not_approved()
    {
        var result = AgreementProposalExchange.StartByEmployee(
            CreateInReviewRequest().WithId(100),
            ClientAccountId,
            CreateDocument(),
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementExchangeRequiresApprovedRequest);
    }

    [Fact]
    public void StartByEmployee_fails_when_document_is_missing()
    {
        var result = AgreementProposalExchange.StartByEmployee(
            CreateApprovedRequest(),
            ClientAccountId,
            null!,
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentIsRequired);
    }

    [Fact]
    public void StartByEmployee_fails_when_employee_is_missing()
    {
        var result = AgreementProposalExchange.StartByEmployee(
            CreateApprovedRequest(),
            ClientAccountId,
            CreateDocument(),
            null,
            null!,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeIsRequired);
    }

    [Fact]
    public void StartByEmployee_fails_without_throwing_when_employee_or_request_is_transient()
    {
        var request = CreateApprovedRequest().WithId(0);
        var employee = CreateEmployee();

        var result = AgreementProposalExchange.StartByEmployee(
            request,
            ClientAccountId,
            CreateDocument(),
            null,
            employee,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.EmployeeIsRequired);
    }

    [Fact]
    public void ClientAcceptActiveProposal_accepts_employee_proposal()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();

        var result = exchange.ClientAcceptActiveProposal(
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        result.IsSuccess.Should().BeTrue();
        exchange.Status.Should().Be(AgreementExchangeStatus.Accepted);
        exchange.Proposals.Single().State.Should().Be(AgreementProposalState.Accepted);
    }

    [Fact]
    public void ClientAcceptActiveProposal_fails_when_exchange_is_not_awaiting_client_confirmation()
    {
        var exchange = CreateAcceptedExchange();
        var activeProposal = exchange.Proposals.Single();

        var result = exchange.ClientAcceptActiveProposal(
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.OnlyAwaitingClientConfirmationCanBeAccepted);
        exchange.Status.Should().Be(AgreementExchangeStatus.Accepted);
        activeProposal.State.Should().Be(AgreementProposalState.Accepted);
    }

    [Fact]
    public void ClientSendOwnVersion_supersedes_employee_proposal_and_creates_client_version()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var originalProposal = exchange.Proposals.Single();

        var result = exchange.ClientSendOwnVersion(
            CreateDocument("client.pdf"),
            ProposalComment.Create("Client version.").Value,
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        result.IsSuccess.Should().BeTrue();
        originalProposal.State.Should().Be(AgreementProposalState.SupersededByCounterProposal);
        exchange.Status.Should().Be(AgreementExchangeStatus.AwaitingEmployeeResponse);
        exchange.ActiveProposalVersion.Value.Should().Be(2);
        exchange.Proposals.Should().HaveCount(2);

        var activeProposal = exchange.Proposals.Single(x => x.Version == exchange.ActiveProposalVersion);
        activeProposal.Author.Sender.Should().Be(AgreementProposalSender.Client);
        activeProposal.Author.SenderId.Should().Be(ClientAccountId);
        activeProposal.State.Should().Be(AgreementProposalState.SentByClient);
    }

    [Fact]
    public void ClientSendOwnVersion_fails_when_exchange_is_not_awaiting_client_confirmation()
    {
        var exchange = CreateAwaitingEmployeeResponseExchange();
        var before = Snapshot(exchange);

        var result = exchange.ClientSendOwnVersion(
            CreateDocument("client-second.pdf"),
            null,
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientVersionCanBeSentOnlyWhenAwaitingClientConfirmation);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void ClientSendOwnVersion_fails_when_document_is_missing()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.ClientSendOwnVersion(
            null!,
            null,
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentIsRequired);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void ClientSendOwnVersion_fails_without_partial_mutation_when_client_is_invalid()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.ClientSendOwnVersion(
            CreateDocument("client.pdf"),
            null,
            CreateClient(),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientAccountIsRequired);
        AssertSnapshot(exchange, before);
    }


    [Fact]
    public void StartByEmployee_fails_when_client_account_id_is_missing()
    {
        var result = AgreementProposalExchange.StartByEmployee(
            CreateApprovedRequest(),
            0,
            CreateDocument(),
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientAccountIsRequired);
    }

    [Fact]
    public void ClientSendOwnVersion_fails_when_client_does_not_belong_to_exchange()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.ClientSendOwnVersion(
            CreateDocument("client.pdf"),
            null,
            CreatePersistedClient(id: ClientAccountId + 100),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientCannotActOnThisAgreementExchange);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void ClientAcceptActiveProposal_fails_when_client_does_not_belong_to_exchange()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.ClientAcceptActiveProposal(
            CreatePersistedClient(id: ClientAccountId + 100),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientCannotActOnThisAgreementExchange);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void EmployeeSendNewVersion_supersedes_client_proposal_and_creates_employee_version()
    {
        var exchange = CreateAwaitingEmployeeResponseExchange();
        var clientProposal = exchange.Proposals.Single(x => x.Version.Value == 2);

        var result = exchange.EmployeeSendNewVersion(
            CreateDocument("employee-second.pdf"),
            ProposalComment.Create("Employee version.").Value,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsSuccess.Should().BeTrue();
        clientProposal.State.Should().Be(AgreementProposalState.SupersededByCounterProposal);
        exchange.Status.Should().Be(AgreementExchangeStatus.AwaitingClientConfirmation);
        exchange.ActiveProposalVersion.Value.Should().Be(3);
        exchange.Proposals.Should().HaveCount(3);

        var activeProposal = exchange.Proposals.Single(x => x.Version == exchange.ActiveProposalVersion);
        activeProposal.Author.Sender.Should().Be(AgreementProposalSender.Employee);
        activeProposal.Author.SenderId.Should().Be(7);
        activeProposal.State.Should().Be(AgreementProposalState.AwaitingClientConfirmation);
    }

    [Fact]
    public void EmployeeSendNewVersion_fails_when_exchange_is_not_awaiting_employee_response()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.EmployeeSendNewVersion(
            CreateDocument("employee-second.pdf"),
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeCanRespondOnlyWhenAwaitingEmployeeResponse);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void EmployeeSendNewVersion_fails_when_document_is_missing()
    {
        var exchange = CreateAwaitingEmployeeResponseExchange();
        var before = Snapshot(exchange);

        var result = exchange.EmployeeSendNewVersion(
            null!,
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentIsRequired);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void EmployeeSendNewVersion_fails_without_partial_mutation_when_employee_is_invalid()
    {
        var exchange = CreateAwaitingEmployeeResponseExchange();
        var before = Snapshot(exchange);

        var result = exchange.EmployeeSendNewVersion(
            CreateDocument("employee-second.pdf"),
            null,
            CreateEmployee(),
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeIsRequired);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void FinalRefuseProposal_succeeds_from_awaiting_client_confirmation()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var reason = FinalRefusalReason.Create("Cannot conclude agreement.").Value;
        var refusedAt = DateTimeOffset.UtcNow;
        var activeVersion = exchange.ActiveProposalVersion;
        var proposalCount = exchange.Proposals.Count;

        var result = exchange.FinalRefuseProposal(
            CreatePersistedEmployee(id: 7),
            reason,
            refusedAt);

        result.IsSuccess.Should().BeTrue();
        exchange.Status.Should().Be(AgreementExchangeStatus.FinallyRefused);
        exchange.FinalRefusedByEmployeeId.Should().Be(7);
        exchange.FinalRefusedAt.Should().Be(refusedAt);
        exchange.FinalRefusalReason.Should().Be(reason);
        exchange.ActiveProposalVersion.Should().Be(activeVersion);
        exchange.Proposals.Should().HaveCount(proposalCount);
    }

    [Fact]
    public void FinalRefuseProposal_succeeds_from_awaiting_employee_response_and_accepts_null_reason()
    {
        var exchange = CreateAwaitingEmployeeResponseExchange();

        var result = exchange.FinalRefuseProposal(
            CreatePersistedEmployee(id: 7),
            null,
            DateTimeOffset.UtcNow);

        result.IsSuccess.Should().BeTrue();
        exchange.Status.Should().Be(AgreementExchangeStatus.FinallyRefused);
        exchange.FinalRefusalReason.Should().BeNull();
    }

    [Fact]
    public void FinalRefuseProposal_fails_when_exchange_is_accepted()
    {
        var exchange = CreateAcceptedExchange();
        var before = Snapshot(exchange);

        var result = exchange.FinalRefuseProposal(
            CreatePersistedEmployee(id: 7),
            FinalRefusalReason.Create("No.").Value,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AcceptedAgreementExchangeCannotBeRefused);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void FinalRefuseProposal_fails_when_exchange_is_already_finally_refused()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        exchange.FinalRefuseProposal(
            CreatePersistedEmployee(id: 7),
            null,
            DateTimeOffset.UtcNow);
        var before = Snapshot(exchange);

        var result = exchange.FinalRefuseProposal(
            CreatePersistedEmployee(id: 7),
            null,
            DateTimeOffset.UtcNow.AddMinutes(1));

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementExchangeAlreadyFinallyRefused);
        AssertSnapshot(exchange, before);
    }

    [Fact]
    public void FinalRefuseProposal_fails_without_partial_mutation_when_employee_is_invalid()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        var before = Snapshot(exchange);

        var result = exchange.FinalRefuseProposal(
            CreateEmployee(),
            null,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeIsRequired);
        AssertSnapshot(exchange, before);
    }

    private const long ClientAccountId = 10;

    private static AgreementProposalExchange CreateAwaitingClientConfirmationExchange()
    {
        return AgreementProposalExchange.StartByEmployee(
            CreateApprovedRequest(),
            ClientAccountId,
            CreateDocument(),
            null,
            CreatePersistedEmployee(id: 7),
            DateTimeOffset.UtcNow).Value;
    }

    private static AgreementProposalExchange CreateAwaitingEmployeeResponseExchange()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        exchange.ClientSendOwnVersion(
            CreateDocument("client.pdf"),
            null,
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        return exchange;
    }

    private static AgreementProposalExchange CreateAcceptedExchange()
    {
        var exchange = CreateAwaitingClientConfirmationExchange();
        exchange.ClientAcceptActiveProposal(
            CreatePersistedClient(id: ClientAccountId),
            DateTimeOffset.UtcNow);

        return exchange;
    }

    private static ConnectionRequest CreateApprovedRequest()
    {
        var request = CreateInReviewRequest().WithId(100);
        var employee = CreatePersistedEmployee(id: 7);

        request.StartReview(employee, DateTimeOffset.UtcNow);
        request.ApproveReview(employee, DateTimeOffset.UtcNow.AddMinutes(1));

        return request;
    }

    private static ConnectionRequest CreateInReviewRequest()
    {
        return ConnectionRequest.Create(
            CreatePersistedApplicant(),
            L1ValidTestData.RequestDetails,
            L1ValidTestData.Address).Value;
    }

    private static IndividualApplicantParty CreatePersistedApplicant()
    {
        return IndividualApplicantParty.Create(
            clientAccountId: 10,
            L1ValidTestData.FullName,
            L1ValidTestData.Email,
            L1ValidTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value.WithId(42);
    }

    private static Employee CreateEmployee()
    {
        return Employee.Create(
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow).Value;
    }

    private static Employee CreatePersistedEmployee(long id)
    {
        return CreateEmployee().WithId(id);
    }

    private static ClientAccount CreateClient()
    {
        return ClientAccount.Register(
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
            DateTimeOffset.UtcNow).Value;
    }

    private static ClientAccount CreatePersistedClient(long id)
    {
        return CreateClient().WithId(id);
    }

    private static AgreementDocumentRef CreateDocument(string fileName = "agreement.pdf")
    {
        return AgreementDocumentRef.Create(
            $"agreements/{fileName}",
            fileName,
            "application/pdf",
            100).Value;
    }

    private static ExchangeSnapshot Snapshot(AgreementProposalExchange exchange)
    {
        return new ExchangeSnapshot(
            exchange.Status,
            exchange.ActiveProposalVersion,
            exchange.FinalRefusedByEmployeeId,
            exchange.FinalRefusedAt,
            exchange.FinalRefusalReason,
            exchange.Proposals.Count,
            exchange.Proposals.Select(x => (x.Version, x.State)).ToArray());
    }

    private static void AssertSnapshot(
        AgreementProposalExchange exchange,
        ExchangeSnapshot snapshot)
    {
        exchange.Status.Should().Be(snapshot.Status);
        exchange.ActiveProposalVersion.Should().Be(snapshot.ActiveProposalVersion);
        exchange.FinalRefusedByEmployeeId.Should().Be(snapshot.FinalRefusedByEmployeeId);
        exchange.FinalRefusedAt.Should().Be(snapshot.FinalRefusedAt);
        exchange.FinalRefusalReason.Should().Be(snapshot.FinalRefusalReason);
        exchange.Proposals.Should().HaveCount(snapshot.ProposalCount);
        exchange.Proposals.Select(x => (x.Version, x.State)).Should().Equal(snapshot.Proposals);
    }

    private sealed record ExchangeSnapshot(
        AgreementExchangeStatus Status,
        AgreementProposalVersion ActiveProposalVersion,
        long? FinalRefusedByEmployeeId,
        DateTimeOffset? FinalRefusedAt,
        FinalRefusalReason? FinalRefusalReason,
        int ProposalCount,
        IReadOnlyCollection<(AgreementProposalVersion Version, AgreementProposalState State)> Proposals);
}
