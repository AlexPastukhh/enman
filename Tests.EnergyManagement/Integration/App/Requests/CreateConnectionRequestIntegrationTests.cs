using System.Net;
using System.Net.Http.Json;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Application.Commands;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App.Requests;

[Collection("IntegrationTestCollection")]
public sealed class CreateConnectionRequestIntegrationTests : AppIntegrationTestBase
{
    public CreateConnectionRequestIntegrationTests(IntegrationTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task LoginCookie_CreateConnectionRequest_Succeeds()
    {
        var email = UniqueEmail();
        await RegisterAccountAsync(email);
        var client = _factory.CreateClient();
        var login = await LoginAsync(client, email, ValidPassword);

        var applicantResponse = await PostAsJsonWithCsrfAsync(
            client,
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto());
        await HttpResponseAssertions.For(applicantResponse, _output).ShouldBeSuccess();
        var applicantParty = await applicantResponse.Content
            .ReadFromJsonAsync<CreateApplicantPartyResponse>()
            ?? throw new InvalidOperationException("Applicant party response body was empty.");

        var requestResponse = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(existingApplicantPartyId: applicantParty.ApplicantPartyId));
        await HttpResponseAssertions.For(requestResponse, _output).ShouldBeSuccess();

        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        row!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        row.Status.Should().Be("InReview");
        login.AccountId.Should().Be(applicantParty.ClientAccountId);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithExistingOwnedApplicantParty_Succeeds()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, applicantParty.ApplicantPartyId);

        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var applicantCountAfter = await GetApplicantPartyCountAsync(account.AccountId);

        row.Should().NotBeNull();
        row!.Id.Should().BeGreaterThan(0);
        row!.ApplicantPartyId.Should().Be(applicantParty.ApplicantPartyId);
        row.Status.Should().Be("InReview");
        row.Details.Should().Be(RequestDetails);
        row.City.Should().Be(City);
        row.Street.Should().Be(Street);
        applicantCountAfter.Should().Be(applicantCountBefore);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithExistingNonCurrentApplicantParty_Succeeds()
    {
        var account = await RegisterAccountAsync();
        var first = await CreateApplicantPartyAsync(account.AccountId);
        var second = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/applicant-parties/individual",
            ValidApplicantPartyDto(
                firstName: "Second",
                middleName: "Applicant",
                lastName: "Profile",
                email: "second.profile@example.com",
                phoneNumber: "79237554729"));
        await HttpResponseAssertions.For(second, _output).ShouldBeSuccess();
        await SetApplicantCurrentFlagAsync(first.ApplicantPartyId, isCurrent: false);

        await CreateConnectionRequestAsync(account.AccountId, first.ApplicantPartyId);

