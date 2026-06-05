using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.Security;

[Collection("IntegrationTestCollection")]
public sealed class AntiforgeryIntegrationTests : AppIntegrationTestBase
{
    public AntiforgeryIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task AntiforgeryTokenEndpoint_ReturnsRequestToken()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/antiforgery/token");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var body = await response.Content.ReadFromJsonAsync<AntiforgeryTokenResponse>()
            ?? throw new InvalidOperationException("Antiforgery token response body was empty.");
        body.RequestToken.Should().NotBeNullOrWhiteSpace();
        response.Headers.TryGetValues("Set-Cookie", out var cookies).Should().BeTrue();
        cookies.Should().NotBeEmpty();
    }

    [Fact]
    public async Task UnsafeAnonymousAuthEndpoint_WithoutToken_ReturnsAntiforgeryProblem()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequestDto(UniqueEmail(), "WrongPassword!123"));

        await AssertAntiforgeryProblemAsync(response);
    }

    [Fact]
    public async Task UnsafeAnonymousAuthEndpoint_WithToken_ReachesNormalValidationFlow()
    {
        var client = _factory.CreateClient();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto(UniqueEmail(), "WrongPassword!123"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        var problem = await ReadProblemDetailsAsync(response);
        GetProblemCode(problem).Should().NotBe(AntiforgeryConstants.FailureCode);
    }

    [Fact]
    public async Task UnsafeAuthenticatedEndpoint_WithoutToken_ReturnsAntiforgeryProblemAndDoesNotMutateState()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto());

        await AssertAntiforgeryProblemAsync(response);
        (await GetApplicantPartyCountAsync(account.AccountId)).Should().Be(applicantCountBefore);
    }

    [Fact]
    public async Task UnsafeAuthenticatedEndpoint_WithToken_SucceedsAfterLoginAndTokenRefresh()
    {
        var email = UniqueEmail();
        var account = await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        await LoginAsync(client, email, ValidPassword);
        await GetAntiforgeryTokenAsync(client);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto());

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var applicant = await response.Content.ReadFromJsonAsync<CreateApplicantPartyResponse>()
            ?? throw new InvalidOperationException("Applicant party response body was empty.");
        applicant.ClientAccountId.Should().Be(account.AccountId);
    }

    [Fact]
    public async Task UnsafeEndpoint_WithValidTokenAndInvalidDto_ReturnsValidationProblemNotAntiforgeryProblem()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/applicant-parties/individual",
            new CreateIndividualApplicantPartyDto(
                null!,
                ApplicantEmail,
                PhoneNumber));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        var problem = await ReadProblemDetailsAsync(response);
        GetProblemCode(problem).Should().NotBe(AntiforgeryConstants.FailureCode);
    }

    [Fact]
    public async Task SafeGetEndpoint_WithoutToken_DoesNotRequireAntiforgeryToken()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/applicant-parties");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task Logout_WithoutTokenReturnsAntiforgeryProblem_AndWithTokenSucceeds()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var missingTokenClient = _factory.CreateClient();
        await LoginAsync(missingTokenClient, email, ValidPassword);

        var missingTokenLogout = await missingTokenClient.PostAsync("/api/auth/logout", content: null);
        await AssertAntiforgeryProblemAsync(missingTokenLogout);

        var validTokenClient = _factory.CreateClient();
        await LoginAsync(validTokenClient, email, ValidPassword);
        var logout = await PostWithCsrfAsync(validTokenClient, "/api/auth/logout");

        logout.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private async Task AssertAntiforgeryProblemAsync(HttpResponseMessage response)
    {
        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(StatusCodes.Status400BadRequest);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/problem+json");

        var problem = await ReadProblemDetailsAsync(response);
        problem.Type.Should().Be(AntiforgeryConstants.FailureType);
        problem.Title.Should().Be(AntiforgeryConstants.FailureTitle);
        problem.Status.Should().Be(StatusCodes.Status400BadRequest);
        problem.Detail.Should().Be(AntiforgeryConstants.FailureDetail);
        GetProblemCode(problem).Should().Be(AntiforgeryConstants.FailureCode);
    }

    private static async Task<ProblemDetails> ReadProblemDetailsAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ProblemDetails>()
            ?? throw new InvalidOperationException("ProblemDetails response body was empty.");
    }

    private static string? GetProblemCode(ProblemDetails problem)
    {
        if (!problem.Extensions.TryGetValue("code", out var code) || code is null)
        {
            return null;
        }

        return code is JsonElement jsonElement
            ? jsonElement.GetString()
            : code.ToString();
    }
}
