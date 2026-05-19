using Domain.EnergyManagement.DocumentManaging;
using EnergyManagement.Testing.TestDatabase;
using Microsoft.Data.SqlClient;

namespace EnergyManagement.Tools.TestDatabase;

public sealed class SeedE2eDemoDataCommand
{
    public const string EmployeeEmail = "e2e.employee@example.com";
    public const string ClientEmail = "e2e.client@example.com";
    public const string Password = "ValidPassword111!";

    public const long EmployeeId = 9001;
    public const long ClientAccountId = 9002;
    public const long ApplicantPartyId = 9003;
    public const long ReviewRequestId = 9004;
    public const long AgreementRequestId = 9005;

    public async Task<int> ExecuteAsync(string[] args, CancellationToken cancellationToken = default)
    {
        if (!TryParse(args, out var connectionString, out var error))
        {
            Console.Error.WriteLine(error);
            WriteUsage(Console.Error);
            return 1;
        }

        try
        {
            var manager = new TestDatabaseManager(connectionString);
            await manager.EnsureTablesCreatedAsync(cancellationToken);
            await SeedAsync(connectionString, cancellationToken);

            Console.WriteLine("E2E demo data seeded.");
            Console.WriteLine($"Employee: {EmployeeEmail} / {Password}");
            Console.WriteLine($"Client:   {ClientEmail} / {Password}");
            Console.WriteLine($"Review request id:    {ReviewRequestId}");
            Console.WriteLine($"Agreement request id: {AgreementRequestId}");

            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("E2E demo data seed failed:");
            Console.Error.WriteLine(exception);
            return 1;
        }
    }

