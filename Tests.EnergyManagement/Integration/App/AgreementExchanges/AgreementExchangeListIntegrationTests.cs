using System.Data;
using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementExchangeListIntegrationTests : AppIntegrationTestBase
{
    public AgreementExchangeListIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task List_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/agreement-exchanges");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task List_for_client_returns_only_client_owned_exchanges()
    {
        var firstAccount = await RegisterAccountAsync();
        var firstRequestId = await CreateRequestForAccountAsync(firstAccount.AccountId);
        var firstExchangeId = await InsertAgreementExchangeAsync(
            firstRequestId,
            firstAccount.AccountId,
            sender: "Employee",
            senderId: 77);

        var secondAccount = await RegisterAccountAsync();
        var secondRequestId = await CreateRequestForAccountAsync(secondAccount.AccountId);
        await InsertAgreementExchangeAsync(
            secondRequestId,
            secondAccount.AccountId,
            sender: "Employee",
            senderId: 78);

        var client = AuthenticatedClient(firstAccount.AccountId, firstAccount.Email, role: "Client");

        var result = await GetAgreementExchangesAsync(client);

        result.Exchanges.Should().ContainSingle();
        var exchange = result.Exchanges.Single();
        exchange.ExchangeId.Should().Be(firstExchangeId);
        exchange.RequestId.Should().Be(firstRequestId);
        exchange.ExchangeStatus.Should().Be("AwaitingClientConfirmation");
        exchange.ActiveProposalVersion.Should().Be(1);
        exchange.ActiveProposalSender.Should().Be("Employee");
        exchange.ActiveProposalSenderId.Should().Be(77);
        exchange.RequestDisplayName.Should().NotBeNullOrWhiteSpace();
        exchange.ObjectAddress.Should().Contain(City);
        exchange.LastActivityAt.Should().NotBeNull();
    }

    [Fact]
    public async Task List_for_employee_returns_employee_visible_exchanges()
    {
        var firstAccount = await RegisterAccountAsync();
        var firstRequestId = await CreateRequestForAccountAsync(firstAccount.AccountId);
        var firstExchangeId = await InsertAgreementExchangeAsync(
            firstRequestId,
            firstAccount.AccountId,
            sender: "Employee",
            senderId: 77);

        var secondAccount = await RegisterAccountAsync();
        var secondRequestId = await CreateRequestForAccountAsync(secondAccount.AccountId);
        var secondExchangeId = await InsertAgreementExchangeAsync(
            secondRequestId,
            secondAccount.AccountId,
            sender: "Employee",
            senderId: 78);

        const long employeeId = 500;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-500@example.com", role: "Employee");

        var result = await GetAgreementExchangesAsync(client);

        result.Exchanges.Select(x => x.ExchangeId)
            .Should()
            .Contain(new[] { firstExchangeId, secondExchangeId });
    }

    [Fact]
    public async Task List_for_inactive_employee_returns_validation_problem()
    {
        const long employeeId = 501;
        await InsertEmployeeAsync(employeeId, isActive: false);
        var client = AuthenticatedClient(employeeId, "employee-501@example.com", role: "Employee");

        var response = await client.GetAsync("/api/agreement-exchanges");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task List_with_status_filter_returns_matching_exchanges()
    {
        var account = await RegisterAccountAsync();
        var firstRequestId = await CreateRequestForAccountAsync(account.AccountId);
        var firstExchangeId = await InsertAgreementExchangeAsync(
            firstRequestId,
            account.AccountId,
            status: "AwaitingClientConfirmation",
            sender: "Employee",
            senderId: 77);

        var secondRequestId = await CreateRequestForAccountAsync(account.AccountId);
        await InsertAgreementExchangeAsync(
            secondRequestId,
            account.AccountId,
            status: "AwaitingEmployeeResponse",
            activeProposalVersion: 2,
            sender: "Client",
            senderId: account.AccountId,
            proposalState: "SentByClient");

        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var result = await GetAgreementExchangesAsync(client, "AwaitingClientConfirmation");

        result.Exchanges.Should().ContainSingle();
        result.Exchanges.Single().ExchangeId.Should().Be(firstExchangeId);
    }

    [Fact]
    public async Task List_with_invalid_status_returns_validation_problem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await client.GetAsync("/api/agreement-exchanges?status=Unknown");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    private async Task<long> CreateRequestForAccountAsync(long accountId)
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

    private async Task<AgreementExchangeListResponseDto> GetAgreementExchangesAsync(
        HttpClient client,
        string? status = null)
    {
        var path = string.IsNullOrWhiteSpace(status)
            ? "/api/agreement-exchanges"
            : $"/api/agreement-exchanges?status={Uri.EscapeDataString(status)}";

        var response = await client.GetAsync(path);
        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<AgreementExchangeListResponseDto>()
            ?? throw new InvalidOperationException("Agreement exchange list response body was empty.");
    }

    private async Task<long> InsertAgreementExchangeAsync(
        long requestId,
        long clientAccountId,
        string status = "AwaitingClientConfirmation",
        int activeProposalVersion = 1,
        string sender = "Employee",
        long senderId = 77,
        string proposalState = "AwaitingClientConfirmation")
    {
        var createdAt = DateTimeOffset.UtcNow;
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var exchangeCommand = new SqlCommand(
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

        exchangeCommand.Parameters.AddWithValue("@requestId", requestId);
        exchangeCommand.Parameters.AddWithValue("@clientAccountId", clientAccountId);
        exchangeCommand.Parameters.AddWithValue("@status", status);
        exchangeCommand.Parameters.AddWithValue("@activeProposalVersion", activeProposalVersion);
        exchangeCommand.Parameters.AddWithValue("@createdAt", createdAt);

        var exchangeId = (long)(await exchangeCommand.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not insert agreement exchange."));

        await using var proposalCommand = new SqlCommand(
            """
            INSERT INTO dbo.L1AgreementProposals
                (AgreementProposalExchangeId, Version, Sender, SenderId, State,
                 DocumentStorageKey, DocumentOriginalFileName, DocumentContentType, DocumentSizeBytes,
                 Comment, CreatedAt)
            VALUES
                (@exchangeId, @version, @sender, @senderId, @state,
                 @storageKey, @originalFileName, @contentType, @sizeBytes,
                 @comment, @createdAt)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        proposalCommand.Parameters.AddWithValue("@exchangeId", exchangeId);
        proposalCommand.Parameters.AddWithValue("@version", activeProposalVersion);
        proposalCommand.Parameters.AddWithValue("@sender", sender);
        proposalCommand.Parameters.AddWithValue("@senderId", senderId);
        proposalCommand.Parameters.AddWithValue("@state", proposalState);
        proposalCommand.Parameters.AddWithValue("@storageKey", $"agreements/{exchangeId}-{activeProposalVersion}.pdf");
        proposalCommand.Parameters.AddWithValue("@originalFileName", $"agreement-{exchangeId}-{activeProposalVersion}.pdf");
        proposalCommand.Parameters.AddWithValue("@contentType", "application/pdf");
        proposalCommand.Parameters.AddWithValue("@sizeBytes", 4096L);
        proposalCommand.Parameters.AddWithValue("@comment", DBNull.Value);
        proposalCommand.Parameters.AddWithValue("@createdAt", createdAt.AddMinutes(1));

        await proposalCommand.ExecuteNonQueryAsync();

        return exchangeId;
    }
}
