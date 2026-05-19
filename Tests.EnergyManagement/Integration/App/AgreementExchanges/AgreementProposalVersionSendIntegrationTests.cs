using System.Data;
using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Api;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementProposalVersionSendIntegrationTests : AppIntegrationTestBase
{
    public AgreementProposalVersionSendIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Send_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/requests/1/agreement-exchange/proposals",
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Send_without_csrf_returns_bad_request()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await client.PostAsJsonAsync(
            "/api/requests/1/agreement-exchange/proposals",
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Send_with_missing_document_returns_validation_problem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests/1/agreement-exchange/proposals",
            new SendAgreementProposalVersionDto(null, "Missing document."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Client_sends_own_version_for_owned_exchange()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: 77);

        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await SendProposalVersionRequestAsync(
            client,
            requestId,
            ValidDto(comment: "Client counter proposal."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingEmployeeResponse");
        exchange.ActiveProposalVersion.Should().Be(2);

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().HaveCount(2);
        proposals[0].Version.Should().Be(1);
        proposals[0].State.Should().Be("SupersededByCounterProposal");
        proposals[1].Version.Should().Be(2);
        proposals[1].Sender.Should().Be("Client");
        proposals[1].SenderId.Should().Be(account.AccountId);
        proposals[1].State.Should().Be("SentByClient");
        proposals[1].Comment.Should().Be("Client counter proposal.");
        proposals[1].DocumentStorageKey.Should().Be("agreements/request-v2.pdf");
    }

    [Fact]
    public async Task Client_cannot_send_for_another_client_exchange()
    {
        var owner = await RegisterAccountAsync();
        var ownerRequestId = await CreateApprovedRequestForAccountAsync(owner.AccountId);
        await InsertExchangeWithSingleEmployeeProposalAsync(
            ownerRequestId,
            owner.AccountId,
            employeeSenderId: 77);

        var other = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(other.AccountId, other.Email, role: "Client");

        var response = await SendProposalVersionRequestAsync(
            client,
            ownerRequestId,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Employee_sends_new_version_after_client_version()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        const long employeeId = 810;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedL1Client(employeeId, "employee-810@example.com", role: "Employee");

        var response = await SendProposalVersionRequestAsync(
            employeeClient,
            requestId,
            ValidDto(
                storageKey: "agreements/request-v3.pdf",
                originalFileName: "request-v3.pdf",
                sizeBytes: 12288,
                comment: "Employee revised proposal."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingClientConfirmation");
        exchange.ActiveProposalVersion.Should().Be(3);

        var proposals = await GetProposalRowsAsync(exchangeId);
        proposals.Should().HaveCount(3);
        proposals[1].State.Should().Be("SupersededByCounterProposal");
        proposals[2].Version.Should().Be(3);
        proposals[2].Sender.Should().Be("Employee");
        proposals[2].SenderId.Should().Be(employeeId);
        proposals[2].State.Should().Be("AwaitingClientConfirmation");
        proposals[2].Comment.Should().Be("Employee revised proposal.");
    }

    [Fact]
    public async Task Different_active_employee_can_send_employee_version()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId,
            firstEmployeeSenderId: 900);

        const long differentEmployeeId = 901;
        await InsertEmployeeAsync(differentEmployeeId);
        var employeeClient = AuthenticatedL1Client(
            differentEmployeeId,
            "employee-901@example.com",
            role: "Employee");

        var response = await SendProposalVersionRequestAsync(
            employeeClient,
            requestId,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Client_cannot_send_twice_in_a_row()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await SendProposalVersionRequestAsync(
            client,
            requestId,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Inactive_employee_cannot_send_version()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        const long employeeId = 811;
        await InsertEmployeeAsync(employeeId, isActive: false);
        var employeeClient = AuthenticatedL1Client(employeeId, "employee-811@example.com", role: "Employee");

        var response = await SendProposalVersionRequestAsync(
            employeeClient,
            requestId,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    private Task<HttpResponseMessage> SendProposalVersionRequestAsync(
        HttpClient client,
        long requestId,
        SendAgreementProposalVersionDto dto)
    {
        return PostAsJsonWithCsrfAsync(
            client,
            $"/api/requests/{requestId}/agreement-exchange/proposals",
            dto);
    }

    private static SendAgreementProposalVersionDto ValidDto(
        string storageKey = "agreements/request-v2.pdf",
        string originalFileName = "request-v2.pdf",
        string contentType = "application/pdf",
        long sizeBytes = 8192,
        string? comment = "Counter proposal.")
    {
        return new SendAgreementProposalVersionDto(
            new AgreementDocumentRefDto(
                storageKey,
                originalFileName,
                contentType,
                sizeBytes),
            comment);
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
            storageKey: "agreements/request-v1.pdf",
            originalFileName: "request-v1.pdf",
            sizeBytes: 4096,
            comment: "Initial employee proposal.",
            createdAt: createdAt.AddMinutes(1));

        return exchangeId;
    }

    private async Task<long> InsertExchangeAwaitingEmployeeAsync(
        long requestId,
        long clientAccountId,
        long clientSenderId,
        long firstEmployeeSenderId = 77)
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
            senderId: firstEmployeeSenderId,
            state: "SupersededByCounterProposal",
            storageKey: "agreements/request-v1.pdf",
            originalFileName: "request-v1.pdf",
            sizeBytes: 4096,
            comment: "Initial employee proposal.",
            createdAt: createdAt.AddMinutes(1));

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 2,
            sender: "Client",
            senderId: clientSenderId,
            state: "SentByClient",
            storageKey: "agreements/request-v2.pdf",
            originalFileName: "request-v2.pdf",
            sizeBytes: 8192,
            comment: "Client counter proposal.",
            createdAt: createdAt.AddMinutes(2));

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
        string storageKey,
        string originalFileName,
        long sizeBytes,
        string? comment,
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
        command.Parameters.AddWithValue("@storageKey", storageKey);
        command.Parameters.AddWithValue("@originalFileName", originalFileName);
        command.Parameters.AddWithValue("@sizeBytes", sizeBytes);
        command.Parameters.AddWithValue("@comment", (object?)comment ?? DBNull.Value);
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
            SELECT Version, Sender, SenderId, State, DocumentStorageKey, Comment
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
                reader.GetString("State"),
                reader.GetString("DocumentStorageKey"),
                reader.IsDBNull("Comment") ? null : reader.GetString("Comment")));
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
        string State,
        string DocumentStorageKey,
        string? Comment);
}
