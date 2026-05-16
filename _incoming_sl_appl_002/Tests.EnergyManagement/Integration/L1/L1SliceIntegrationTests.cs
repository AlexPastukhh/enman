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
            ValidConnectionRequestDto(existingApplicantPartyId: applicantParty.ApplicantPartyId));
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

        await CreateConnectionRequestAsync(account.AccountId, details: "First in-review request.");
        var inReview = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Second approved request.");
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
    public async Task ListMyRequests_WithEmptyStatusFilter_ReturnsSuccess()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/l1/requests?status=");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
    }

    [Fact]
    public async Task ListMyRequests_ReturnsNewestFirst()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        await CreateConnectionRequestAsync(account.AccountId, details: "Older request.");
        var older = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(older!.Id, DateTimeOffset.UtcNow.AddDays(-1));

        await CreateConnectionRequestAsync(account.AccountId, details: "Newer request.");
        var newer = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        await UpdateRequestCreatedAtAsync(newer!.Id, DateTimeOffset.UtcNow);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var requests = await GetMyRequestsAsync(client);

        requests.Should().HaveCount(2);
        requests[0].RequestId.Should().Be(newer.Id);
        requests[1].RequestId.Should().Be(older.Id);
    }

    [Fact]
    public async Task GetMyRequestDetails_WithoutAuth_ReturnsUnauthorized()
    {
        var response = await _factory.CreateClient().GetAsync("/api/l1/requests/1");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForOwnInReviewRequest_ReturnsSubmittedRequestData()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row!.Id);

        details.RequestId.Should().Be(row.Id);
        details.RequestType.Should().Be("Connection");
        details.Status.Should().Be("InReview");
        details.CreatedAt.Should().NotBe(default);
        details.SubmittedRequest.Details.Should().Be(RequestDetails);
        details.SubmittedRequest.ObjectAddress.City.Should().Be(City);
        details.SubmittedRequest.ObjectAddress.Street.Should().Be(Street);
        details.ReviewResult.Should().BeNull();
    }

    [Fact]
    public async Task GetMyRequestDetails_ForMissingRequest_ReturnsNotFound()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId, account.Email);

        var response = await client.GetAsync("/api/l1/requests/987654");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForAnotherAccountRequest_ReturnsNotFound()
    {
        var accountWithRequest = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(accountWithRequest.AccountId);
        await CreateConnectionRequestAsync(accountWithRequest.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);

        var anotherAccount = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(anotherAccount.AccountId, anotherAccount.Email);

        var response = await client.GetAsync($"/api/l1/requests/{row!.Id}");

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForRejectedRequest_ReturnsFeedbackAndSubmittedData()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(-5);
        const string rejectionReason = "Need more object address details.";
        await UpdateRequestReviewAsync(
            row!.Id,
            status: "Rejected",
            decision: "Rejected",
            decidedAt,
            rejectionReason);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row.Id);

        details.Status.Should().Be("Rejected");
        details.SubmittedRequest.Details.Should().Be(RequestDetails);
        details.SubmittedRequest.ObjectAddress.City.Should().Be(City);
        details.ReviewResult.Should().NotBeNull();
        details.ReviewResult!.Decision.Should().Be("Rejected");
        details.ReviewResult.DecidedAt.Should().BeCloseTo(decidedAt, TimeSpan.FromSeconds(1));
        details.ReviewResult.Rejection.Should().NotBeNull();
        details.ReviewResult.Rejection!.Reason.Should().Be(rejectionReason);
    }

    [Fact]
    public async Task GetMyRequestDetails_ForApprovedRequest_ReturnsApprovedDecision()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);
        await CreateConnectionRequestAsync(account.AccountId);
        var row = await GetLatestRequestRowForApplicantPartyAsync(applicantParty.ApplicantPartyId);
        var decidedAt = DateTimeOffset.UtcNow.AddMinutes(-3);
        await UpdateRequestReviewAsync(
            row!.Id,
            status: "Approved",
            decision: "Approved",
            decidedAt,
            rejectionReason: null);

        var client = AuthenticatedL1Client(account.AccountId, account.Email);
        var details = await GetMyRequestDetailsAsync(client, row.Id);

        details.Status.Should().Be("Approved");
        details.ReviewResult.Should().NotBeNull();
        details.ReviewResult!.Decision.Should().Be("Approved");
        details.ReviewResult.DecidedAt.Should().BeCloseTo(decidedAt, TimeSpan.FromSeconds(1));
        details.ReviewResult.Rejection.Should().BeNull();
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
        var second = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/applicant-parties/individual",
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
    public async Task RegisterClientAccount_WithInvalidEmailAndPassword_ReturnsValidationProblem()
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto("not-an-email", "short"));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidEmailAndBlankPassword_ReturnsValidationProblem()
    {
        var response = await _factory.CreateClient().PostAsJsonAsync(
            "/api/l1/auth/login",
            new L1LoginRequest("not-an-email", " "));

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

    [Fact]
    public async Task CreateConnectionRequest_WithExistingApplicantPartyOwnedByAnotherAccount_ReturnsValidationProblem()
    {
        var owner = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(owner.AccountId);
        var other = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(other.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
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
        var client = AuthenticatedL1Client(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
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
        var client = AuthenticatedL1Client(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
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
        var client = AuthenticatedL1Client(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            new L1CreateConnectionRequestDto(
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
        var client = AuthenticatedL1Client(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            new L1CreateConnectionRequestDto(
                "Existing",
                applicantParty.ApplicantPartyId,
                null,
                RequestDetails,
                new L1AddressDto(
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
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
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
    public async Task CreateConnectionRequest_WithInvalidNewApplicantData_CreatesNoApplicantAndNoRequest()
    {
        var account = await RegisterAccountAsync();
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestWithNewApplicantDto(
                newApplicantParty: new L1CreateIndividualApplicantPartyDto(
                    new L1FullNameDto("", MiddleName, LastName),
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
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestWithNewApplicantDto(
                newApplicantParty: new L1CreateIndividualApplicantPartyDto(
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
        var client = AuthenticatedL1Client(account.AccountId);
        var applicantCountBefore = await GetApplicantPartyCountAsync(account.AccountId);
        var requestCountBefore = await GetRequestCountAsync();

        var response = await client.PostAsJsonAsync(
            "/api/l1/requests",
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

        var response = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/requests",
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

        var response = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(existingApplicantPartyId: null));

        await HttpResponseAssertions.For(response, _output)
            .ShouldBeStatusCode(ProblemDetailsContract.ValidationStatusCode);
    }

    [Fact]
    public async Task CreateConnectionRequest_ExistingBranchRejectsExtraNewApplicantPayload()
    {
        var account = await RegisterAccountAsync();
        var applicantParty = await CreateApplicantPartyAsync(account.AccountId);

        var response = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/requests",
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

        var response = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/requests",
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

        var response = await AuthenticatedL1Client(account.AccountId).PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestWithNewApplicantDto(
                existingApplicantPartyId: applicantParty.ApplicantPartyId));

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

    private async Task<L1AccountApplicantPartiesResponse> GetAccountApplicantPartiesAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/applicant-parties");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1AccountApplicantPartiesResponse>()
            ?? throw new InvalidOperationException("L1 account applicant parties response body was empty.");
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

    private async Task<L1MyRequestDetailsDto> GetMyRequestDetailsAsync(
        HttpClient client,
        long requestId)
    {
        var response = await client.GetAsync($"/api/l1/requests/{requestId}");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1MyRequestDetailsDto>()
            ?? throw new InvalidOperationException("L1 my request details response body was empty.");
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
        long? existingApplicantPartyId = null,
        string details = RequestDetails)
    {
        existingApplicantPartyId ??= await GetLatestApplicantPartyIdForAccountAsync(accountId)
            ?? throw new InvalidOperationException("Could not find applicant party for request helper.");

        var response = await AuthenticatedL1Client(accountId).PostAsJsonAsync(
            "/api/l1/requests",
            ValidConnectionRequestDto(
                details: details,
                existingApplicantPartyId: existingApplicantPartyId));

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

    private static L1CreateIndividualApplicantPartyDto ValidApplicantPartyDto(
        string firstName = FirstName,
        string middleName = MiddleName,
        string lastName = LastName,
        string email = ApplicantEmail,
        string phoneNumber = PhoneNumber)
    {
        return new L1CreateIndividualApplicantPartyDto(
            new L1FullNameDto(firstName, middleName, lastName),
            email,
            phoneNumber);
    }

    private static L1CreateConnectionRequestDto ValidConnectionRequestDto(
        string details = RequestDetails,
        string? applicantContextType = "Existing",
        long? existingApplicantPartyId = 1,
        L1CreateIndividualApplicantPartyDto? newApplicantParty = null)
    {
        return new L1CreateConnectionRequestDto(
            applicantContextType,
            existingApplicantPartyId,
            newApplicantParty,
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

    private static L1CreateConnectionRequestDto ValidConnectionRequestWithNewApplicantDto(
        string details = RequestDetails,
        L1CreateIndividualApplicantPartyDto? newApplicantParty = null,
        long? existingApplicantPartyId = null)
    {
        return ValidConnectionRequestDto(
            details,
            applicantContextType: "New",
            existingApplicantPartyId,
            newApplicantParty ?? ValidApplicantPartyDto(
                firstName: "Request",
                middleName: "New",
                lastName: "Applicant",
                email: "request.new.applicant@example.com",
                phoneNumber: "79237554728"));
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
            SELECT Id, ClientAccountId, Email, PhoneNumber, VerificationStatus, IsCurrentActiveVersion,
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
            reader.GetString("FullName_LastName"),
            reader.GetString("VerificationStatus"),
            reader.GetBoolean("IsCurrentActiveVersion"));
    }

    private async Task<long?> GetLatestApplicantPartyIdForAccountAsync(long accountId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT TOP (1) Id
            FROM dbo.L1ApplicantParties
            WHERE ClientAccountId = @id
            ORDER BY Id DESC
            """,
            accountId);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return reader.GetInt64("Id");
    }

    private async Task<int> GetApplicantPartyCountAsync(long accountId)
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "SELECT COUNT(*) FROM dbo.L1ApplicantParties WHERE ClientAccountId = @id",
            connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@id", accountId);

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not read L1ApplicantParties count.");

        return (int)scalar;
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

    private async Task SetApplicantCurrentFlagAsync(long applicantPartyId, bool isCurrent)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.L1ApplicantParties
            SET IsCurrentActiveVersion = @value
            WHERE Id = @id
            """,
            applicantPartyId,
            isCurrent);
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

    private async Task UpdateRequestReviewAsync(
        long requestId,
        string status,
        string decision,
        DateTimeOffset decidedAt,
        string? rejectionReason)
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            """
            UPDATE dbo.L1ClientRequests
            SET Status = @status,
                ReviewDecision = @decision,
                ReviewDecidedAt = @decidedAt,
                ReviewReviewerId = @reviewerId,
                ReviewRejectionReason = @rejectionReason
            WHERE Id = @id
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        command.Parameters.AddWithValue("@id", requestId);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@decision", decision);
        command.Parameters.AddWithValue("@decidedAt", decidedAt);
        command.Parameters.AddWithValue("@reviewerId", 5);
        command.Parameters.AddWithValue(
            "@rejectionReason",
            rejectionReason is null ? DBNull.Value : rejectionReason);

        await command.ExecuteNonQueryAsync();
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
        string LastName,
        string VerificationStatus,
        bool IsCurrentActiveVersion);

    private sealed record RequestRow(
        long Id,
        long ApplicantPartyId,
        string Status,
        string Details,
        string City,
        string Street);
}
