using System.Data;
using EnergyManagement.Server.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnergyManagement.Testing.TestDatabase;

public sealed class TestDatabaseManager
{
    private readonly string _connectionString;

    public TestDatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task ResetAsync(CancellationToken cancellationToken = default)
    {
        await EnsureTablesCreatedAsync(cancellationToken);
        await ClearAsync(cancellationToken);
    }

    public async Task EnsureTablesCreatedAsync(CancellationToken cancellationToken = default)
    {
        await EnsureDatabaseCreatedAsync(cancellationToken);
        await EnsureApplicationTablesCreatedAsync(cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        const string query = """
            BEGIN TRANSACTION;
            IF OBJECT_ID(N'dbo.AgreementProposals', N'U') IS NOT NULL DELETE FROM dbo.AgreementProposals;
            IF OBJECT_ID(N'dbo.AgreementProposalExchanges', N'U') IS NOT NULL DELETE FROM dbo.AgreementProposalExchanges;
            IF OBJECT_ID(N'dbo.RequestReviews', N'U') IS NOT NULL DELETE FROM dbo.RequestReviews;
            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL DELETE FROM dbo.ClientRequests;
            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL DELETE FROM dbo.ApplicantParties;
            IF OBJECT_ID(N'dbo.Accounts', N'U') IS NOT NULL DELETE FROM dbo.Accounts;
            COMMIT TRANSACTION;
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureDatabaseCreatedAsync(CancellationToken cancellationToken)
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        var catalog = builder.InitialCatalog;
        if (string.IsNullOrWhiteSpace(catalog))
        {
            throw new InvalidOperationException("The test database connection string must include an Initial Catalog.");
        }

        builder.InitialCatalog = "master";

        await using var connection = new SqlConnection(builder.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        var databaseName = QuoteSqlIdentifier(catalog);
        await using var command = new SqlCommand(
            $"IF DB_ID(@databaseName) IS NULL CREATE DATABASE {databaseName};",
            connection);
        command.Parameters.AddWithValue("@databaseName", catalog);

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureApplicationTablesCreatedAsync(CancellationToken cancellationToken)
    {
        if (await TableExistsAsync("Accounts", cancellationToken))
        {
            await EnsureApplicantPartyCurrentVersionColumnAsync(cancellationToken);
            await EnsureApplicantPartyVerificationStatusColumnAsync(cancellationToken);
            await EnsureApplicantPartiesClientAccountIdColumnAsync(cancellationToken);
            await EnsureApplicantPartySubtypeRequisiteColumnsAsync(cancellationToken);
            await EnsureClientRequestClientAccountIdColumnAsync(cancellationToken);
            await EnsureRequestReviewsTableAsync(cancellationToken);
            await EnsureAgreementProposalTablesAsync(cancellationToken);
            await EnsureAccountEmployeeColumnsAsync(cancellationToken);
            return;
        }

        await using var context = new EnergyManagementDbContext(_connectionString);
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
        await databaseCreator.CreateTablesAsync(cancellationToken);
        await EnsureApplicantPartiesClientAccountIdColumnAsync(cancellationToken);
        await EnsureApplicantPartySubtypeRequisiteColumnsAsync(cancellationToken);
        await EnsureClientRequestClientAccountIdColumnAsync(cancellationToken);
        await EnsureRequestReviewsTableAsync(cancellationToken);
        await EnsureAgreementProposalTablesAsync(cancellationToken);
        await EnsureAccountEmployeeColumnsAsync(cancellationToken);
    }

    private async Task EnsureApplicantPartiesClientAccountIdColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ApplicantParties', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.ApplicantParties
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_ApplicantParties_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ApplicantParties', N'ClientAccountId') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ApplicantParties_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.ApplicantParties'))
            BEGIN
                EXEC(N'CREATE INDEX IX_ApplicantParties_ClientAccountId
                    ON dbo.ApplicantParties(ClientAccountId);');
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }


    private async Task EnsureClientRequestClientAccountIdColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ClientRequests', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.ClientRequests
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_ClientRequests_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ClientRequests', N'ClientAccountId') IS NOT NULL
            BEGIN
                EXEC(N'UPDATE requests
                SET ClientAccountId = applicantParties.ClientAccountId
                FROM dbo.ClientRequests AS requests
                INNER JOIN dbo.ApplicantParties AS applicantParties
                    ON requests.ApplicantPartyId = applicantParties.Id
                WHERE requests.ClientAccountId = 0;');
            END

            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_ClientRequests_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.ClientRequests'))
            BEGIN
                EXEC(N'CREATE INDEX IX_ClientRequests_ClientAccountId
                    ON dbo.ClientRequests(ClientAccountId);');
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureApplicantPartySubtypeRequisiteColumnsAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL
            BEGIN
                IF COL_LENGTH(N'dbo.ApplicantParties', N'ApplicantPartyDiscriminator') IS NOT NULL
                BEGIN
                    ALTER TABLE dbo.ApplicantParties
                    ALTER COLUMN ApplicantPartyDiscriminator nvarchar(34) NOT NULL;
                END

                IF COL_LENGTH(N'dbo.ApplicantParties', N'IndividualEntrepreneurFullName_FirstName') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD IndividualEntrepreneurFullName_FirstName nvarchar(100) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'IndividualEntrepreneurFullName_MiddleName') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD IndividualEntrepreneurFullName_MiddleName nvarchar(100) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'IndividualEntrepreneurFullName_LastName') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD IndividualEntrepreneurFullName_LastName nvarchar(100) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'IndividualEntrepreneur_Inn') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD IndividualEntrepreneur_Inn nvarchar(12) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'IndividualEntrepreneur_Ogrnip') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD IndividualEntrepreneur_Ogrnip nvarchar(15) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'LegalEntity_OrganizationName') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD LegalEntity_OrganizationName nvarchar(250) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'LegalEntity_Inn') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD LegalEntity_Inn nvarchar(10) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'LegalEntity_Kpp') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD LegalEntity_Kpp nvarchar(9) NULL;

                IF COL_LENGTH(N'dbo.ApplicantParties', N'LegalEntity_Ogrn') IS NULL
                    ALTER TABLE dbo.ApplicantParties ADD LegalEntity_Ogrn nvarchar(13) NULL;
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }


    private async Task EnsureRequestReviewsTableAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.RequestReviews', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.RequestReviews
                (
                    RequestId bigint NOT NULL,
                    ClientAccountId bigint NOT NULL,
                    Status nvarchar(50) NOT NULL,
                    StartedByEmployeeId bigint NOT NULL,
                    StartedAt datetimeoffset NOT NULL,
                    CompletedByEmployeeId bigint NULL,
                    CompletedAt datetimeoffset NULL,
                    RejectionReason nvarchar(1000) NULL,
                    CONSTRAINT PK_RequestReviews PRIMARY KEY (RequestId),
                    CONSTRAINT FK_RequestReviews_ClientRequests_RequestId
                        FOREIGN KEY (RequestId) REFERENCES dbo.ClientRequests(Id)
                        ON DELETE CASCADE
                );
            END

            IF OBJECT_ID(N'dbo.RequestReviews', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.RequestReviews', N'RejectionReason') IS NULL
            BEGIN
                ALTER TABLE dbo.RequestReviews
                ADD RejectionReason nvarchar(1000) NULL;
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }


    private async Task EnsureAgreementProposalTablesAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ClientRequests', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.AgreementProposalExchanges', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.AgreementProposalExchanges
                (
                    Id bigint IDENTITY(1,1) NOT NULL,
                    RequestId bigint NOT NULL,
                    ClientAccountId bigint NOT NULL,
                    Status nvarchar(50) NOT NULL,
                    ActiveProposalVersion int NOT NULL,
                    FinalRefusedByEmployeeId bigint NULL,
                    FinalRefusedAt datetimeoffset NULL,
                    FinalRefusalReason nvarchar(2000) NULL,
                    CreatedAt datetimeoffset NOT NULL,
                    CONSTRAINT PK_AgreementProposalExchanges PRIMARY KEY (Id),
                    CONSTRAINT FK_AgreementProposalExchanges_ClientRequests_RequestId
                        FOREIGN KEY (RequestId) REFERENCES dbo.ClientRequests(Id)
                        ON DELETE NO ACTION
                );

                CREATE INDEX IX_AgreementProposalExchanges_RequestId
                    ON dbo.AgreementProposalExchanges(RequestId);

                EXEC(N'CREATE INDEX IX_AgreementProposalExchanges_ClientAccountId
                    ON dbo.AgreementProposalExchanges(ClientAccountId);');
            END

            IF OBJECT_ID(N'dbo.AgreementProposalExchanges', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.AgreementProposalExchanges', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.AgreementProposalExchanges
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_AgreementProposalExchanges_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.AgreementProposalExchanges', N'U') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_AgreementProposalExchanges_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.AgreementProposalExchanges'))
            BEGIN
                EXEC(N'CREATE INDEX IX_AgreementProposalExchanges_ClientAccountId
                    ON dbo.AgreementProposalExchanges(ClientAccountId);');
            END

            IF OBJECT_ID(N'dbo.AgreementProposalExchanges', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.AgreementProposals', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.AgreementProposals
                (
                    Id bigint IDENTITY(1,1) NOT NULL,
                    AgreementProposalExchangeId bigint NOT NULL,
                    Version int NOT NULL,
                    Sender nvarchar(50) NOT NULL,
                    SenderId bigint NOT NULL,
                    State nvarchar(50) NOT NULL,
                    DocumentStorageKey nvarchar(500) NOT NULL,
                    DocumentOriginalFileName nvarchar(255) NOT NULL,
                    DocumentContentType nvarchar(100) NOT NULL,
                    DocumentSizeBytes bigint NOT NULL,
                    Comment nvarchar(2000) NULL,
                    CreatedAt datetimeoffset NOT NULL,
                    CONSTRAINT PK_AgreementProposals PRIMARY KEY (Id),
                    CONSTRAINT FK_AgreementProposals_AgreementProposalExchanges_AgreementProposalExchangeId
                        FOREIGN KEY (AgreementProposalExchangeId) REFERENCES dbo.AgreementProposalExchanges(Id)
                        ON DELETE CASCADE
                );

                CREATE INDEX IX_AgreementProposals_AgreementProposalExchangeId
                    ON dbo.AgreementProposals(AgreementProposalExchangeId);
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureAccountEmployeeColumnsAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.Accounts', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.Accounts', N'EmployeeFullName_FirstName') IS NULL
            BEGIN
                ALTER TABLE dbo.Accounts
                ADD EmployeeFullName_FirstName nvarchar(100) NULL,
                    EmployeeFullName_MiddleName nvarchar(100) NULL,
                    EmployeeFullName_LastName nvarchar(100) NULL;
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureApplicantPartyCurrentVersionColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ApplicantParties', N'IsCurrentActiveVersion') IS NULL
            BEGIN
                ALTER TABLE dbo.ApplicantParties
                ADD IsCurrentActiveVersion bit NOT NULL
                    CONSTRAINT DF_ApplicantParties_IsCurrentActiveVersion DEFAULT 1;
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task EnsureApplicantPartyVerificationStatusColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.ApplicantParties', N'VerificationStatus') IS NULL
            BEGIN
                ALTER TABLE dbo.ApplicantParties
                ADD VerificationStatus nvarchar(50) NOT NULL
                    CONSTRAINT DF_ApplicantParties_VerificationStatus DEFAULT N'Unverified';
            END
            """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<bool> TableExistsAsync(string tableName, CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(
            "SELECT CASE WHEN OBJECT_ID(@tableName, N'U') IS NULL THEN 0 ELSE 1 END",
            connection);
        command.Parameters.AddWithValue("@tableName", $"dbo.{tableName}");

        var scalar = await command.ExecuteScalarAsync(cancellationToken)
            ?? throw new InvalidOperationException($"Could not check {tableName} table existence.");

        return (int)scalar == 1;
    }

    private static string QuoteSqlIdentifier(string value)
    {
        return $"[{value.Replace("]", "]]", StringComparison.Ordinal)}]";
    }
}
