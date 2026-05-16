using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using EnergyManagement.Server.L1.Application.Security;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1;

[Collection("IntegrationTestCollection")]
public sealed class L1SliceIntegrationTests
{
    private readonly IntegrationTestFixture _fixture;
    private readonly WebAppFactory _factory;
    private readonly ITestOutputHelper _output;

    public L1SliceIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _factory = fixture.Factory;
        _output = output;
    }

    [Fact]
    public async Task L1RegisterLoginAndCurrentUser_UsesL1AccountIdentity()
    {
        var email = UniqueEmail();
        var account = await RegisterAccountAsync(email);
        var client = _factory.CreateClient();

        var login = await LoginAsync(client, email, ValidPassword);
        var currentUser = await GetCurrentUserAsync(client);

        login.AccountId.Should().Be(account.AccountId);
        login.Email.Should().Be(email);
        login.Role.Should().Be("Client");
        login.IsActive.Should().BeTrue();
        login.IsAuthenticated.Should().BeTrue();

        currentUser.Should().BeEquivalentTo(login);
    }

    [Fact]
    public async Task L1LoginCookie_CreateIndividualApplicantParty_Succeeds()
    {
        var email = UniqueEmail();
        var account = await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        await LoginAsync(client, email, ValidPassword);

        var response = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        var applicantParty = await response.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");

        applicantParty.ClientAccountId.Should().Be(account.AccountId);
    }

    [Fact]
    public async Task GetCurrentIndividualApplicantParty_WithCurrentApplicant_ReturnsApplicantData()
    {
        var account = await RegisterAccountAsync();
        await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var currentApplicant = await GetCurrentIndividualApplicantPartyAsync(client);

        currentApplicant.Exists.Should().BeTrue();
        currentApplicant.ApplicantParty.Should().NotBeNull();
        currentApplicant.ApplicantParty!.FullName.FirstName.Should().Be(FirstName);
        currentApplicant.ApplicantParty.FullName.MiddleName.Should().Be(MiddleName);
        currentApplicant.ApplicantParty.FullName.LastName.Should().Be(LastName);
        currentApplicant.ApplicantParty.Email.Should().Be(ApplicantEmail);
        currentApplicant.ApplicantParty.PhoneNumber.Should().Be(PhoneNumber);
        currentApplicant.ApplicantParty.VerificationStatus.Should().Be("Unverified");
    }

    [Fact]
    public async Task GetCurrentIndividualApplicantParty_WithoutCurrentApplicant_ReturnsExistsFalse()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var currentApplicant = await GetCurrentIndividualApplicantPartyAsync(client);

        currentApplicant.Exists.Should().BeFalse();
        currentApplicant.ApplicantParty.Should().BeNull();
    }

    [Fact]
    public async Task GetCurrentIndividualApplicantParty_Unauthenticated_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient()
            .GetAsync("/api/l1/applicant-parties/current-individual");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetCurrentIndividualApplicantParty_DoesNotReturnAnotherAccountApplicantParty()
    {
        var accountWithApplicant = await RegisterAccountAsync();
        await CreateApplicantPartyAsync(accountWithApplicant.AccountId);

        var accountWithoutApplicant = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(
            accountWithoutApplicant.AccountId,
            accountWithoutApplicant.Email);

        var currentApplicant = await GetCurrentIndividualApplicantPartyAsync(client);

        currentApplicant.Exists.Should().BeFalse();
        currentApplicant.ApplicantParty.Should().BeNull();
    }

    [Fact]
    public async Task L1LoginCookie_CreateConnectionRequest_Succeeds()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        var login = await LoginAsync(client, email, ValidPassword);

        var applicantResponse = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());
        await HttpResponseAssertions.For(applicantResponse, _output).ShouldBeSuccess();
        var applicantParty = await applicantResponse.Content
            .ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");

        var requestResponse = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto());
        await HttpResponseAssertions.For(requestResponse, _output).ShouldBeSuccess();

        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        row!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        row.Status.Should().Be("InReview");
        login.AccountId.Should().Be(applicantParty.ClientAccountId);
    }

    [Fact]
    public async Task ListMyRequests_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/l1/requests");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ListMyRequests_WithNoRequests_ReturnsEmptyArray()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var requests = await GetMyRequestsAsync(client);

        requests.Should().BeEmpty();
    }

    [Fact]
    public async Task ListMyRequests_ReturnsCurrentAccountRequestSummaries()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client);
        var persisted = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        requests.Should().ContainSingle();
        var request = requests.Single();
        request.RequestId.Should().Be(persisted!.Id);
        request.RequestType.Should().Be("Connection");
        request.Status.Should().Be("InReview");
        request.Summary.Should().Be(RequestDetails);
        request.ObjectAddress.City.Should().Be(City);
        request.ObjectAddress.Street.Should().Be(Street);
    }

    [Fact]
    public async Task ListMyRequests_DoesNotReturnAnotherAccountRequests()
    {
        var accountWithRequest = await RegisterAccountAsync();
        await CreateApplicantPartyAsync(accountWithRequest.AccountId);
        await CreateConnectionRequestAsync(accountWithRequest.AccountId);

        var accountWithoutRequest = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(
            accountWithoutRequest.AccountId,
            accountWithoutRequest.Email);

        var requests = await GetMyRequestsAsync(client);

        requests.Should().BeEmpty();
    }

    [Fact]
    public async Task ListMyRequests_WithStatusFilter_ReturnsMatchingRequests()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, "First in-review request.");
        var inReview = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        await CreateConnectionRequestAsync(account.AccountId, "Second approved request.");
        var approved = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestStatusAsync(approved!.Id, "Approved");

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client, status: "Approved");

        requests.Should().ContainSingle();
        requests.Single().RequestId.Should().Be(approved.Id);
        requests.Single().Status.Should().Be("Approved");
        requests.Single().RequestId.Should().NotBe(inReview!.Id);
    }

    [Fact]
    public async Task ListMyRequests_WithInvalidStatusFilter_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/l1/requests?status=Done");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task ListMyRequests_ReturnsNewestFirst()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, "Older request.");
        var older = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(older!.Id, DateTimeOffset.UtcNow.AddDays(-1));

        await CreateConnectionRequestAsync(account.AccountId, "Newer request.");
        var newer = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(newer!.Id, DateTimeOffset.UtcNow);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client);

        requests.Should().HaveCount(2);
        requests[0].RequestId.Should().Be(newer.Id);
        requests[1].RequestId.Should().Be(older.Id);
    }

    [Fact]
    public async Task L1Login_WithInvalidPassword_ReturnsValidationProblem()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);

        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/auth/login",
            new L1LoginRequest(email, "WrongPassword!123"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task L1Login_WithUnknownEmail_ReturnsSameSafeFailureAsInvalidPassword()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();

        var invalidPassword = await client.PostAsJsonAsync(
            "/api/l1/auth/login",
            new L1LoginRequest(email, "WrongPassword!123"));
        var unknownEmail = await client.PostAsJsonAsync(
            "/api/l1/auth/login",
            new L1LoginRequest(UniqueEmail(), "WrongPassword!123"));

        await HttpResponseAssertions.For(invalidPassword, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        await HttpResponseAssertions.For(unknownEmail, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var invalidPasswordBody = await invalidPassword.Content.ReadAsStringAsync();
        var unknownEmailBody = await unknownEmail.Content.ReadAsStringAsync();
        unknownEmailBody.Should().Be(invalidPasswordBody);
    }

    [Fact]
    public async Task L1CurrentUser_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task L1ProtectedEndpoint_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task L1CurrentUser_WithNonExistingAccountClaim_ReturnsUnauthorized()
    {
        var response = await AuthenticatedL1Client(989_898).GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task LegacyShapedCookie_WithExistingL1AccountId_IsRejected()
    {
        var account = await RegisterAccountAsync();
        var client = LegacyShapedAuthenticatedClient(account.AccountId);

        var currentUser = await client.GetAsync("/api/l1/auth/current-user");
        var applicantParty = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(currentUser, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
        await HttpResponseAssertions.For(applicantParty, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task L1Cookie_WithMarkerAndExistingAccount_Succeeds()
    {
        var account = await RegisterAccountAsync();

        var currentUser = await AuthenticatedL1Client(account.AccountId, account.Email)
            .GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(currentUser, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task L1Logout_AfterLogin_RemovesCurrentUserSession()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        await LoginAsync(client, email, ValidPassword);

        var logout = await client.PostAsync("/api/l1/auth/logout", content: null);
        logout.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var currentUser = await client.GetAsync("/api/l1/auth/current-user");
        await HttpResponseAssertions.For(currentUser, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RegisterClientAccount_CreatesL1Account()
    {
        var email = UniqueEmail();
        var response = await RegisterAccountAsync(email);

        var row = await GetAccountRowAsync(response.AccountId);

        response.AccountId.Should().BeGreaterThan(0);
        response.Email.Should().Be(email);
        row.Should().NotBeNull();
        row!.Email.Should().Be(email);
        row.PasswordHash.Should().NotBeNullOrWhiteSpace();
        row.Role.Should().Be("Client");
        row.AccountType.Should().Be("Client");
        row.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task CreateIndividualApplicantParty_StoresGeneratedAccountReference()
    {
        var account = await RegisterAccountAsync();
        var response = await CreateApplicantPartyAsync(account.AccountId);

        var row = await GetApplicantPartyRowAsync(response.ApplicantPartyId);

        response.ApplicantPartyId.Should().BeGreaterThan(0);
        response.ClientAccountId.Should().Be(account.AccountId);
        row.Should().NotBeNull();
        row!.ClientAccountId.Should().Be(account.AccountId);
        row.FirstName.Should().Be(FirstName);
        row.MiddleName.Should().Be(MiddleName);
        row.LastName.Should().Be(LastName);
        row.Email.Should().Be(ApplicantEmail);
        row.PhoneNumber.Should().Be(PhoneNumber);
    }

    [Fact]
    public async Task CreateConnectionRequest_StoresServerSelectedCurrentActiveApplicantPartyReference()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId);

        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        row.Should().NotBeNull();
        row!.Id.Should().BeGreaterThan(0);
        row!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        row.Status.Should().Be("InReview");
        row.Details.Should().Be(RequestDetails);
        row.City.Should().Be(City);
        row.Street.Should().Be(Street);
    }

    [Fact]
    public async Task L1Flow_PropagatesEfGeneratedIdsAcrossAggregates()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);

        var applicantPartyRow = await GetApplicantPartyRowAsync(applicantParty.ApplicantPartyId);
        var requestRow = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        account.AccountId.Should().BeGreaterThan(0);
        applicantParty.ApplicantPartyId.Should().BeGreaterThan(0);
        requestRow!.Id.Should().BeGreaterThan(0);
        applicantPartyRow!.ClientAccountId.Should().Be(account.AccountId);
        requestRow!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
    }

    [Fact]
    public async Task RegisterClientAccount_WithDuplicateEmail_ReturnsValidationProblem()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);

        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateIndividualApplicantParty_ForMissingAccount_ReturnsValidationProblem()
    {
        var client = AuthenticatedL1Client(989_898);

        var response = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithoutCurrentActiveApplicantParty_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var requestCountAfter = await GetRequestCountAsync();
        requestCountAfter.Should().Be(requestCountBefore);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithEmptyDetails_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedL1Client(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(details: " "));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    private async Task<L1RegisterClientAccountResponse> RegisterAccountAsync(string? email = null)
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto(email ?? UniqueEmail(), ValidPassword));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1RegisterClientAccountResponse>()
            ?? throw new InvalidOperationException("L1 register response body was empty.");
    }

    private async Task<L1CurrentUserResponse> LoginAsync(
        HttpClient client,
        string email,
        string password)
    {
        var response = await client.PostAsJsonAsync(
            "/api/l1/auth/login",
            new L1LoginRequest(email, password));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentUserResponse>()
            ?? throw new InvalidOperationException("L1 login response body was empty.");
    }

    private async Task<L1CurrentUserResponse> GetCurrentUserAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentUserResponse>()
            ?? throw new InvalidOperationException("L1 current-user response body was empty.");
    }

    private async Task<L1CurrentIndividualApplicantPartyResponse> GetCurrentIndividualApplicantPartyAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/applicant-parties/current-individual");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 current applicant party response body was empty.");
    }

    private async Task<IReadOnlyList<L1MyRequestSummaryDto>> GetMyRequestsAsync(
        HttpClient client,
        string? status = null)
    {
        var path = status is null
            ? "/api/l1/requests"
            : $"/api/l1/requests?status={Uri.EscapeDataString(status)}";
        var response = await client.GetAsync(path);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<IReadOnlyList<L1MyRequestSummaryDto>>()
            ?? throw new InvalidOperationException("L1 my requests response body was empty.");
    }

    private async Task<L1CreateIndividualApplicantPartyResponse> CreateApplicantPartyAsync(long accountId)
    {
        var response = await AuthenticatedL1Client(accountId).PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");
    }

    private async Task<HttpResponseMessage> CreateConnectionRequestAsync(
        long accountId,
        string details = RequestDetails)
    {
        var response = await AuthenticatedL1Client(accountId).PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(details));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return response;
    }

    private HttpClient AuthenticatedL1Client(
        long accountId,
        string email = "l1-authenticated@example.com",
        string role = "Client")
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(L1AuthClaimTypes.AuthModel, L1AuthClaimTypes.AuthModelValue))
            .CreateClient();
    }

    private HttpClient LegacyShapedAuthenticatedClient(long accountId)
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
            new Claim(ClaimTypes.Email, "legacy-shaped@example.com"))
            .CreateClient();
    }

    private static L1CreateIndividualApplicantPartyDto ValidApplicantPartyDto()
    {
        return new L1CreateIndividualApplicantPartyDto(
            new L1FullNameDto(FirstName, MiddleName, LastName),
            ApplicantEmail,
            PhoneNumber);
    }

    private static L1CreateConnectionRequestDto ValidConnectionRequestDto(
        string details = RequestDetails)
    {
        return new L1CreateConnectionRequestDto(
            details,
            new L1AddressDto(
                PostalCode,
                Region,
                City,
                Street,
                House,
                Building,
                Apartment));
    }

    private async Task<AccountRow?> GetAccountRowAsync(long id)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, AccountType, Email, PasswordHash, Role, IsActive
            FROM dbo.L1Accounts
            WHERE Id = @id
            """,
            id);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new AccountRow(
            reader.GetInt64("Id"),
            reader.GetString("AccountType"),
            reader.GetString("Email"),
            reader.GetString("PasswordHash"),
            reader.GetString("Role"),
            reader.GetBoolean("IsActive"));
    }

    private async Task<ApplicantPartyRow?> GetApplicantPartyRowAsync(long id)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, ClientAccountId, Email, PhoneNumber,
                   FullName_FirstName, FullName_MiddleName, FullName_LastName
            FROM dbo.L1ApplicantParties
            WHERE Id = @id
            """,
            id);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new ApplicantPartyRow(
            reader.GetInt64("Id"),
            reader.GetInt64("ClientAccountId"),
            reader.GetString("Email"),
            reader.GetString("PhoneNumber"),
            reader.GetString("FullName_FirstName"),
            reader.GetString("FullName_MiddleName"),
            reader.GetString("FullName_LastName"));
    }

    private async Task<RequestRow?> GetRequestRowAsync(long id)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, ApplicantPartyId, Status, Details,
                   ObjectAddress_City, ObjectAddress_Street
            FROM dbo.L1ClientRequests
            WHERE Id = @id
            """,
            id);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new RequestRow(
            reader.GetInt64("Id"),
            reader.GetInt64("ApplicantPartyId"),
            reader.GetString("Status"),
            reader.GetString("Details"),
            reader.GetString("ObjectAddress_City"),
            reader.GetString("ObjectAddress_Street"));
    }

    private async Task<RequestRow?> GetLatestRequestRowForApplicantPartyAsync(long applicantPartyId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT TOP (1) Id, ApplicantPartyId, Status, Details,
                   ObjectAddress_City, ObjectAddress_Street
            FROM dbo.L1ClientRequests
            WHERE ApplicantPartyId = @id
            ORDER BY Id DESC
            """,
            applicantPartyId);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new RequestRow(
            reader.GetInt64("Id"),
            reader.GetInt64("ApplicantPartyId"),
            reader.GetString("Status"),
            reader.GetString("Details"),
            reader.GetString("ObjectAddress_City"),
            reader.GetString("ObjectAddress_Street"));
    }

    private async Task<int> GetRequestCountAsync()
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "SELECT COUNT(*) FROM dbo.L1ClientRequests",
            connection)
        {
            CommandType = CommandType.Text
        };

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not read L1ClientRequests count.");

        return (int)scalar;
    }

    private async Task UpdateRequestStatusAsync(long requestId, string status)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.L1ClientRequests
            SET Status = @value
            WHERE Id = @id
            """,
            requestId,
            status);
    }

    private async Task UpdateRequestCreatedAtAsync(long requestId, DateTimeOffset createdAt)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.L1ClientRequests
            SET CreatedAt = @value
            WHERE Id = @id
            """,
            requestId,
            createdAt);
    }

    private async Task<SqlDataReader> ExecuteReaderAsync(string query, long id)
    {
        var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@id", id);

        return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }

    private async Task ExecuteNonQueryAsync<TValue>(string query, long id, TValue value)
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@value", value ?? throw new ArgumentNullException(nameof(value)));

        await command.ExecuteNonQueryAsync();
    }

    private static string UniqueEmail()
    {
        return $"l1-{Guid.NewGuid():N}@example.com";
    }

    private const string ValidPassword = "ValidPassword!123";
    private const string FirstName = "John";
    private const string MiddleName = "Michael";
    private const string LastName = "Doe";
    private const string ApplicantEmail = "applicant.l1@example.com";
    private const string PhoneNumber = "79237554726";
    private const string RequestDetails = "Connection request details for L1 integration test.";
    private const string PostalCode = "123456";
    private const string Region = "Region";
    private const string City = "City";
    private const string Street = "Street";
    private const string House = "12";
    private const string Building = "1";
    private const string Apartment = "34";

    private sealed record AccountRow(
        long Id,
        string AccountType,
        string Email,
        string PasswordHash,
        string Role,
        bool IsActive);

    private sealed record ApplicantPartyRow(
        long Id,
        long ClientAccountId,
        string Email,
        string PhoneNumber,
        string FirstName,
        string MiddleName,
        string LastName);

    private sealed record RequestRow(
        long Id,
        long ApplicantPartyId,
        string Status,
        string Details,
        string City,
        string Street);
}
