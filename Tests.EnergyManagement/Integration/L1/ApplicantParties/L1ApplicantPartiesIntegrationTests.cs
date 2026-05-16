using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.ApplicantParties;

[Collection("IntegrationTestCollection")]
public sealed class L1ApplicantPartiesIntegrationTests : L1IntegrationTestBase
{
    public L1ApplicantPartiesIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
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
    public async Task ListAccountApplicantParties_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/l1/applicant-parties");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ListAccountApplicantParties_WithNoApplicantParties_ReturnsEmptyList()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await GetAccountApplicantPartiesAsync(client);

        response.ApplicantParties.Should().BeEmpty();
    }

    [Fact]
    public async Task ListAccountApplicantParties_WithFirstApplicantParty_ReturnsSummaryAndNoClientAccountId()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var httpResponse = await client.GetAsync("/api/l1/applicant-parties");
        await HttpResponseAssertions.For(httpResponse, _output).ShouldBeSuccess();
        var responseBody = await httpResponse.Content.ReadAsStringAsync();
        var response = System.Text.Json.JsonSerializer.Deserialize<L1AccountApplicantPartiesResponse>(
                responseBody,
                new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web))
            ?? throw new InvalidOperationException("L1 account applicant parties response body was empty.");

        response.ApplicantParties.Should().ContainSingle();
        var summary = response.ApplicantParties.Single();
        summary.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        summary.ApplicantPartyType.Should().Be("Individual");
        summary.DisplayName.Should().Be($"{LastName} {FirstName} {MiddleName}");
        summary.FullName.Should().NotBeNull();
        summary.FullName!.FirstName.Should().Be(FirstName);
        summary.FullName.MiddleName.Should().Be(MiddleName);
        summary.FullName.LastName.Should().Be(LastName);
        summary.Email.Should().Be(ApplicantEmail);
        summary.PhoneNumber.Should().Be(PhoneNumber);
        summary.VerificationStatus.Should().Be("Unverified");
        summary.IsCurrentDefault.Should().BeTrue();
        summary.CreatedAt.Should().NotBeNull();
        responseBody.Should().NotContain("clientAccountId");
    }

    [Fact]
    public async Task ListAccountApplicantParties_WithMultipleSameTypeApplicantParties_ReturnsAllWithOnlyFirstCurrentDefault()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);

        var secondResponse = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto(
                firstName: "Jane",
                middleName: "Anne",
                lastName: "Smith",
                email: UniqueEmail(),
                phoneNumber: "79237554727"));
        await HttpResponseAssertions.For(secondResponse, _output).ShouldBeSuccess();
        var second = await secondResponse.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var response = await GetAccountApplicantPartiesAsync(client);

        response.ApplicantParties.Should().HaveCount(2);
        var firstSummary = response.ApplicantParties.Single(x => x.ApplicantPartyId == first.ApplicantPartyId);
        var secondSummary = response.ApplicantParties.Single(x => x.ApplicantPartyId == second.ApplicantPartyId);
        firstSummary.IsCurrentDefault.Should().BeTrue();
        secondSummary.IsCurrentDefault.Should().BeFalse();
        response.ApplicantParties.Should().OnlyContain(x => x.ApplicantPartyType == "Individual");
    }

    [Fact]
    public async Task ListAccountApplicantParties_DoesNotReturnAnotherAccountApplicantParties()
    {
        var owner = await RegisterAccountAsync();
        var ownerApplicantParty = await CreateApplicantPartyAsync(owner.AccountId);

        var other = await RegisterAccountAsync();
        var otherApplicantParty = await CreateApplicantPartyAsync(other.AccountId);
        var client = AuthenticatedL1Client(other.AccountId, other.Email);

        var response = await GetAccountApplicantPartiesAsync(client);

        response.ApplicantParties.Should().ContainSingle();
        response.ApplicantParties.Single().ApplicantPartyId.Should().Be(otherApplicantParty.ApplicantPartyId);
        response.ApplicantParties.Should().NotContain(x => x.ApplicantPartyId == ownerApplicantParty.ApplicantPartyId);
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
        row.VerificationStatus.Should().Be("Unverified");
        row.IsCurrentActiveVersion.Should().BeTrue();
    }

    [Fact]
    public async Task CreateIndividualApplicantParty_WithInvalidData_ReturnsValidationProblemAndCreatesNoApplicant()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            new L1CreateIndividualApplicantPartyDto(
                new L1FullNameDto("", MiddleName, LastName),
                "not-an-email",
                ""));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var applicantCountAfter = await GetApplicantPartyCountAsync(account.AccountId);
        applicantCountAfter.Should().Be(applicantCountBefore);
    }

    [Fact]
    public async Task CreateIndividualApplicantParty_CreatingSecondApplicantDoesNotReplaceFirstDefault()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);

        var secondResponse = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            ValidApplicantPartyDto(
                firstName: "Jane",
                middleName: "Anne",
                lastName: "Smith",
                email: "second.applicant.l1@example.com",
                phoneNumber: "79237554727"));
        await HttpResponseAssertions.For(secondResponse, _output).ShouldBeSuccess();
        var second = await secondResponse.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");

        var firstRow = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var secondRow = await GetApplicantPartyRowAsync(second.ApplicantPartyId);
        var applicantCount = await GetApplicantPartyCountAsync(account.AccountId);

        applicantCount.Should().Be(2);
        firstRow.Should().NotBeNull();
        secondRow.Should().NotBeNull();
        firstRow!.IsCurrentActiveVersion.Should().BeTrue();
        firstRow.VerificationStatus.Should().Be("Unverified");
        firstRow.FirstName.Should().Be(FirstName);
        secondRow!.ClientAccountId.Should().Be(account.AccountId);
        secondRow.FirstName.Should().Be("Jane");
        secondRow.VerificationStatus.Should().Be("Unverified");
        secondRow.IsCurrentActiveVersion.Should().BeFalse();
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
    public async Task CreateIndividualApplicantParty_WithMissingFullName_ReturnsValidationProblemAndCreatesNoApplicant()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
            new L1CreateIndividualApplicantPartyDto(
                null!,
                ApplicantEmail,
                PhoneNumber));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        (await GetApplicantPartyCountAsync(account.AccountId)).Should().Be(applicantCountBefore);
    }

}

