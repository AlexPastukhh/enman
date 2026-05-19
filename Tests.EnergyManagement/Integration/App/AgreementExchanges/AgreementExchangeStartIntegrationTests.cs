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
public sealed class AgreementExchangeStartIntegrationTests : AppIntegrationTestBase
{
    public AgreementExchangeStartIntegrationTests(
        IntegrationTestFixture fixture,
        ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task Start_without_auth_returns_unauthorized()
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/employee/requests/1/agreement-exchange/start",
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Start_as_client_returns_forbidden()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email, role: "Client");

        var response = await StartAgreementExchangeRequestAsync(
            client,
            requestId: 1,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Start_without_csrf_returns_bad_request()
    {
        const long employeeId = 1201;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-1201@example.com", role: "Employee");

        var response = await client.PostAsJsonAsync(
            "/api/employee/requests/1/agreement-exchange/start",
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Start_with_missing_document_returns_validation_problem()
    {
        const long employeeId = 1202;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-1202@example.com", role: "Employee");

        var response = await StartAgreementExchangeRequestAsync(
            client,
            requestId: 1,
            new StartAgreementExchangeDto(null, "Missing document."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Start_for_not_approved_request_returns_lifecycle_problem()
    {
        var account = await RegisterAccountAsync();
        var applicant = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, applicant.ApplicantPartyId);
        var request = await GetLatestRequestRowForApplicantPartyAsync(applicant.ApplicantPartyId)
            ?? throw new InvalidOperationException("Request row was not found.");

        const long employeeId = 1203;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-1203@example.com", role: "Employee");

        var response = await StartAgreementExchangeRequestAsync(
            client,
            request.Id,
            ValidDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    [Fact]
    public async Task Start_for_approved_request_creates_exchange_and_first_employee_proposal()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);

        const long employeeId = 1204;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-1204@example.com", role: "Employee");

        var response = await StartAgreementExchangeRequestAsync(
            client,
            requestId,
            ValidDto(comment: "Initial proposal."));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var exchange = await GetExchangeByRequestIdAsync(requestId)
            ?? throw new InvalidOperationException("Agreement exchange row was not found.");

        exchange.RequestId.Should().Be(requestId);
        exchange.ClientAccountId.Should().Be(account.AccountId);
        exchange.Status.Should().Be("AwaitingClientConfirmation");
        exchange.ActiveProposalVersion.Should().Be(1);

        var proposals = await GetProposalRowsAsync(exchange.Id);
        proposals.Should().ContainSingle();
        var proposal = proposals.Single();
        proposal.Version.Should().Be(1);
        proposal.Sender.Should().Be("Employee");
        proposal.SenderId.Should().Be(employeeId);
        proposal.State.Should().Be("AwaitingClientConfirmation");
        proposal.DocumentStorageKey.Should().Be("agreements/request-v1.pdf");
        proposal.Comment.Should().Be("Initial proposal.");
    }

    [Fact]
    public async Task Start_rejects_duplicate_exchange_for_same_request()
    {
        var account = await RegisterAccountAsync();
        var requestId = await CreateApprovedRequestForAccountAsync(account.AccountId);

        const long employeeId = 1205;
        await InsertEmployeeAsync(employeeId);
        var client = AuthenticatedClient(employeeId, "employee-1205@example.com", role: "Employee");

        var first = await StartAgreementExchangeRequestAsync(
            client,
            requestId,
            ValidDto());
        await HttpResponseAssertions.For(first, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NoContent);

        var second = await StartAgreementExchangeRequestAsync(
            client,
            requestId,
            ValidDto(storageKey: "agreements/request-v1-duplicate.pdf"));

        await HttpResponseAssertions.For(second, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.UnprocessableEntity);
    }

    private Task<HttpResponseMessage> StartAgreementExchangeRequestAsync(
        HttpClient client,
        long requestId,
        StartAgreementExchangeDto dto)
    {
        return PostAsJsonWithCsrfAsync(
            client,
            $"/api/employee/requests/{requestId}/agreement-exchange/start",
            dto);
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

    private static StartAgreementExchangeDto ValidDto(
        string storageKey = "agreements/request-v1.pdf",
        string originalFileName = "request-v1.pdf",
        string contentType = "application/pdf",
        long sizeBytes = 4096,
        string? comment = "Initial agreement proposal.")
    {
        return new StartAgreementExchangeDto(
            new AgreementDocumentRefDto(
                storageKey,
                originalFileName,
                contentType,
                sizeBytes),
            comment);
    }

    private async Task<ExchangeRow?> GetExchangeByRequestIdAsync(long requestId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, RequestId, ClientAccountId, Status, ActiveProposalVersion
            FROM dbo.L1AgreementProposalExchanges
            WHERE RequestId = @id
            """,
            requestId);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new ExchangeRow(
            reader.GetInt64("Id"),
            reader.GetInt64("RequestId"),
            reader.GetInt64("ClientAccountId"),
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
        long RequestId,
        long ClientAccountId,
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
