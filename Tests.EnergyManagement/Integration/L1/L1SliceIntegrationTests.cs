using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
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
            ValidConnectionRequestDto(applicantParty.ApplicantPartyId));
        await HttpResponseAssertions.For(requestResponse, _output).ShouldBeSuccess();

        var request = await requestResponse.Content.ReadFromJsonAsync<L1CreateConnectionRequestResponse>()
            ?? throw new InvalidOperationException("L1 request response body was empty.");

        request.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        request.Status.Should().Be("InReview");

        var row = await GetRequestRowAsync(request.RequestId);
        row!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        login.AccountId.Should().Be(applicantParty.ClientAccountId);
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
        var response = await AuthenticatedClient(989_898).GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
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
    public async Task CreateConnectionRequest_StoresGeneratedApplicantPartyReference()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        var response = await CreateConnectionRequestAsync(
            account.AccountId,
            applicantParty.ApplicantPartyId);

        var row = await GetRequestRowAsync(response.RequestId);

        response.RequestId.Should().BeGreaterThan(0);
        response.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        response.Status.Should().Be("InReview");
        row.Should().NotBeNull();
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
        var request = await CreateConnectionRequestAsync(
            account.AccountId,
            applicantParty.ApplicantPartyId);

        var applicantPartyRow = await GetApplicantPartyRowAsync(applicantParty.ApplicantPartyId);
        var requestRow = await GetRequestRowAsync(request.RequestId);

        account.AccountId.Should().BeGreaterThan(0);
        applicantParty.ApplicantPartyId.Should().BeGreaterThan(0);
        request.RequestId.Should().BeGreaterThan(0);
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
        var client = AuthenticatedClient(989_898);

        var response = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_ForMissingApplicantParty_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(applicantPartyId: 797_979));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithEmptyDetails_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedClient(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(applicantParty.ApplicantPartyId, details: " "));

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

    private async Task<L1CreateIndividualApplicantPartyResponse> CreateApplicantPartyAsync(long accountId)
    {
        var response = await AuthenticatedClient(accountId).PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");
    }

    private async Task<L1CreateConnectionRequestResponse> CreateConnectionRequestAsync(
        long accountId,
        long applicantPartyId)
    {
        var response = await AuthenticatedClient(accountId).PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(applicantPartyId));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CreateConnectionRequestResponse>()
            ?? throw new InvalidOperationException("L1 request response body was empty.");
    }

    private HttpClient AuthenticatedClient(long accountId)
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()))
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
        long applicantPartyId,
        string details = RequestDetails)
    {
        return new L1CreateConnectionRequestDto(
            applicantPartyId,
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
