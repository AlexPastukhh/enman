using System.Data;
using System.Net.Http.Json;
using System.Security.Claims;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.L1.Api;
using EnergyManagement.Server.L1.Application.Commands;
using EnergyManagement.Server.L1.Application.Security;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.L1;

public abstract class L1IntegrationTestBase
{
    protected readonly IntegrationTestFixture _fixture;
    protected readonly WebAppFactory _factory;
    protected readonly ITestOutputHelper _output;

    protected L1IntegrationTestBase(IntegrationTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _factory = fixture.Factory;
        _output = output;
    }

    protected async Task<L1RegisterClientAccountResponse> RegisterAccountAsync(string? email = null)
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/register",
            new L1RegisterClientAccountDto(email ?? UniqueEmail(), ValidPassword));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1RegisterClientAccountResponse>()
            ?? throw new InvalidOperationException("L1 register response body was empty.");
    }

    protected async Task<L1CurrentUserResponse> LoginAsync(
        HttpClient client,
        string email,
        string password)
    {
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/l1/auth/login",
            new L1LoginRequest(email, password));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentUserResponse>()
            ?? throw new InvalidOperationException("L1 login response body was empty.");
    }

    protected async Task<L1CurrentUserResponse> GetCurrentUserAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/auth/current-user");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentUserResponse>()
            ?? throw new InvalidOperationException("L1 current-user response body was empty.");
    }

    protected async Task<L1CurrentIndividualApplicantPartyResponse> GetCurrentIndividualApplicantPartyAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/applicant-parties/current-individual");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CurrentIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 current applicant party response body was empty.");
    }

    protected async Task<L1AccountApplicantPartiesResponse> GetAccountApplicantPartiesAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/l1/applicant-parties");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1AccountApplicantPartiesResponse>()
            ?? throw new InvalidOperationException("L1 account applicant parties response body was empty.");
    }

    protected async Task<IReadOnlyList<L1MyRequestSummaryDto>> GetMyRequestsAsync(
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


    protected async Task<EmployeeRequestListResponseDto> GetEmployeeRequestsAsync(
        HttpClient client,
        string? status = null,
        string? reviewState = null)
    {
        var query = new List<string>();
        if (status is not null)
        {
            query.Add($"status={Uri.EscapeDataString(status)}");
        }

        if (reviewState is not null)
        {
            query.Add($"reviewState={Uri.EscapeDataString(reviewState)}");
        }

        var path = query.Count == 0
            ? "/api/employee/requests"
            : $"/api/employee/requests?{string.Join("&", query)}";

        var response = await client.GetAsync(path);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<EmployeeRequestListResponseDto>()
            ?? throw new InvalidOperationException("Employee request list response body was empty.");
    }

    protected async Task<L1MyRequestDetailsDto> GetMyRequestDetailsAsync(
        HttpClient client,
        long requestId)
    {
        var response = await client.GetAsync($"/api/l1/requests/{requestId}");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1MyRequestDetailsDto>()
            ?? throw new InvalidOperationException("L1 my request details response body was empty.");
    }

    protected async Task<L1CreateIndividualApplicantPartyResponse> CreateApplicantPartyAsync(long accountId)
    {
        return await CreateApplicantPartyAsync(accountId, ValidApplicantPartyDto());
    }

    protected async Task<L1CreateIndividualApplicantPartyResponse> CreateApplicantPartyAsync(
        long accountId,
        L1CreateIndividualApplicantPartyDto dto)
    {
        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedL1Client(accountId),
            "/api/l1/applicant-parties/individual",
            dto);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<L1CreateIndividualApplicantPartyResponse>()
            ?? throw new InvalidOperationException("L1 applicant party response body was empty.");
    }

    protected Task<HttpResponseMessage> MakeApplicantPartyCurrentDefaultRequestAsync(
        long accountId,
        long applicantPartyId)
    {
        return PostWithCsrfAsync(
            AuthenticatedL1Client(accountId),
            $"/api/l1/applicant-parties/{applicantPartyId}/make-current-default");
    }

    protected async Task MakeApplicantPartyCurrentDefaultAsync(
        long accountId,
        long applicantPartyId)
    {
        var response = await MakeApplicantPartyCurrentDefaultRequestAsync(accountId, applicantPartyId);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();
    }

    protected async Task<HttpResponseMessage> CreateConnectionRequestAsync(
        long accountId,
        long? existingApplicantPartyId = null,
        string details = RequestDetails)
    {
        existingApplicantPartyId ??= await GetLatestApplicantPartyIdForAccountAsync(accountId)
            ?? throw new InvalidOperationException("Could not find applicant party for request helper.");

        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedL1Client(accountId),
            "/api/l1/requests",
            ValidConnectionRequestDto(
                details: details,
                existingApplicantPartyId: existingApplicantPartyId));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return response;
    }

    protected async Task<HttpResponseMessage> PostAsJsonWithCsrfAsync<TValue>(
        HttpClient client,
        string requestUri,
        TValue value)
    {
        var token = await GetAntiforgeryTokenAsync(client);
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = JsonContent.Create(value)
        };
        request.Headers.Add(AntiforgeryConstants.HeaderName, token);

        return await client.SendAsync(request);
    }

    protected async Task<HttpResponseMessage> PostWithCsrfAsync(
        HttpClient client,
        string requestUri)
    {
        var token = await GetAntiforgeryTokenAsync(client);
        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
        request.Headers.Add(AntiforgeryConstants.HeaderName, token);

        return await client.SendAsync(request);
    }

    protected async Task<string> GetAntiforgeryTokenAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/antiforgery/token");
        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        var token = await response.Content.ReadFromJsonAsync<AntiforgeryTokenResponse>()
            ?? throw new InvalidOperationException("Antiforgery token response body was empty.");

        token.RequestToken.Should().NotBeNullOrWhiteSpace();
        return token.RequestToken;
    }

    protected HttpClient AuthenticatedL1Client(
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

    protected HttpClient LegacyShapedAuthenticatedClient(long accountId)
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
            new Claim(ClaimTypes.Email, "legacy-shaped@example.com"))
            .CreateClient();
    }

    protected static L1CreateIndividualApplicantPartyDto ValidApplicantPartyDto(
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

    protected static L1CreateConnectionRequestDto ValidConnectionRequestDto(
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

    protected static L1CreateConnectionRequestDto ValidConnectionRequestWithNewApplicantDto(
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

    protected async Task<AccountRow?> GetAccountRowAsync(long id)
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

    protected async Task<ApplicantPartyRow?> GetApplicantPartyRowAsync(long id)
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

    protected async Task<long?> GetLatestApplicantPartyIdForAccountAsync(long accountId)
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

    protected async Task<int> GetApplicantPartyCountAsync(long accountId)
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

    protected async Task<RequestRow?> GetRequestRowAsync(long id)
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

    protected async Task<RequestRow?> GetLatestRequestRowForApplicantPartyAsync(long applicantPartyId)
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

    protected async Task<int> GetRequestCountAsync()
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

    protected async Task UpdateRequestStatusAsync(long requestId, string status)
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

    protected async Task SetApplicantCurrentFlagAsync(long applicantPartyId, bool isCurrent)
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

    protected async Task UpdateRequestCreatedAtAsync(long requestId, DateTimeOffset createdAt)
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

    protected async Task InsertRequestReviewAsync(
        long requestId,
        string reviewStatus,
        long startedByEmployeeId,
        DateTimeOffset startedAt,
        long? completedByEmployeeId = null,
        DateTimeOffset? completedAt = null,
        string? rejectionFeedback = null)
    {
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            """
            INSERT INTO dbo.L1RequestReviews
                (RequestId, Status, StartedByEmployeeId, StartedAt, CompletedByEmployeeId, CompletedAt, RejectionFeedback)
            VALUES
                (@requestId, @reviewStatus, @startedByEmployeeId, @startedAt, @completedByEmployeeId, @completedAt, @rejectionFeedback)
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        command.Parameters.AddWithValue("@requestId", requestId);
        command.Parameters.AddWithValue("@reviewStatus", reviewStatus);
        command.Parameters.AddWithValue("@startedByEmployeeId", startedByEmployeeId);
        command.Parameters.AddWithValue("@startedAt", startedAt);
        command.Parameters.AddWithValue("@completedByEmployeeId",
            completedByEmployeeId is null ? DBNull.Value : completedByEmployeeId);
        command.Parameters.AddWithValue("@completedAt",
            completedAt is null ? DBNull.Value : completedAt);
        command.Parameters.AddWithValue("@rejectionFeedback",
            rejectionFeedback is null ? DBNull.Value : rejectionFeedback);

        await command.ExecuteNonQueryAsync();
    }

    protected async Task UpdateRequestReviewAsync(
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

    protected async Task<SqlDataReader> ExecuteReaderAsync(string query, long id)
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

    protected async Task ExecuteNonQueryAsync<TValue>(string query, long id, TValue value)
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

    protected static string UniqueEmail()
    {
        return $"l1-{Guid.NewGuid():N}@example.com";
    }

    protected const string ValidPassword = "ValidPassword!123";
    protected const string FirstName = "John";
    protected const string MiddleName = "Michael";
    protected const string LastName = "Doe";
    protected const string ApplicantEmail = "applicant.l1@example.com";
    protected const string PhoneNumber = "79237554726";
    protected const string RequestDetails = "Connection request details for L1 integration test.";
    protected const string PostalCode = "123456";
    protected const string Region = "Region";
    protected const string City = "City";
    protected const string Street = "Street";
    protected const string House = "12";
    protected const string Building = "1";
    protected const string Apartment = "34";

    protected sealed record AccountRow(
        long Id,
        string AccountType,
        string Email,
        string PasswordHash,
        string Role,
        bool IsActive);

    protected sealed record ApplicantPartyRow(
        long Id,
        long ClientAccountId,
        string Email,
        string PhoneNumber,
        string FirstName,
        string MiddleName,
        string LastName,
        string VerificationStatus,
        bool IsCurrentActiveVersion);

    protected sealed record RequestRow(
        long Id,
        long ApplicantPartyId,
        string Status,
        string Details,
        string City,
        string Street);
}
