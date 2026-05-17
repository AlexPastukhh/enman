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
    public async Task MakeApplicantPartyCurrentDefault_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().PostAsync(
            "/api/l1/applicant-parties/1/make-current-default",
            content: null);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task MakeApplicantPartyCurrentDefault_WithMissingApplicantParty_ReturnsValidationProblemAndDoesNotChangeDefaults()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);
        var firstBefore = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await MakeApplicantPartyCurrentDefaultRequestAsync(
            account.AccountId,
            first.ApplicantPartyId + 999_999);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var firstAfter = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var applicantCountAfter = await GetApplicantPartyCountAsync(account.AccountId);

        firstBefore.Should().NotBeNull();
        firstAfter.Should().Be(firstBefore);
        applicantCountAfter.Should().Be(applicantCountBefore);
    }

    [Fact]
    public async Task MakeApplicantPartyCurrentDefault_WithNotOwnedApplicantParty_ReturnsValidationProblemAndDoesNotChangeDefaults()
    {
        var owner = await RegisterAccountAsync();
        var ownerApplicantParty = await CreateApplicantPartyAsync(owner.AccountId);
        var ownerApplicantBefore = await GetApplicantPartyRowAsync(ownerApplicantParty.ApplicantPartyId);

        var other = await RegisterAccountAsync();
        var otherApplicantParty = await CreateApplicantPartyAsync(other.AccountId);
        var otherApplicantBefore = await GetApplicantPartyRowAsync(otherApplicantParty.ApplicantPartyId);

        var response = await MakeApplicantPartyCurrentDefaultRequestAsync(
            other.AccountId,
            ownerApplicantParty.ApplicantPartyId);

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        var ownerApplicantAfter = await GetApplicantPartyRowAsync(ownerApplicantParty.ApplicantPartyId);
        var otherApplicantAfter = await GetApplicantPartyRowAsync(otherApplicantParty.ApplicantPartyId);

        ownerApplicantBefore.Should().NotBeNull();
        otherApplicantBefore.Should().NotBeNull();
        ownerApplicantAfter.Should().Be(ownerApplicantBefore);
        otherApplicantAfter.Should().Be(otherApplicantBefore);
    }

    [Fact]
    public async Task MakeApplicantPartyCurrentDefault_WithOwnedNonDefaultApplicantParty_SwitchesPersistedDefault()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);
        var second = await CreateApplicantPartyAsync(
            account.AccountId,
            ValidApplicantPartyDto(
                firstName: "Jane",
                middleName: "Anne",
                lastName: "Smith",
                email: UniqueEmail(),
                phoneNumber: "79237554727"));

        var firstBefore = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var secondBefore = await GetApplicantPartyRowAsync(second.ApplicantPartyId);
        firstBefore!.IsCurrentActiveVersion.Should().BeTrue();
        secondBefore!.IsCurrentActiveVersion.Should().BeFalse();

        await MakeApplicantPartyCurrentDefaultAsync(account.AccountId, second.ApplicantPartyId);

        var firstAfter = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var secondAfter = await GetApplicantPartyRowAsync(second.ApplicantPartyId);
        var applicantCount = await GetApplicantPartyCountAsync(account.AccountId);

        applicantCount.Should().Be(2);
        firstAfter.Should().NotBeNull();
        secondAfter.Should().NotBeNull();

        firstAfter!.IsCurrentActiveVersion.Should().BeFalse();
        firstAfter.ClientAccountId.Should().Be(firstBefore.ClientAccountId);
        firstAfter.Email.Should().Be(firstBefore.Email);
        firstAfter.PhoneNumber.Should().Be(firstBefore.PhoneNumber);
        firstAfter.FirstName.Should().Be(firstBefore.FirstName);
        firstAfter.MiddleName.Should().Be(firstBefore.MiddleName);
        firstAfter.LastName.Should().Be(firstBefore.LastName);
        firstAfter.VerificationStatus.Should().Be(firstBefore.VerificationStatus);

        secondAfter!.IsCurrentActiveVersion.Should().BeTrue();
        secondAfter.ClientAccountId.Should().Be(secondBefore.ClientAccountId);
        secondAfter.Email.Should().Be(secondBefore.Email);
        secondAfter.PhoneNumber.Should().Be(secondBefore.PhoneNumber);
        secondAfter.FirstName.Should().Be(secondBefore.FirstName);
        secondAfter.MiddleName.Should().Be(secondBefore.MiddleName);
        secondAfter.LastName.Should().Be(secondBefore.LastName);
        secondAfter.VerificationStatus.Should().Be(secondBefore.VerificationStatus);
    }

    [Fact]
    public async Task MakeApplicantPartyCurrentDefault_WithAlreadyCurrentDefaultApplicantParty_IsIdempotent()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);
        var rowBefore = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        await MakeApplicantPartyCurrentDefaultAsync(account.AccountId, first.ApplicantPartyId);

        var rowAfter = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var applicantCountAfter = await GetApplicantPartyCountAsync(account.AccountId);

        rowBefore.Should().NotBeNull();
        rowBefore!.IsCurrentActiveVersion.Should().BeTrue();
        rowAfter.Should().Be(rowBefore);
        applicantCountAfter.Should().Be(applicantCountBefore);
    }

    [Fact]
    public async Task MakeApplicantPartyCurrentDefault_DoesNotChangeExistingRequests()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId, first.ApplicantPartyId);
        var requestBefore = await GetLatestRequestRowForApplicantPartyAsync(first.ApplicantPartyId);
        requestBefore.Should().NotBeNull();

        var second = await CreateApplicantPartyAsync(
            account.AccountId,
            ValidApplicantPartyDto(
                firstName: "Request",
                middleName: "Safe",
                lastName: "Default",
                email: UniqueEmail(),
                phoneNumber: "79237554728"));

        await MakeApplicantPartyCurrentDefaultAsync(account.AccountId, second.ApplicantPartyId);

        var requestAfter = await GetRequestRowAsync(requestBefore!.Id);
        var firstAfter = await GetApplicantPartyRowAsync(first.ApplicantPartyId);
        var secondAfter = await GetApplicantPartyRowAsync(second.ApplicantPartyId);

        requestAfter.Should().Be(requestBefore);
        firstAfter!.IsCurrentActiveVersion.Should().BeFalse();
        secondAfter!.IsCurrentActiveVersion.Should().BeTrue();
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

