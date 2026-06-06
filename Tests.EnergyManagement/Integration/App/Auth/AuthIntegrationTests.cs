using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.Auth;

[Collection("IntegrationTestCollection")]
public sealed class AuthIntegrationTests : AppIntegrationTestBase
{
    public AuthIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task RegisterLoginAndCurrentUser_UsesAccountIdentity()
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
    public async Task Login_WithInvalidPassword_ReturnsValidationProblem()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);

        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto(email, "WrongPassword!123"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var problem = await response.Content.ReadFromJsonAsync<JsonDocument>()
            ?? throw new InvalidOperationException("ProblemDetails response body was empty.");
        var error = problem.RootElement
            .GetProperty(ProblemDetailsContract.ErrorsExtension)[0];

        error.GetProperty("FieldName").GetString().Should().Be("Password");
        error.GetProperty("ErrorCode").GetString().Should().Be("account.password.is.wrong");
        error.TryGetProperty("code", out _).Should().BeFalse();
        error.TryGetProperty("statusCode", out _).Should().BeFalse();
    }

    [Fact]
    public async Task Login_WithUnknownEmail_ReturnsSameSafeFailureAsInvalidPassword()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();

        var invalidPassword = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto(email, "WrongPassword!123"));
        var unknownEmail = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto(UniqueEmail(), "WrongPassword!123"));

        await HttpResponseAssertions.For(invalidPassword, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        await HttpResponseAssertions.For(unknownEmail, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var invalidPasswordBody = await invalidPassword.Content.ReadAsStringAsync();
        var unknownEmailBody = await unknownEmail.Content.ReadAsStringAsync();
        unknownEmailBody.Should().Be(invalidPasswordBody);
    }

    [Fact]
    public async Task CurrentUser_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/auth/current-user");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task OldLoginPostRoute_ReturnsMethodNotAllowed()
    {
        var oldLoginRoute = string.Join(
            '/',
            string.Empty,
            "api",
            "l1",
            "auth",
            "login");

        var response = await _factory.CreateClient().PostAsJsonAsync(
            oldLoginRoute,
            new LoginRequestDto(UniqueEmail(), ValidPassword));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task ProtectedEndpoint_WithoutAuth_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CurrentUser_WithNonExistingAccountClaim_ReturnsUnauthorized()
    {
        var response = await AuthenticatedClient(989_898).GetAsync("/api/auth/current-user");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task LegacyShapedCookie_WithExistingAccountId_IsRejected()
    {
        var account = await RegisterAccountAsync();
        var client = LegacyShapedAuthenticatedClient(account.AccountId);

        var currentUser = await client.GetAsync("/api/auth/current-user");
        var applicantParty = await PostAsJsonWithCsrfAsync(
            client,
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(currentUser, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
        await HttpResponseAssertions.For(applicantParty, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Cookie_WithMarkerAndExistingAccount_Succeeds()
    {
        var account = await RegisterAccountAsync();

        var currentUser = await AuthenticatedClient(account.AccountId, account.Email)
            .GetAsync("/api/auth/current-user");

        await HttpResponseAssertions.For(currentUser, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task Logout_AfterLogin_RemovesCurrentUserSession()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        await LoginAsync(client, email, ValidPassword);

        var logout = await PostWithCsrfAsync(client, "/api/auth/logout");
        logout.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var currentUser = await client.GetAsync("/api/auth/current-user");
        await HttpResponseAssertions.For(currentUser, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RegisterClientAccount_CreatesAccount()
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
            "/api/auth/register",
            new RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task RegisterClientAccount_WithInvalidEmailAndPassword_ReturnsValidationProblem()
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto("not-an-email", "short"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidEmailAndBlankPassword_ReturnsValidationProblem()
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto("not-an-email", " "));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

}

