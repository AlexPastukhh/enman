using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using EnergyManagement.Server.Api;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.Auth;

[Collection("IntegrationTestCollection")]
public sealed class EmployeeAuthenticationIntegrationTests : AppIntegrationTestBase
{
    private const long EmployeeId = 700;
    private const string EmployeeEmail = "employee.password@example.com";
    private const string WindowsLogin = "TESTDOMAIN\\employee";

    public EmployeeAuthenticationIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task EmployeeAccount_CanLoginWithEmailAndPassword()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(EmployeeId, email: EmployeeEmail, windowsLogin: WindowsLogin);
        var client = _factory.CreateClient();

        var login = await LoginAsync(client, EmployeeEmail, ValidPassword);
        var currentUser = await GetCurrentUserAsync(client);
        var employeeRequests = await client.GetAsync("/api/employee/requests");

        login.AccountId.Should().Be(EmployeeId);
        login.Email.Should().Be(EmployeeEmail);
        login.Role.Should().Be("Employee");
        login.IsActive.Should().BeTrue();
        currentUser.Should().BeEquivalentTo(login);
        await HttpResponseAssertions.For(employeeRequests, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task WindowsSignIn_WithUnknownWindowsIdentity_ReturnsForbiddenProblem()
    {
        await ResetDatabaseAsync();
        var client = EmployeeWindowsClient("TESTDOMAIN\\missing");

        var response = await client.GetAsync("/api/employee/auth/windows-signin");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
        (await ReadProblemCodeAsync(response)).Should().Be("auth.employee.windows.not_registered");
    }

    [Fact]
    public async Task WindowsSignIn_WithInactiveEmployee_ReturnsForbiddenProblem()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(EmployeeId, email: EmployeeEmail, windowsLogin: WindowsLogin, isActive: false);
        var client = EmployeeWindowsClient(WindowsLogin);

        var response = await client.GetAsync("/api/employee/auth/windows-signin");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Forbidden);
        (await ReadProblemCodeAsync(response)).Should().Be("auth.employee.inactive");
    }

    [Fact]
    public async Task WindowsSignIn_WithActiveEmployee_IssuesEmployeeSessionCookie()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(EmployeeId, email: EmployeeEmail, windowsLogin: WindowsLogin);
        var client = EmployeeWindowsClient(WindowsLogin);

        var response = await client.GetAsync("/api/employee/auth/windows-signin");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var session = await response.Content.ReadFromJsonAsync<CurrentUserResponseDto>()
            ?? throw new InvalidOperationException("Windows sign-in response body was empty.");
        session.AccountId.Should().Be(EmployeeId);
        session.Email.Should().Be(EmployeeEmail);
        session.Role.Should().Be("Employee");
        session.IsAuthenticated.Should().BeTrue();

        var employeeRequests = await client.GetAsync("/api/employee/requests");
        await HttpResponseAssertions.For(employeeRequests, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task Logout_AfterEmployeeWindowsSignIn_ClearsApplicationCookie()
    {
        await ResetDatabaseAsync();
        await InsertEmployeeAsync(EmployeeId, email: EmployeeEmail, windowsLogin: WindowsLogin);
        var client = EmployeeWindowsClient(WindowsLogin);
        await HttpResponseAssertions.For(
            await client.GetAsync("/api/employee/auth/windows-signin"),
            _output).ShouldBeSuccess();

        var logout = await PostWithCsrfAsync(client, "/api/auth/logout");
        logout.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var employeeRequests = await client.GetAsync("/api/employee/requests");
        await HttpResponseAssertions.For(employeeRequests, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    private HttpClient EmployeeWindowsClient(string windowsLogin)
    {
        return _factory.EmployeeWindowsIdentity(new Claim(ClaimTypes.Name, windowsLogin)).CreateClient();
    }

    private static async Task<string?> ReadProblemCodeAsync(HttpResponseMessage response)
    {
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.TryGetProperty("code", out var code)
            ? code.GetString()
            : null;
    }

    private Task ResetDatabaseAsync()
    {
        return new global::EnergyManagement.Testing.TestDatabase.TestDatabaseManager(_fixture.ConnectionString).ResetAsync();
    }
}