    private static async Task SeedAsync(string connectionString, CancellationToken cancellationToken)
    {
        var passwordHash = PasswordHash.CreateFromPlainTextPassword(Password).Value.Value;
        var now = DateTimeOffset.UtcNow;

        const string sql = """
            BEGIN TRANSACTION;

            DELETE FROM dbo.L1AgreementProposals
            WHERE AgreementProposalExchangeId IN (
                SELECT Id FROM dbo.L1AgreementProposalExchanges
                WHERE RequestId IN (@reviewRequestId, @agreementRequestId)
            );

            DELETE FROM dbo.L1AgreementProposalExchanges
            WHERE RequestId IN (@reviewRequestId, @agreementRequestId);

            DELETE FROM dbo.L1RequestReviews
            WHERE RequestId IN (@reviewRequestId, @agreementRequestId);

                DELETE FROM dbo.L1ClientRequests
                WHERE Id IN (@reviewRequestId, @agreementRequestId);

                DELETE FROM dbo.L1ApplicantParties
                WHERE Id = @applicantPartyId;

            DELETE FROM dbo.L1Accounts
            WHERE Id IN (@employeeId, @clientAccountId);

            SET IDENTITY_INSERT dbo.L1Accounts ON;

            INSERT INTO dbo.L1Accounts
                (Id, Email, PasswordHash, Role, IsActive, CreatedAt, AccountType, WindowsLogin,
                 EmployeeFullName_FirstName, EmployeeFullName_MiddleName, EmployeeFullName_LastName)
            VALUES
                (@employeeId, @employeeEmail, @passwordHash, N'Employee', 1, @now, N'Employee', N'E2E\employee',
                 N'Employee', N'Demo', N'Reviewer');

            INSERT INTO dbo.L1Accounts
                (Id, Email, PasswordHash, Role, IsActive, CreatedAt, AccountType, WindowsLogin,
                 EmployeeFullName_FirstName, EmployeeFullName_MiddleName, EmployeeFullName_LastName)
            VALUES
                (@clientAccountId, @clientEmail, @passwordHash, N'Client', 1, @now, N'Client', NULL,
                 NULL, NULL, NULL);

            SET IDENTITY_INSERT dbo.L1Accounts OFF;

            SET IDENTITY_INSERT dbo.L1ApplicantParties ON;

            IF COL_LENGTH(N'dbo.L1ApplicantParties', N'ClientAccountId') IS NOT NULL
            BEGIN
                EXEC sp_executesql
                    N'INSERT INTO dbo.L1ApplicantParties
                        (Id, ClientAccountId, ApplicantPartyType, Email, PhoneNumber, CreatedAt,
                         ApplicantPartyDiscriminator, FullName_FirstName, FullName_MiddleName, FullName_LastName,
                         IsCurrentActiveVersion, VerificationStatus)
                    VALUES
                        (@applicantPartyId, @clientAccountId, N''Individual'', @clientEmail, N''+79001234567'', @now,
                         N''Individual'', N''Demo'', N''Client'', N''Applicant'', 1, N''Unverified'');',
                    N'@applicantPartyId bigint, @clientAccountId bigint, @clientEmail nvarchar(400), @now datetimeoffset',
                    @applicantPartyId = @applicantPartyId, @clientAccountId = @clientAccountId, @clientEmail = @clientEmail, @now = @now;
            END
            ELSE
            BEGIN
                INSERT INTO dbo.L1ApplicantParties
                    (Id, ApplicantPartyType, Email, PhoneNumber, CreatedAt,
                     ApplicantPartyDiscriminator, FullName_FirstName, FullName_MiddleName, FullName_LastName,
                     IsCurrentActiveVersion, VerificationStatus)
                VALUES
                    (@applicantPartyId, N'Individual', @clientEmail, N'+79001234567', @now,
                     N'Individual', N'Demo', N'Client', N'Applicant', 1, N'Unverified');
            END

            SET IDENTITY_INSERT dbo.L1ApplicantParties OFF;

            SET IDENTITY_INSERT dbo.L1ClientRequests ON;

            IF COL_LENGTH(N'dbo.L1ClientRequests', N'ClientAccountId') IS NOT NULL
            BEGIN
                EXEC sp_executesql
                    N'INSERT INTO dbo.L1ClientRequests
                        (Id, ApplicantPartyId, ClientAccountId, RequestType, Status, Details, CreatedAt,
                         ClientRequestDiscriminator, ObjectAddress_PostalCode, ObjectAddress_Region,
                         ObjectAddress_City, ObjectAddress_Street, ObjectAddress_House,
                         ObjectAddress_Building, ObjectAddress_Apartment)
                    VALUES
                        (@reviewRequestId, @applicantPartyId, @clientAccountId, N''Connection'', N''InReview'',
                         N''E2E employee review request.'', @now, N''Connection'', N''658480'', N''Алтайский край'',
                         N''Заринск'', N''Ленина'', N''10'', NULL, NULL);

                    INSERT INTO dbo.L1ClientRequests
                        (Id, ApplicantPartyId, ClientAccountId, RequestType, Status, Details, CreatedAt,
                         ClientRequestDiscriminator, ObjectAddress_PostalCode, ObjectAddress_Region,
                         ObjectAddress_City, ObjectAddress_Street, ObjectAddress_House,
                         ObjectAddress_Building, ObjectAddress_Apartment)
                    VALUES
                        (@agreementRequestId, @applicantPartyId, @clientAccountId, N''Connection'', N''Approved'',
                         N''E2E agreement exchange request.'', @now, N''Connection'', N''658480'', N''Алтайский край'',
                         N''Заринск'', N''Ленина'', N''12'', NULL, NULL);',
                    N'@reviewRequestId bigint, @applicantPartyId bigint, @clientAccountId bigint, @agreementRequestId bigint, @now datetimeoffset',
                    @reviewRequestId = @reviewRequestId, @applicantPartyId = @applicantPartyId, @clientAccountId = @clientAccountId, @agreementRequestId = @agreementRequestId, @now = @now;
            END
            ELSE
            BEGIN
                INSERT INTO dbo.L1ClientRequests
                    (Id, ApplicantPartyId, RequestType, Status, Details, CreatedAt,
                     ClientRequestDiscriminator, ObjectAddress_PostalCode, ObjectAddress_Region,
                     ObjectAddress_City, ObjectAddress_Street, ObjectAddress_House,
                     ObjectAddress_Building, ObjectAddress_Apartment)
                VALUES
                    (@reviewRequestId, @applicantPartyId, N'Connection', N'InReview',
                     N'E2E employee review request.', @now, N'Connection', N'658480', N'Алтайский край',
                     N'Заринск', N'Ленина', N'10', NULL, NULL);

                INSERT INTO dbo.L1ClientRequests
                    (Id, ApplicantPartyId, RequestType, Status, Details, CreatedAt,
                     ClientRequestDiscriminator, ObjectAddress_PostalCode, ObjectAddress_Region,
                     ObjectAddress_City, ObjectAddress_Street, ObjectAddress_House,
                     ObjectAddress_Building, ObjectAddress_Apartment)
                VALUES
                    (@agreementRequestId, @applicantPartyId, N'Connection', N'Approved',
                     N'E2E agreement exchange request.', @now, N'Connection', N'658480', N'Алтайский край',
                     N'Заринск', N'Ленина', N'12', NULL, NULL);
            END

            SET IDENTITY_INSERT dbo.L1ClientRequests OFF;

            IF COL_LENGTH(N'dbo.L1RequestReviews', N'ClientAccountId') IS NOT NULL
            BEGIN
                EXEC sp_executesql
                    N'INSERT INTO dbo.L1RequestReviews
                        (RequestId, ClientAccountId, Status, StartedByEmployeeId, StartedAt,
                         CompletedByEmployeeId, CompletedAt, RejectionReason)
                    VALUES
                        (@agreementRequestId, @clientAccountId, N''Approved'', @employeeId, DATEADD(minute, -20, @now),
                         @employeeId, DATEADD(minute, -10, @now), NULL);',
                    N'@agreementRequestId bigint, @clientAccountId bigint, @employeeId bigint, @now datetimeoffset',
                    @agreementRequestId = @agreementRequestId, @clientAccountId = @clientAccountId, @employeeId = @employeeId, @now = @now;
            END
            ELSE
            BEGIN
                INSERT INTO dbo.L1RequestReviews
                    (RequestId, Status, StartedByEmployeeId, StartedAt,
                     CompletedByEmployeeId, CompletedAt, RejectionReason)
                VALUES
                    (@agreementRequestId, N'Approved', @employeeId, DATEADD(minute, -20, @now),
                     @employeeId, DATEADD(minute, -10, @now), NULL);
            END

            COMMIT TRANSACTION;
            """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@employeeId", EmployeeId);
        command.Parameters.AddWithValue("@clientAccountId", ClientAccountId);
        command.Parameters.AddWithValue("@applicantPartyId", ApplicantPartyId);
        command.Parameters.AddWithValue("@reviewRequestId", ReviewRequestId);
        command.Parameters.AddWithValue("@agreementRequestId", AgreementRequestId);
        command.Parameters.AddWithValue("@employeeEmail", EmployeeEmail);
        command.Parameters.AddWithValue("@clientEmail", ClientEmail);
        command.Parameters.AddWithValue("@passwordHash", passwordHash);
        command.Parameters.AddWithValue("@now", now);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static bool TryParse(string[] args, out string connectionString, out string error)
    {
        connectionString = TestDatabaseDefaults.LocalDbConnectionString;
        error = string.Empty;

        for (var index = 1; index < args.Length; index++)
        {
            var arg = args[index];

            if (arg == "--connection")
            {
                if (index + 1 >= args.Length)
                {
                    error = "Missing value for --connection.";
                    return false;
                }

                connectionString = args[++index];
                continue;
            }

            error = $"Unknown argument: {arg}";
            return false;
        }

        return true;
    }

    private static void WriteUsage(TextWriter writer)
    {
        writer.WriteLine("Usage:");
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- seed-e2e-demo-data [--connection <connection string>]");
    }
}
