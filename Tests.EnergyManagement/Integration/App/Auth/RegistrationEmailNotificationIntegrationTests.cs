using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.Auth;

[Collection("IntegrationTestCollection")]
public sealed class RegistrationEmailNotificationIntegrationTests : AppIntegrationTestBase
{
    public RegistrationEmailNotificationIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task RegisterClientAccount_WhenRegistrationSucceeds_SendsRegistrationEmail()
    {
        var emailSender = new FakeEmailSender();
        var client = _factory.WithEmailSender(emailSender).CreateClient();
        var email = UniqueEmail();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var registered = await response.Content.ReadFromJsonAsync<RegisterClientAccountResponse>()
            ?? throw new InvalidOperationException("L1 register response body was empty.");

        emailSender.SendAttempts.Should().Be(1);
        emailSender.SentMessages.Should().ContainSingle();
        emailSender.SentMessages[0].To.Should().Be(email);
        emailSender.SentMessages[0].Subject.Should().NotBeNullOrWhiteSpace();
        emailSender.SentMessages[0].Body.Should().NotBeNullOrWhiteSpace();
        registered.Email.Should().Be(email);
    }

    [Fact]
    public async Task RegisterClientAccount_WhenRequestValidationFails_DoesNotSendRegistrationEmail()
    {
        var emailSender = new FakeEmailSender();
        var client = _factory.WithEmailSender(emailSender).CreateClient();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto("not-an-email", "short"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        emailSender.SendAttempts.Should().Be(0);
        emailSender.SentMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task RegisterClientAccount_WhenRegistrationCommandFails_DoesNotSendRegistrationEmail()
    {
        var emailSender = new FakeEmailSender();
        var client = _factory.WithEmailSender(emailSender).CreateClient();
        var email = UniqueEmail();

        var first = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto(email, ValidPassword));
        await HttpResponseAssertions.For(first, _output).ShouldBeSuccess();
        emailSender.Clear();

        var duplicate = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(duplicate, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
        emailSender.SendAttempts.Should().Be(0);
        emailSender.SentMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task RegisterClientAccount_WhenEmailSendingFails_KeepsRegistrationSuccessful()
    {
        var emailSender = new FakeEmailSender { ThrowOnSend = true };
        var client = _factory.WithEmailSender(emailSender).CreateClient();
        var email = UniqueEmail();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto(email, ValidPassword));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
        var registered = await response.Content.ReadFromJsonAsync<RegisterClientAccountResponse>()
            ?? throw new InvalidOperationException("L1 register response body was empty.");
        var row = await GetAccountRowAsync(registered.AccountId);

        emailSender.SendAttempts.Should().Be(1);
        row.Should().NotBeNull();
        row!.Email.Should().Be(email);
    }
}
