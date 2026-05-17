using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.Auth;

[Collection("IntegrationTestCollection")]
public sealed class L1AuthIntegrationTests : L1IntegrationTestBase
{
    public L1AuthIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
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
    public async Task L1Login_WithInvalidPassword_ReturnsValidationProblem()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);

        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
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

        var invalidPassword = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/login",
            new L1LoginRequest(email, "WrongPassword!123"));
        var unknownEmail = await PostAsJsonWithCsrfAsync(
            client,
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
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
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
        var applicantParty = await PostAsJsonWithCsrfAsync(
            client,
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

        var logout = await PostWithCsrfAsync(client, "/api/l1/auth/logout");
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
    public async Task RegisterClientAccount_WithDuplicateEmail_ReturnsValidationProblem()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);

        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task RegisterClientAccount_WithInvalidEmailAndPassword_ReturnsValidationProblem()
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto("not-an-email", "short"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidEmailAndBlankPassword_ReturnsValidationProblem()
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/login",
            new L1LoginRequest("not-an-email", " "));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

}

