using System.Data;
using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.L1.Api;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementExchangeDetailsIntegrationTests : L1IntegrationTestBase
{
    public AgreementExchangeDetailsIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Details_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/agreement-exchanges/1");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Details_for_client_returns_own_exchange_with_proposal_history()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertAgreementExchangeWithHistoryAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var details = await GetAgreementExchangeDetailsAsync(client, exchangeId);

        details.ExchangeId.Should().Be(exchangeId);
        details.RequestId.Should().Be(requestId);
        details.ExchangeStatus.Should().Be("AwaitingEmployeeResponse");
        details.ActiveProposalVersion.Should().Be(2);
        details.CurrentActorSide.Should().Be("Client");
        details.Request.RequestId.Should().Be(requestId);
        details.Request.RequestStatus.Should().Be("Approved");
        details.Request.RequestDisplayName.Should().NotBeNullOrWhiteSpace();
        details.Request.ObjectAddress.Should().Contain(City);
        details.LastActivityAt.Should().NotBeNull();

        details.ActiveProposal.Version.Should().Be(2);
        details.ActiveProposal.Sender.Should().Be("Client");
        details.ActiveProposal.SenderId.Should().Be(account.AccountId);
        details.ActiveProposal.State.Should().Be("SentByClient");
        details.ActiveProposal.Comment.Should().Be("Client counter proposal");
        details.ActiveProposal.Document.StorageKey.Should().Contain($"agreements/{exchangeId}-2.pdf");
        details.ActiveProposal.Document.OriginalFileName.Should().Be($"agreement-{exchangeId}-2.pdf");
        details.ActiveProposal.Document.ContentType.Should().Be("application/pdf");
        details.ActiveProposal.Document.SizeBytes.Should().Be(8192L);

        details.Proposals.Select(x => x.Version).Should().Equal(1, 2);
        details.Proposals[0].Sender.Should().Be("Employee");
        details.Proposals[0].State.Should().Be("SupersededByCounterProposal");
        details.Proposals[1].Sender.Should().Be("Client");
    }

    [Fact]
    public async Task Details_for_client_does_not_return_another_client_exchange()
    {
        var owner = await RegisterAccountAsync();
        var ownerRequestId = await CreateApprovedRequestForAccountAsync(owner.AccountId);
        var exchangeId = await InsertAgreementExchangeWithHistoryAsync(
            ownerRequestId,
            owner.AccountId,
            clientSenderId: owner.AccountId);

        var other = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(other.AccountId, other.Email, role: "Client");

        var response = await client.GetAsync($"/api/agreement-exchanges/{exchangeId}");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Details_for_employee_returns_visible_exchange()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertAgreementExchangeWithHistoryAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        const long employeeId = 700;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedL1Client(employeeId, "employee-700@example.com", role: "Employee");

        var details = await GetAgreementExchangeDetailsAsync(client, exchangeId);

        details.ExchangeId.Should().Be(exchangeId);
        details.CurrentActorSide.Should().Be("Employee");
        details.Proposals.Should().HaveCount(2);
        details.ActiveProposal.Version.Should().Be(2);
    }

    [Fact]
    public async Task Details_for_inactive_employee_returns_validation_problem()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var exchangeId = await InsertAgreementExchangeWithHistoryAsync(
            requestId,
            account.AccountId,
            clientSenderId: account.AccountId);

        const long employeeId = 701;
        await InsertEmployeeAsync(employeeId, isActive: false);
        var client = AuthenticatedL1Client(employeeId, "employee-701@example.com", role: "Employee");

        var response = await client.GetAsync($"/api/agreement-exchanges/{exchangeId}");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Details_for_missing_exchange_returns_not_found()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");

        var response = await client.GetAsync("/api/agreement-exchanges/999999");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
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

    private async Task<AgreementExchangeDetailsResponseDto> GetAgreementExchangeDetailsAsync(
        HttpClient client,
        long exchangeId)
    {
        var response = await client.GetAsync($"/api/agreement-exchanges/{exchangeId}");
        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<AgreementExchangeDetailsResponseDto>()
            ?? throw new InvalidOperationException("Agreement exchange details response body was empty.");
    }

    private async Task<long> InsertAgreementExchangeWithHistoryAsync(
        long requestId,
        long clientAccountId,
        long clientSenderId)
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
                (@requestId, @clientAccountId, N'AwaitingEmployeeResponse', 2,
                 NULL, NULL, NULL, @createdAt)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        exchangeCommand.Parameters.AddWithValue("@requestId", requestId);
        exchangeCommand.Parameters.AddWithValue("@clientAccountId", clientAccountId);
        exchangeCommand.Parameters.AddWithValue("@createdAt", createdAt);

        var exchangeId = (long)(await exchangeCommand.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not insert agreement exchange."));

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 1,
            sender: "Employee",
            senderId: 77,
            state: "SupersededByCounterProposal",
            comment: "Initial employee proposal",
            createdAt: createdAt.AddMinutes(1));

        await InsertProposalAsync(
            connection,
            exchangeId,
            version: 2,
            sender: "Client",
            senderId: clientSenderId,
            state: "SentByClient",
            comment: "Client counter proposal",
            createdAt: createdAt.AddMinutes(2));

        return exchangeId;
    }

    private static async Task InsertProposalAsync(
        SqlConnection connection,
        long exchangeId,
        int version,
        string sender,
        long senderId,
        string state,
        string? comment,
        DateTimeOffset createdAt)
    {
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
        proposalCommand.Parameters.AddWithValue("@version", version);
        proposalCommand.Parameters.AddWithValue("@sender", sender);
        proposalCommand.Parameters.AddWithValue("@senderId", senderId);
        proposalCommand.Parameters.AddWithValue("@state", state);
        proposalCommand.Parameters.AddWithValue("@storageKey", $"agreements/{exchangeId}-{version}.pdf");
        proposalCommand.Parameters.AddWithValue("@originalFileName", $"agreement-{exchangeId}-{version}.pdf");
        proposalCommand.Parameters.AddWithValue("@contentType", "application/pdf");
        proposalCommand.Parameters.AddWithValue("@sizeBytes", version == 1 ? 4096L : 8192L);
        proposalCommand.Parameters.AddWithValue("@comment", (object?)comment ?? DBNull.Value);
        proposalCommand.Parameters.AddWithValue("@createdAt", createdAt);

        await proposalCommand.ExecuteNonQueryAsync();
    }
}
