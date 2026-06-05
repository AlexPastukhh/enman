using System.Data;
using System.Net.Http.Json;
using System.Security.Claims;
using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Server.Api.Security;
using EnergyManagement.Server.Api;
using EnergyManagement.Server.Api.Contracts.ApplicantParties;
using EnergyManagement.Server.Application.Commands;
using EnergyManagement.Server.Application.ApplicantParties.Commands;
using EnergyManagement.Server.Application.Security;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Tests.EnergyManagement.TestHelpers;
using Xunit.Abstractions;

namespace Tests.EnergyManagement.Integration.App;

public abstract class AppIntegrationTestBase
{
    protected readonly IntegrationTestFixture _fixture;
    protected readonly WebAppFactory _factory;
    protected readonly ITestOutputHelper _output;

    protected AppIntegrationTestBase(IntegrationTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _factory = fixture.Factory;
        _output = output;
    }

    protected async Task<RegisterClientAccountResponse> RegisterAccountAsync(string? email = null)
    {
        var client = _factory.CreateClient();
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/register",
            new RegisterClientAccountDto(email ?? UniqueEmail(), ValidPassword));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<RegisterClientAccountResponse>()
            ?? throw new InvalidOperationException("Register response body was empty.");
    }

    protected async Task<CurrentUserResponseDto> LoginAsync(
        HttpClient client,
        string email,
        string password)
    {
        var response = await PostAsJsonWithCsrfAsync(
            client,
            "/api/auth/login",
            new LoginRequestDto(email, password));

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<CurrentUserResponseDto>()
            ?? throw new InvalidOperationException("Login response body was empty.");
    }

    protected async Task<CurrentUserResponseDto> GetCurrentUserAsync(HttpClient client)
    {
        var response = await client.GetAsync("/api/auth/current-user");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<CurrentUserResponseDto>()
            ?? throw new InvalidOperationException("Current-user response body was empty.");
    }

    protected async Task<CurrentIndividualApplicantPartyResponseDto> GetCurrentIndividualApplicantPartyAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/applicant-parties/current-individual");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<CurrentIndividualApplicantPartyResponseDto>()
            ?? throw new InvalidOperationException("Current applicant party response body was empty.");
    }

    protected async Task<AccountApplicantPartiesResponseDto> GetAccountApplicantPartiesAsync(
        HttpClient client)
    {
        var response = await client.GetAsync("/api/applicant-parties");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<AccountApplicantPartiesResponseDto>()
            ?? throw new InvalidOperationException("Account applicant parties response body was empty.");
    }

    protected async Task<IReadOnlyList<MyRequestSummaryDto>> GetMyRequestsAsync(
        HttpClient client,
        string? status = null)
    {
        var path = status is null
            ? "/api/requests"
            : $"/api/requests?status={Uri.EscapeDataString(status)}";
        var response = await client.GetAsync(path);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<IReadOnlyList<MyRequestSummaryDto>>()
            ?? throw new InvalidOperationException("My requests response body was empty.");
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


    protected async Task<EmployeeRequestDetailsDto> GetEmployeeRequestDetailsAsync(
        HttpClient client,
        long requestId)
    {
        var response = await client.GetAsync($"/api/employee/requests/{requestId}");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<EmployeeRequestDetailsDto>()
            ?? throw new InvalidOperationException("Employee request details response body was empty.");
    }

    protected Task<HttpResponseMessage> StartEmployeeRequestReviewRequestAsync(
        HttpClient client,
        long requestId)
    {
        return PostWithCsrfAsync(client, $"/api/employee/requests/{requestId}/review/start");
    }

    protected async Task StartEmployeeRequestReviewAsync(
        HttpClient client,
        long requestId)
    {
        var response = await StartEmployeeRequestReviewRequestAsync(client, requestId);

        await HttpResponseAssertions.For(response, _output).ShouldBeStatusCode((int)System.Net.HttpStatusCode.NoContent);
    }


    protected Task<HttpResponseMessage> ApproveEmployeeRequestReviewRequestAsync(
        HttpClient client,
        long requestId)
    {
        return PostWithCsrfAsync(client, $"/api/employee/requests/{requestId}/review/approve");
    }

    protected async Task ApproveEmployeeRequestReviewAsync(
        HttpClient client,
        long requestId)
    {
        var response = await ApproveEmployeeRequestReviewRequestAsync(client, requestId);

        await HttpResponseAssertions.For(response, _output).ShouldBeStatusCode((int)System.Net.HttpStatusCode.NoContent);
    }



    protected Task<HttpResponseMessage> RejectEmployeeRequestReviewRequestAsync(
        HttpClient client,
        long requestId,
        string? feedback = "Request rejected by employee.")
    {
        return PostAsJsonWithCsrfAsync(
            client,
            $"/api/employee/requests/{requestId}/review/reject",
            new EmployeeRejectRequestReviewDto(feedback));
    }

    protected async Task RejectEmployeeRequestReviewAsync(
        HttpClient client,
        long requestId,
        string? feedback = "Request rejected by employee.")
    {
        var response = await RejectEmployeeRequestReviewRequestAsync(client, requestId, feedback);

        await HttpResponseAssertions.For(response, _output).ShouldBeStatusCode((int)System.Net.HttpStatusCode.NoContent);
    }

    protected async Task<MyRequestDetailsDto> GetMyRequestDetailsAsync(
        HttpClient client,
        long requestId)
    {
        var response = await client.GetAsync($"/api/requests/{requestId}");

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<MyRequestDetailsDto>()
            ?? throw new InvalidOperationException("My request details response body was empty.");
    }

    protected async Task<CreateApplicantPartyResponse> CreateApplicantPartyAsync(long accountId)
    {
        return await CreateApplicantPartyAsync(accountId, ValidApplicantPartyDto());
    }

    protected async Task<CreateApplicantPartyResponse> CreateApplicantPartyAsync(
        long accountId,
        CreateIndividualApplicantPartyDto dto)
    {
        var response = await PostAsJsonWithCsrfAsync(
            AuthenticatedClient(accountId),
            "/api/applicant-parties/individual",
            dto);

        await HttpResponseAssertions.For(response, _output).ShouldBeSuccess();

        return await response.Content.ReadFromJsonAsync<CreateApplicantPartyResponse>()
            ?? throw new InvalidOperationException("Applicant party response body was empty.");
    }

    protected Task<HttpResponseMessage> MakeApplicantPartyCurrentDefaultRequestAsync(
        long accountId,
        long applicantPartyId)
    {
        return PostWithCsrfAsync(
            AuthenticatedClient(accountId),
            $"/api/applicant-parties/{applicantPartyId}/make-current-default");
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
            AuthenticatedClient(accountId),
            "/api/requests",
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

    protected HttpClient AuthenticatedClient(
        long accountId,
        string email = "l1-authenticated@example.com",
        string role = "Client")
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(AuthClaimTypes.AuthModel, AuthClaimTypes.AuthModelValue))
            .CreateClient();
    }

    protected HttpClient LegacyShapedAuthenticatedClient(long accountId)
    {
        return _factory.AuthenticatedInstanceWithClaims(
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
            new Claim(ClaimTypes.Email, "legacy-shaped@example.com"))
            .CreateClient();
    }

    protected static CreateIndividualApplicantPartyDto ValidApplicantPartyDto(
        string firstName = FirstName,
        string middleName = MiddleName,
        string lastName = LastName,
        string email = ApplicantEmail,
        string phoneNumber = PhoneNumber)
    {
        return new CreateIndividualApplicantPartyDto(
            new FullNameDto(firstName, middleName, lastName),
            email,
            phoneNumber);
    }

    protected static CreateConnectionRequestDto ValidConnectionRequestDto(
        string details = RequestDetails,
        string? applicantContextType = "Existing",
        long? existingApplicantPartyId = 1,
        CreateIndividualApplicantPartyDto? newApplicantParty = null)
    {
        return new CreateConnectionRequestDto(
            applicantContextType,
            existingApplicantPartyId,
            ToInlineApplicantPartyForRequestDto(newApplicantParty),
            details,
            new AddressDto(
                PostalCode,
                Region,
                City,
                Street,
                House,
                Building,
                Apartment));
    }

    protected static CreateConnectionRequestDto ValidConnectionRequestWithNewApplicantDto(
        string details = RequestDetails,
        CreateIndividualApplicantPartyDto? newApplicantParty = null,
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

    protected static InlineApplicantPartyForRequestDto? ToInlineApplicantPartyForRequestDto(
        CreateIndividualApplicantPartyDto? applicantParty)
    {
        return applicantParty is null
            ? null
            : new InlineApplicantPartyForRequestDto(
                "Individual",
                applicantParty.FullName,
                OrganizationName: null,
                Inn: null,
                Kpp: null,
                Ogrn: null,
                Ogrnip: null,
                applicantParty.Email,
                applicantParty.PhoneNumber);
    }

    protected async Task<AccountRow?> GetAccountRowAsync(long id)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, AccountType, Email, PasswordHash, Role, IsActive
            FROM dbo.Accounts
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
                   ApplicantPartyType,
                   FullName_FirstName, FullName_MiddleName, FullName_LastName,
                   IndividualEntrepreneurFullName_FirstName,
                   IndividualEntrepreneurFullName_MiddleName,
                   IndividualEntrepreneurFullName_LastName,
                   IndividualEntrepreneur_Inn,
                   IndividualEntrepreneur_Ogrnip,
                   LegalEntity_OrganizationName,
                   LegalEntity_Inn,
                   LegalEntity_Kpp,
                   LegalEntity_Ogrn
            FROM dbo.ApplicantParties
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
            reader.GetString("ApplicantPartyType"),
            reader.IsDBNull("FullName_FirstName") ? null : reader.GetString("FullName_FirstName"),
            reader.IsDBNull("FullName_MiddleName") ? null : reader.GetString("FullName_MiddleName"),
            reader.IsDBNull("FullName_LastName") ? null : reader.GetString("FullName_LastName"),
            reader.IsDBNull("IndividualEntrepreneurFullName_FirstName") ? null : reader.GetString("IndividualEntrepreneurFullName_FirstName"),
            reader.IsDBNull("IndividualEntrepreneurFullName_MiddleName") ? null : reader.GetString("IndividualEntrepreneurFullName_MiddleName"),
            reader.IsDBNull("IndividualEntrepreneurFullName_LastName") ? null : reader.GetString("IndividualEntrepreneurFullName_LastName"),
            reader.IsDBNull("IndividualEntrepreneur_Inn") ? null : reader.GetString("IndividualEntrepreneur_Inn"),
            reader.IsDBNull("IndividualEntrepreneur_Ogrnip") ? null : reader.GetString("IndividualEntrepreneur_Ogrnip"),
            reader.IsDBNull("LegalEntity_OrganizationName") ? null : reader.GetString("LegalEntity_OrganizationName"),
            reader.IsDBNull("LegalEntity_Inn") ? null : reader.GetString("LegalEntity_Inn"),
            reader.IsDBNull("LegalEntity_Kpp") ? null : reader.GetString("LegalEntity_Kpp"),
            reader.IsDBNull("LegalEntity_Ogrn") ? null : reader.GetString("LegalEntity_Ogrn"),
            reader.GetString("VerificationStatus"),
            reader.GetBoolean("IsCurrentActiveVersion"));
    }

    protected async Task<long?> GetLatestApplicantPartyIdForAccountAsync(long accountId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT TOP (1) Id
            FROM dbo.ApplicantParties
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
            "SELECT COUNT(*) FROM dbo.ApplicantParties WHERE ClientAccountId = @id",
            connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@id", accountId);

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not read ApplicantParties count.");

        return (int)scalar;
    }

    protected async Task<RequestRow?> GetRequestRowAsync(long id)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT Id, ApplicantPartyId, Status, Details,
                   ObjectAddress_City, ObjectAddress_Street
            FROM dbo.ClientRequests
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
            FROM dbo.ClientRequests
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
            "SELECT COUNT(*) FROM dbo.ClientRequests",
            connection)
        {
            CommandType = CommandType.Text
        };

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not read ClientRequests count.");

        return (int)scalar;
    }

    protected async Task UpdateRequestStatusAsync(long requestId, string status)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.ClientRequests
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
            UPDATE dbo.ApplicantParties
            SET IsCurrentActiveVersion = @value
            WHERE Id = @id
            """,
            applicantPartyId,
            isCurrent);
    }

    protected async Task UpdateApplicantVerificationStatusAsync(
        long applicantPartyId,
        string verificationStatus)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.ApplicantParties
            SET VerificationStatus = @value
            WHERE Id = @id
            """,
            applicantPartyId,
            verificationStatus);
    }

    protected async Task UpdateRequestCreatedAtAsync(long requestId, DateTimeOffset createdAt)
    {
        await ExecuteNonQueryAsync(
            """
            UPDATE dbo.ClientRequests
            SET CreatedAt = @value
            WHERE Id = @id
            """,
            requestId,
            createdAt);
    }

    protected async Task InsertEmployeeAsync(
        long employeeId,
        string firstName = "Employee",
        string middleName = "Review",
        string lastName = "User",
        bool isActive = true,
        string? email = null,
        string? windowsLogin = null)
    {
        var passwordHash = PasswordHash.CreateFromPlainTextPassword(ValidPassword).Value.Value;
        await using var connection = new SqlConnection(_fixture.ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            """
            IF NOT EXISTS (SELECT 1 FROM dbo.Accounts WHERE Id = @employeeId)
            BEGIN
                SET IDENTITY_INSERT dbo.Accounts ON;

                INSERT INTO dbo.Accounts
                    (Id, Email, PasswordHash, Role, IsActive, CreatedAt, AccountType, WindowsLogin,
                     EmployeeFullName_FirstName, EmployeeFullName_MiddleName, EmployeeFullName_LastName)
                VALUES
                    (@employeeId, @email, @passwordHash, N'Employee', @isActive, @createdAt, N'Employee', @windowsLogin,
                     @firstName, @middleName, @lastName);

                SET IDENTITY_INSERT dbo.Accounts OFF;
            END
            """,
            connection)
        {
            CommandType = CommandType.Text
        };

        command.Parameters.AddWithValue("@employeeId", employeeId);
        command.Parameters.AddWithValue("@email", email ?? $"employee-{employeeId}@example.com");
        command.Parameters.AddWithValue("@passwordHash", passwordHash);
        command.Parameters.AddWithValue("@windowsLogin", (object?)windowsLogin ?? DBNull.Value);
        command.Parameters.AddWithValue("@firstName", firstName);
        command.Parameters.AddWithValue("@middleName", middleName);
        command.Parameters.AddWithValue("@lastName", lastName);
        command.Parameters.AddWithValue("@isActive", isActive);
        command.Parameters.AddWithValue("@createdAt", DateTimeOffset.UtcNow);

        await command.ExecuteNonQueryAsync();
    }

    protected async Task<RequestReviewRow?> GetRequestReviewRowAsync(long requestId)
    {
        await using var reader = await ExecuteReaderAsync(
            """
            SELECT RequestId, Status, StartedByEmployeeId, StartedAt, CompletedByEmployeeId, CompletedAt, RejectionReason
            FROM dbo.RequestReviews
            WHERE RequestId = @id
            """,
            requestId);

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new RequestReviewRow(
            reader.GetInt64("RequestId"),
            reader.GetString("Status"),
            reader.GetInt64("StartedByEmployeeId"),
            reader.GetDateTimeOffset(reader.GetOrdinal("StartedAt")),
            reader.IsDBNull("CompletedByEmployeeId") ? null : reader.GetInt64("CompletedByEmployeeId"),
            reader.IsDBNull("CompletedAt") ? null : reader.GetDateTimeOffset(reader.GetOrdinal("CompletedAt")),
            reader.IsDBNull("RejectionReason") ? null : reader.GetString("RejectionReason"));
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
            INSERT INTO dbo.RequestReviews
                (RequestId, Status, StartedByEmployeeId, StartedAt, CompletedByEmployeeId, CompletedAt, RejectionReason)
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
            UPDATE dbo.ClientRequests
            SET Status = @status
            WHERE Id = @id;

            MERGE dbo.RequestReviews AS target
            USING (SELECT @id AS RequestId) AS source
                ON target.RequestId = source.RequestId
            WHEN MATCHED THEN
                UPDATE SET
                    Status = @decision,
                    CompletedByEmployeeId = @reviewerId,
                    CompletedAt = @decidedAt,
                    RejectionReason = @rejectionReason
            WHEN NOT MATCHED THEN
                INSERT (RequestId, Status, StartedByEmployeeId, StartedAt, CompletedByEmployeeId, CompletedAt, RejectionReason)
                VALUES (@id, @decision, @reviewerId, @decidedAt, @reviewerId, @decidedAt, @rejectionReason);
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
    protected const string RequestDetails = "Connection request details for app integration test.";
    protected const string PostalCode = "123456";
    protected const string Region = "Region";
    protected const string City = "City";
    protected const string Street = "Street";
    protected const string House = "12";
    protected const string Building = "1";
    protected const string Apartment = "34";

    protected sealed record RequestReviewRow(
        long RequestId,
        string Status,
        long StartedByEmployeeId,
        DateTimeOffset StartedAt,
        long? CompletedByEmployeeId,
        DateTimeOffset? CompletedAt,
        string? RejectionReason);

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
        string ApplicantPartyType,
        string? FirstName,
        string? MiddleName,
        string? LastName,
        string? IndividualEntrepreneurFirstName,
        string? IndividualEntrepreneurMiddleName,
        string? IndividualEntrepreneurLastName,
        string? IndividualEntrepreneurInn,
        string? IndividualEntrepreneurOgrnip,
        string? LegalEntityOrganizationName,
        string? LegalEntityInn,
        string? LegalEntityKpp,
        string? LegalEntityOgrn,
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
