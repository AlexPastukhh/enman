using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.L1.Api;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.AgreementExchanges;

[Collection("IntegrationTestCollection")]
public sealed class AgreementProposalDocumentDownloadIntegrationTests : L1IntegrationTestBase
{
    public AgreementProposalDocumentDownloadIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Download_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().GetAsync(
            "/api/agreement-exchanges/1/proposals/1/document/download");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Client_can_download_own_agreement_proposal_document()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");
        var document = await UploadDocumentAsync(client);
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var (exchangeId, proposalId) = await InsertAgreementExchangeWithDocumentProposalAsync(
            requestId,
            account.AccountId,
            sender: "Client",
            senderId: account.AccountId,
            document);

        var response = await client.GetAsync(
            $"/api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/pdf");
        response.Content.Headers.ContentDisposition?.FileNameStar.Should().Be("agreement.pdf");
        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Should().Equal(ValidPdfBytes);
    }

    [Fact]
    public async Task Other_client_cannot_download_document_from_foreign_exchange()
    {
        var owner = await RegisterAccountAsync();
        var ownerClient = AuthenticatedL1Client(owner.AccountId, owner.Email, role: "Client");
        var document = await UploadDocumentAsync(ownerClient);
        var requestId = await CreateApprovedRequestForAccountAsync(owner.AccountId);
        var (exchangeId, proposalId) = await InsertAgreementExchangeWithDocumentProposalAsync(
            requestId,
            owner.AccountId,
            sender: "Client",
            senderId: owner.AccountId,
            document);
        var other = await RegisterAccountAsync();
        var otherClient = AuthenticatedL1Client(other.AccountId, other.Email, role: "Client");

        var response = await otherClient.GetAsync(
            $"/api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Employee_can_download_visible_agreement_proposal_document()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");
        var document = await UploadDocumentAsync(client);
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var (exchangeId, proposalId) = await InsertAgreementExchangeWithDocumentProposalAsync(
            requestId,
            account.AccountId,
            sender: "Client",
            senderId: account.AccountId,
            document);
        const long employeeId = 740;
        await InsertEmployeeAsync(employeeId);
        var employeeClient = AuthenticatedL1Client(employeeId, "employee-740@example.com", role: "Employee");

        var response = await employeeClient.GetAsync(
            $"/api/agreement-exchanges/{exchangeId}/proposals/{proposalId}/document/download");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Should().Equal(ValidPdfBytes);
    }

    [Fact]
    public async Task Download_for_proposal_not_belonging_to_exchange_returns_not_found()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email, role: "Client");
        var firstDocument = await UploadDocumentAsync(client);
        var firstRequestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var (firstExchangeId, _) = await InsertAgreementExchangeWithDocumentProposalAsync(
            firstRequestId,
            account.AccountId,
            sender: "Client",
            senderId: account.AccountId,
            firstDocument);
        var secondDocument = await UploadDocumentAsync(client, Encoding.UTF8.GetBytes("%PDF-1.4 second document"));
        var secondRequestId = await CreateApprovedRequestForAccountAsync(account.AccountId);
        var (_, secondProposalId) = await InsertAgreementExchangeWithDocumentProposalAsync(
            secondRequestId,
            account.AccountId,
            sender: "Client",
            senderId: account.AccountId,
            secondDocument);

        var response = await client.GetAsync(
            $"/api/agreement-exchanges/{firstExchangeId}/proposals/{secondProposalId}/document/download");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    private async Task<AgreementDocumentRefDto> UploadDocumentAsync(
        HttpClient client,
        byte[]? content = null)
    {
        using var form = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(content ?? ValidPdfBytes);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
        form.Add(fileContent, "document", "agreement.pdf");

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/agreement-proposal-documents")
        {
            Content = form
        };
        request.Headers.Add(
            AntiforgeryConstants.HeaderName,
            await GetAntiforgeryTokenAsync(client));

        var response = await client.SendAsync(request);
        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<AgreementDocumentRefDto>()
            ?? throw new InvalidOperationException("Upload response body was empty.");
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

    private async Task<(long ExchangeId, long ProposalId)> InsertAgreementExchangeWithDocumentProposalAsync(
        long requestId,
        long clientAccountId,
        string sender,
        long senderId,
        AgreementDocumentRefDto document)
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
                (@requestId, @clientAccountId, N'AwaitingEmployeeResponse', 1,
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

        await using var proposalCommand = new SqlCommand(
            """
            INSERT INTO dbo.L1AgreementProposals
                (AgreementProposalExchangeId, Version, Sender, SenderId, State,
                 DocumentStorageKey, DocumentOriginalFileName, DocumentContentType, DocumentSizeBytes,
                 Comment, CreatedAt)
            OUTPUT INSERTED.Id
            VALUES
                (@exchangeId, 1, @sender, @senderId, N'SentByClient',
                 @storageKey, @originalFileName, @contentType, @sizeBytes,
                 N'Downloadable proposal', @createdAt)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        proposalCommand.Parameters.AddWithValue("@exchangeId", exchangeId);
        proposalCommand.Parameters.AddWithValue("@sender", sender);
        proposalCommand.Parameters.AddWithValue("@senderId", senderId);
        proposalCommand.Parameters.AddWithValue("@storageKey", document.StorageKey ?? throw new InvalidOperationException("Storage key missing."));
        proposalCommand.Parameters.AddWithValue("@originalFileName", document.OriginalFileName ?? "agreement.pdf");
        proposalCommand.Parameters.AddWithValue("@contentType", document.ContentType ?? "application/pdf");
        proposalCommand.Parameters.AddWithValue("@sizeBytes", document.SizeBytes);
        proposalCommand.Parameters.AddWithValue("@createdAt", createdAt.AddMinutes(1));

        var proposalId = (long)(await proposalCommand.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not insert agreement proposal."));

        return (exchangeId, proposalId);
    }

    private static readonly byte[] ValidPdfBytes = Encoding.UTF8.GetBytes("%PDF-1.4 test document");
}
