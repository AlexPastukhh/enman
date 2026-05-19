using System.Data;
using System.Net;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementProposalAcceptIntegrationTests : AppIntegrationTestBase
{
    public AgreementProposalAcceptIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Accept_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().PostAsync(
            "/api/agreement-exchanges/1/accept",
            content: null);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Accept_without_csrf_returns_bad_request()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await client.PostAsync(
            "/api/agreement-exchanges/1/accept",
            content: null);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Employee_cannot_accept_client_proposal_endpoint()
    {
        const long employeeId = 820;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee-820@example.com", role: "Employee");

        var response = await AcceptProposalRequestAsync(employeeClient, exchangeId: 1);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Client_accepts_own_active_employee_proposal()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: 77);
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await AcceptProposalRequestAsync(client, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("Accepted");
        exchange.ActiveProposalVersion.Should().Be(1);

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().ContainSingle();
        proposals[0].Version.Should().Be(1);
        proposals[0].Sender.Should().Be("Employee");
        proposals[0].State.Should().Be("Accepted");
    }

    [Fact]
    public async Task Client_accept_does_not_create_new_proposal_version()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: 77);
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");
        var before = await GetProposalRowsAsync(exchangeId);

        var response = await AcceptProposalRequestAsync(client, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var after = await GetProposalRowsAsync(exchangeId);
        after.Should().HaveCount(before.Count);
        after.Select(x => x.Version).Should().Equal(before.Select(x => x.Version));
    }

    [Fact]
    public async Task Client_cannot_accept_another_client_exchange()
    {
        var owner = await RegisterAccountAsync();
        var ownerRequestId = await CreateApprovedRequestForAccountAsync(owner.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            ownerRequestId,
            owner.AccountId,
            employeeSenderId: 77);

        var other = await RegisterAccountAsync();
        var client = AuthenticatedClient(other.AccountId, other.Email, role: "Client");

        var response = await AcceptProposalRequestAsync(client, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingClientConfirmation");

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().ContainSingle();
        proposals[0].State.Should().Be("AwaitingClientConfirmation");
    }

    [Fact]
    public async Task Client_cannot_accept_when_awaiting_employee_response()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await AcceptProposalRequestAsync(client, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingEmployeeResponse");
        exchange.ActiveProposalVersion.Should().Be(2);

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().HaveCount(2);
        proposals[1].State.Should().Be("SentByClient");
    }

    [Fact]
    public async Task Client_cannot_accept_already_accepted_exchange()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertAcceptedExchangeAsync(
            requestId,
            account.AccountId,
            employeeSenderId: 77);
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await AcceptProposalRequestAsync(client, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("Accepted");

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().ContainSingle();
        proposals[0].State.Should().Be("Accepted");
    }

    private Task<HttpResponseMessage> AcceptProposalRequestAsync(
        HttpClient client,
        long exchangeId)
    {
        return PostWithCsrfAsync(client, $"/api/agreement-exchanges/{exchangeId}/accept");
    }

    private async Task<long> CreateApprovedRequestForAccountAsync(long accountId)
    {
        var applicant = await CreateApplicantPartyAsync(accountId);
        await CreateConnectionRequestAsync(accountId, applicant.ApplicantPartyId);
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicant.ApplicantPartyId)
            ?? throw new InvalidOperationException("Could not find created request.");

        await UpdateRequestReviewAsync(
            request.Id,
            "Approved",
            "Approved",
            DateTimeOffset.UtcNow,
            rejectionReason: null);

        return request.Id;
    }

    private async Task<long> InsertExchangeWithSingleEmployeeProposalAsync(
        long requestId,
        long clientAccountId,
        long employeeSenderId)
    {
        var createdAt = DateTimeOffset.UtcNow;
        var exchangeId = await InsertExchangeAsync(
            requestId,
            clientAccountId,
            status: "AwaitingClientConfirmation",
            activeProposalVersion: 1,
            createdAt);

        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 1,
            sender: "Employee",
            senderId: employeeSenderId,
            state: "AwaitingClientConfirmation",
            createdAt: createdAt.AddMinutes(1));

        return exchangeId;
    }

    private async Task<long> InsertExchangeAwaitingEmployeeAsync(
        long requestId,
        long clientAccountId,
        long clientSenderId)
    {
        var createdAt = DateTimeOffset.UtcNow;
        var exchangeId = await InsertExchangeAsync(
            requestId,
            clientAccountId,
            status: "AwaitingEmployeeResponse",
            activeProposalVersion: 2,
            createdAt);

        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 1,
            sender: "Employee",
            senderId: 77,
            state: "SupersededByCounterProposal",
            createdAt: createdAt.AddMinutes(1));

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 2,
            sender: "Client",
            senderId: clientSenderId,
            state: "SentByClient",
            createdAt: createdAt.AddMinutes(2));

        return exchangeId;
    }

    private async Task<long> InsertAcceptedExchangeAsync(
        long requestId,
        long clientAccountId,
        long employeeSenderId)
    {
        var createdAt = DateTimeOffset.UtcNow;
        var exchangeId = await InsertExchangeAsync(
            requestId,
            clientAccountId,
            status: "Accepted",
            activeProposalVersion: 1,
            createdAt);

        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 1,
            sender: "Employee",
            senderId: employeeSenderId,
            state: "Accepted",
            createdAt: createdAt.AddMinutes(1));

        return exchangeId;
    }

    private async Task<long> InsertExchangeAsync(
        long requestId,
        long clientAccountId,
        string status,
        int activeProposalVersion,
        DateTimeOffset createdAt)
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            """
            INSERT INTO dbo.L1AgreementProposalExchanges
                (RequestId, ClientAccountId, Status, ActiveProposalVersion,
                 FinalRefusedByEmployeeId, FinalRefusedAt, FinalRefusalReason, CreatedAt)
            OUTPUT INSERTED.Id
            VALUES
                (@requestId, @clientAccountId, @status, @activeProposalVersion,
                 NULL, NULL, NULL, @createdAt)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        command.Parameters.AddWithValue("@requestId", requestId);
        command.Parameters.AddWithValue("@clientAccountId", clientAccountId);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@activeProposalVersion", activeProposalVersion);
        command.Parameters.AddWithValue("@createdAt", createdAt);

        return (long)(await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not insert agreement exchange."));
    }

    private static async Task InsertProposalAsync(
        SqlConnection connection,
        long exchangeId,
        int version,
        string sender,
        long senderId,
        string state,
        DateTimeOffset createdAt)
    {
        await using var command = new SqlCommand(
            """
            INSERT INTO dbo.L1AgreementProposals
                (AgreementProposalExchangeId, Version, Sender, SenderId, State,
                 DocumentStorageKey, DocumentOriginalFileName, DocumentContentType, DocumentSizeBytes,
                 Comment, CreatedAt)
            VALUES
                (@exchangeId, @version, @sender, @senderId, @state,
                 @storageKey, @originalFileName, N'application/pdf', @sizeBytes,
                 @comment, @createdAt)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        command.Parameters.AddWithValue("@exchangeId", exchangeId);
        command.Parameters.AddWithValue("@version", version);
        command.Parameters.AddWithValue("@sender", sender);
        command.Parameters.AddWithValue("@senderId", senderId);
        command.Parameters.AddWithValue("@state", state);
        command.Parameters.AddWithValue("@storageKey", $"agreements/{exchangeId}-{version}.pdf");
        command.Parameters.AddWithValue("@originalFileName", $"agreement-{exchangeId}-{version}.pdf");
        command.Parameters.AddWithValue("@sizeBytes", 4096L * version);
        command.Parameters.AddWithValue("@comment", $"Proposal {version}");
        command.Parameters.AddWithValue("@createdAt", createdAt);

        await command.ExecuteNonQueryAsync();
    }

    private async Task<ExchangeRow?> GetExchangeRowAsync(long exchangeId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, Status, ActiveProposalVersion
            FROM dbo.L1AgreementProposalExchanges
            WHERE Id = @id
            """,
            exchangeId);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new ExchangeRow(
            reader.GetInt64("Id"),
            reader.GetString("Status"),
            reader.GetInt32("ActiveProposalVersion"));
    }

    private async Task<IReadOnlyList<ProposalRow>> GetProposalRowsAsync(long exchangeId)
    {
        var proposals = new List<ProposalRow>();

        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Version, Sender, SenderId, State
            FROM dbo.L1AgreementProposals
            WHERE AgreementProposalExchangeId = @id
            ORDER BY Version
            """,
            exchangeId);

        while (await reader.ReadAsync())
        {
            proposals.Add(new ProposalRow(
                reader.GetInt32("Version"),
                reader.GetString("Sender"),
                reader.GetInt64("SenderId"),
                reader.GetString("State")));
        }

        return proposals;
    }

    private sealed record ExchangeRow(
        long Id,
        string Status,
        int ActiveProposalVersion);

    private sealed record ProposalRow(
        int Version,
        string Sender,
        long SenderId,
        string State);
}
