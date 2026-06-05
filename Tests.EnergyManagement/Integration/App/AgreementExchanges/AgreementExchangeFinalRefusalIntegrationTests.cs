using System.Data;
using System.Net;
using EnergyManagement.Server.Api;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementExchangeFinalRefusalIntegrationTests : AppIntegrationTestBase
{
    public AgreementExchangeFinalRefusalIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Final_refuse_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().PostAsync(
            "/api/agreement-exchanges/1/final-refuse",
            content: null);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Final_refuse_without_csrf_returns_bad_request()
    {
        const long employeeId = -930;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--930@example.com", role: "Employee");

        var response = await employeeClient.PostAsync(
            "/api/agreement-exchanges/1/final-refuse",
            content: null);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Client_cannot_final_refuse_exchange()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await FinalRefuseRequestAsync(client, exchangeId: 1);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Employee_final_refuses_active_exchange_with_reason()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: -931);

        const long employeeId = -932;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--932@example.com", role: "Employee");

        var beforeProposals = await GetProposalRowsAsync(exchangeId);

        var response = await FinalRefuseRequestAsync(
            employeeClient,
            exchangeId,
            new FinalRefuseAgreementExchangeDto("Final refusal reason."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("FinallyRefused");
        exchange.ActiveProposalVersion.Should().Be(1);
        exchange.FinalRefusedByEmployeeId.Should().Be(employeeId);
        exchange.FinalRefusedAt.Should().NotBeNull();
        exchange.FinalRefusalReason.Should().Be("Final refusal reason.");

        var request = await GetRequestRowAsync(requestId)
            ?? throw new InvalidOperationException("Request row was not found.");
        request.Status.Should().Be("AgreementExchangeFailed");

        var afterProposals = await GetProposalRowsAsync(exchangeId);
        afterProposals.Should().HaveCount(beforeProposals.Count);
        afterProposals.Select(x => x.Version).Should().Equal(beforeProposals.Select(x => x.Version));
        afterProposals[0].State.Should().Be("AwaitingClientConfirmation");
    }

    [Fact]
    public async Task Employee_final_refuses_active_exchange_without_body_and_stores_null_reason()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeAwaitingEmployeeAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        const long employeeId = -933;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--933@example.com", role: "Employee");

        var response = await FinalRefuseRequestAsync(employeeClient, exchangeId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("FinallyRefused");
        exchange.FinalRefusedByEmployeeId.Should().Be(employeeId);
        exchange.FinalRefusalReason.Should().BeNull();

        var request = await GetRequestRowAsync(requestId)
            ?? throw new InvalidOperationException("Request row was not found.");
        request.Status.Should().Be("AgreementExchangeFailed");
    }

    [Fact]
    public async Task Final_refuse_with_blank_reason_returns_validation_problem_and_does_not_mutate()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: -934);

        const long employeeId = -935;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--935@example.com", role: "Employee");

        var response = await FinalRefuseRequestAsync(
            employeeClient,
            exchangeId,
            new FinalRefuseAgreementExchangeDto("   "));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingClientConfirmation");
        exchange.FinalRefusedByEmployeeId.Should().BeNull();
        exchange.FinalRefusedAt.Should().BeNull();
        exchange.FinalRefusalReason.Should().BeNull();

        var request = await GetRequestRowAsync(requestId)
            ?? throw new InvalidOperationException("Request row was not found.");
        request.Status.Should().Be("Approved");
    }

    [Fact]
    public async Task Final_refuse_with_too_long_reason_returns_validation_problem_and_does_not_mutate()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            requestId,
            account.AccountId,
            employeeSenderId: -936);

        const long employeeId = -937;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--937@example.com", role: "Employee");

        var response = await FinalRefuseRequestAsync(
            employeeClient,
            exchangeId,
            new FinalRefuseAgreementExchangeDto(new string('x', 2001)));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingClientConfirmation");
        exchange.FinalRefusedByEmployeeId.Should().BeNull();

        var request = await GetRequestRowAsync(requestId)
            ?? throw new InvalidOperationException("Request row was not found.");
        request.Status.Should().Be("Approved");
    }

    [Fact]
    public async Task Employee_cannot_final_refuse_accepted_exchange()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertAcceptedExchangeAsync(
            requestId,
            account.AccountId,
            employeeSenderId: -938);

        const long employeeId = -939;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--939@example.com", role: "Employee");

        var response = await FinalRefuseRequestAsync(
            employeeClient,
            exchangeId,
            new FinalRefuseAgreementExchangeDto("Not possible."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("Accepted");
        exchange.FinalRefusedByEmployeeId.Should().BeNull();

        var request = await GetRequestRowAsync(requestId)
            ?? throw new InvalidOperationException("Request row was not found.");
        request.Status.Should().Be("Approved");
    }

    [Fact]
    public async Task Employee_cannot_final_refuse_when_related_request_is_not_approved()
    {
        var account = await RegisterAccountAsync();
        var applicant = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, applicant.ApplicantPartyId);
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicant.ApplicantPartyId)
            ?? throw new InvalidOperationException("Could not find created request.");
        var exchangeId = await InsertExchangeWithSingleEmployeeProposalAsync(
            request.Id,
            account.AccountId,
            employeeSenderId: -940);

        const long employeeId = -941;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedClient(employeeId, "employee--941@example.com", role: "Employee");

        var response = await FinalRefuseRequestAsync(
            employeeClient,
            exchangeId,
            new FinalRefuseAgreementExchangeDto("Request is not approved."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);

        var exchange = await GetExchangeRowAsync(exchangeId)
            ?? throw new InvalidOperationException("Exchange row was not found.");
        exchange.Status.Should().Be("AwaitingClientConfirmation");
        exchange.FinalRefusedByEmployeeId.Should().BeNull();

        var unchangedRequest = await GetRequestRowAsync(request.Id)
            ?? throw new InvalidOperationException("Request row was not found.");
        unchangedRequest.Status.Should().Be("InReview");
    }

    private Task<HttpResponseMessage> FinalRefuseRequestAsync(
        HttpClient client,
        long exchangeId)
    {
        return PostWithCsrfAsync(client, $"/api/agreement-exchanges/{exchangeId}/final-refuse");
    }

    private Task<HttpResponseMessage> FinalRefuseRequestAsync(
        HttpClient client,
        long exchangeId,
        FinalRefuseAgreementExchangeDto dto)
    {
        return PostAsJsonWithCsrfAsync(client, $"/api/agreement-exchanges/{exchangeId}/final-refuse", dto);
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
            INSERT INTO dbo.AgreementProposalExchanges
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
            INSERT INTO dbo.AgreementProposals
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
            SELECT Id, Status, ActiveProposalVersion,
                   FinalRefusedByEmployeeId, FinalRefusedAt, FinalRefusalReason
            FROM dbo.AgreementProposalExchanges
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
            reader.GetInt32("ActiveProposalVersion"),
            GetNullableInt64(reader, "FinalRefusedByEmployeeId"),
            GetNullableDateTimeOffset(reader, "FinalRefusedAt"),
            GetNullableString(reader, "FinalRefusalReason"));
    }

    private async Task<IReadOnlyList<ProposalRow>> GetProposalRowsAsync(long exchangeId)
    {
        var proposals = new List<ProposalRow>();

        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Version, Sender, SenderId, State
            FROM dbo.AgreementProposals
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


    private static long? GetNullableInt64(SqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetInt64(ordinal);
    }

    private static DateTimeOffset? GetNullableDateTimeOffset(SqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetDateTimeOffset(ordinal);
    }

    private static string? GetNullableString(SqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
    }

    private sealed record ExchangeRow(
        long Id,
        string Status,
        int ActiveProposalVersion,
        long? FinalRefusedByEmployeeId,
        DateTimeOffset? FinalRefusedAt,
        string? FinalRefusalReason);

    private sealed record ProposalRow(
        int Version,
        string Sender,
        long SenderId,
        string State);
}