        var row = await GetLatestRequestRowForApplicantPartyAsync(first.ApplicantPartyId);
        row.Should().NotBeNull();
        row!.ApplicantPartyId.Should().Be(first.ApplicantPartyId);
        row.Status.Should().Be("InReview");
    }

    [Fact]
    public async Task AppFlow_PropagatesEfGeneratedIdsAcrossAggregates()
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
    public async Task CreateConnectionRequest_WithExistingApplicantPartyOwnedByAnotherAccount_ReturnsValidationProblem()
    {
        var owner = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(owner.AccountId);
        var other = await RegisterAccountAsync();
        var client = AuthenticatedClient(other.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(existingApplicantPartyId: applicantParty.ApplicantPartyId));

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
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(
                details: " ",
                existingApplicantPartyId: applicantParty.ApplicantPartyId));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithTooLongDetails_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(
                details: new string('x', 3001),
                existingApplicantPartyId: applicantParty.ApplicantPartyId));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithMissingAddress_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            new CreateConnectionRequestDto(
                "Existing",
                applicantParty.ApplicantPartyId,
                null,
                RequestDetails,
                null!));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithInvalidAddress_ReturnsValidationProblem()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            new CreateConnectionRequestDto(
                "Existing",
                applicantParty.ApplicantPartyId,
                null,
                RequestDetails,
                new AddressDto(
                    "12",
                    Region,
                    City,
                    Street,
                    House,
                    Building,
                    Apartment)));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithNewApplicantData_CreatesApplicantAndRequestAtomically()
    {
        var account = await RegisterAccountAsync();
        await CreateApplicantPartyAsync(account.AccountId);
        var client = AuthenticatedClient(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestWithNewApplicantDto());

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        var applicantCountAfter = await GetApplicantPartyCountAsync(account.AccountId);
        var newApplicantId = await GetLatestApplicantPartyIdForAccountAsync(account.AccountId);
        var newApplicant = await GetApplicantPartyRowAsync(newApplicantId!.Value);
        var request = await GetLatestRequestRowForApplicantPartyAsync(newApplicantId.Value);

        applicantCountAfter.Should().Be(applicantCountBefore + 1);
        newApplicant!.VerificationStatus.Should().Be("Unverified");
        newApplicant.FirstName.Should().Be("Request");
        request.Should().NotBeNull();
        request!.ApplicantPartyId.Should().Be(newApplicantId.Value);
        request.Status.Should().Be("InReview");
    }

    [Fact]
    public async Task CreateConnectionRequest_WithNewIndividualEntrepreneurApplicantData_CreatesApplicantAndRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(
                applicantContextType: "New",
                existingApplicantPartyId: null,
                newApplicantParty: null) with
            {
                NewApplicantParty = new CreateConnectionRequestNewApplicantDto(
                    ApplicantPartyType: "IndividualEntrepreneur",
                    FullName: new FullNameDto("Ivan", "Ivanovich", "Ivanov"),
                    OrganizationName: null,
                    Inn: "123456789012",
                    Kpp: null,
                    Ogrn: null,
                    Ogrnip: "123456789012345",
                    Email: ApplicantEmail,
                    PhoneNumber: PhoneNumber)
            });

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        var newApplicantId = await GetLatestApplicantPartyIdForAccountAsync(account.AccountId);
        var newApplicant = await GetApplicantPartyRowAsync(newApplicantId!.Value);
        var request = await GetLatestRequestRowForApplicantPartyAsync(newApplicantId.Value);

        newApplicant!.ApplicantPartyType.Should().Be("IndividualEntrepreneur");
        newApplicant.IndividualEntrepreneurFirstName.Should().Be("Ivan");
        newApplicant.IndividualEntrepreneurInn.Should().Be("123456789012");
        newApplicant.IndividualEntrepreneurOgrnip.Should().Be("123456789012345");
        request.Should().NotBeNull();
        request!.ApplicantPartyId.Should().Be(newApplicantId.Value);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithNewLegalEntityApplicantData_CreatesApplicantAndRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestDto(
                applicantContextType: "New",
                existingApplicantPartyId: null,
                newApplicantParty: null) with
            {
                NewApplicantParty = new CreateConnectionRequestNewApplicantDto(
                    ApplicantPartyType: "LegalEntity",
                    FullName: null,
                    OrganizationName: "ООО Энергия",
                    Inn: "1234567890",
                    Kpp: "123456789",
                    Ogrn: "1234567890123",
                    Ogrnip: null,
                    Email: ApplicantEmail,
                    PhoneNumber: PhoneNumber)
            });

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        var newApplicantId = await GetLatestApplicantPartyIdForAccountAsync(account.AccountId);
        var newApplicant = await GetApplicantPartyRowAsync(newApplicantId!.Value);
        var request = await GetLatestRequestRowForApplicantPartyAsync(newApplicantId.Value);

        newApplicant!.ApplicantPartyType.Should().Be("LegalEntity");
        newApplicant.LegalEntityOrganizationName.Should().Be("ООО Энергия");
        newApplicant.LegalEntityInn.Should().Be("1234567890");
        newApplicant.LegalEntityKpp.Should().Be("123456789");
        newApplicant.LegalEntityOgrn.Should().Be("1234567890123");
        request.Should().NotBeNull();
        request!.ApplicantPartyId.Should().Be(newApplicantId.Value);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithInvalidNewApplicantData_CreatesNoApplicantAndNoRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestWithNewApplicantDto(
                newApplicantParty: new CreateIndividualApplicantPartyDto(
                    new FullNameDto("", MiddleName, LastName),
                    "not-an-email",
                    "")));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        (await GetApplicantPartyCountAsync(account.AccountId)).Should().Be(applicantCountBefore);
        (await GetRequestCountAsync()).Should().Be(requestCountBefore);
    }

    [Fact]
    public async Task CreateConnectionRequest_NewBranchWithMissingFullName_ReturnsValidationProblemAndCreatesNoApplicantOrRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestWithNewApplicantDto(
                newApplicantParty: new CreateIndividualApplicantPartyDto(
                    null!,
                    ApplicantEmail,
                    PhoneNumber)));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        (await GetApplicantPartyCountAsync(account.AccountId)).Should().Be(applicantCountBefore);
        (await GetRequestCountAsync()).Should().Be(requestCountBefore);
    }

    [Fact]
    public async Task CreateConnectionRequest_WithInvalidRequestDataInNewBranch_CreatesNoApplicantAndNoRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedClient(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/requests",
            ValidConnectionRequestWithNewApplicantDto(details: " "));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);

        (await GetApplicantPartyCountAsync(account.AccountId)).Should().Be(applicantCountBefore);
        (await GetRequestCountAsync()).Should().Be(requestCountBefore);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("Unsupported")]
    public async Task CreateConnectionRequest_WithMissingOrUnknownApplicantContextType_ReturnsValidationProblem(
        string? applicantContextType)
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/requests",
            ValidConnectionRequestDto(
                applicantContextType: applicantContextType,
                existingApplicantPartyId: applicantParty.ApplicantPartyId));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_ExistingBranchRequiresExistingApplicantPartyId()
    {
        var account = await RegisterAccountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/requests",
            ValidConnectionRequestDto(existingApplicantPartyId: null));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_ExistingBranchRejectsExtraNewApplicantPayload()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/requests",
            ValidConnectionRequestDto(
                existingApplicantPartyId: applicantParty.ApplicantPartyId,
                newApplicantParty: ValidApplicantPartyDto()));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_NewBranchRequiresNewApplicantPayload()
    {
        var account = await RegisterAccountAsync();

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/requests",
            ValidConnectionRequestDto(
                applicantContextType: "New",
                existingApplicantPartyId: null,
                newApplicantParty: null));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_NewBranchRejectsExtraExistingApplicantPartyId()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(account.AccountId),
            "/api/requests",
            ValidConnectionRequestWithNewApplicantDto(
                existingApplicantPartyId: applicantParty.ApplicantPartyId));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

}

