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
        await EnsureL1TablesCreatedAsync(cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        const string query = """
            BEGIN TRANSACTION;
            IF OBJECT_ID(N'dbo.L1AgreementProposals', N'U') IS NOT NULL DELETE FROM dbo.L1AgreementProposals;
            IF OBJECT_ID(N'dbo.L1AgreementProposalExchanges', N'U') IS NOT NULL DELETE FROM dbo.L1AgreementProposalExchanges;
            IF OBJECT_ID(N'dbo.L1RequestReviews', N'U') IS NOT NULL DELETE FROM dbo.L1RequestReviews;
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL DELETE FROM dbo.L1ClientRequests;
            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL DELETE FROM dbo.L1ApplicantParties;
            IF OBJECT_ID(N'dbo.L1Accounts', N'U') IS NOT NULL DELETE FROM dbo.L1Accounts;
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

    private async Task EnsureL1TablesCreatedAsync(CancellationToken cancellationToken)
    {
        if (await TableExistsAsync("L1Accounts", cancellationToken))
        {
            await EnsureL1ApplicantPartyCurrentVersionColumnAsync(cancellationToken);
            await EnsureL1ApplicantPartyVerificationStatusColumnAsync(cancellationToken);
            await EnsureL1ApplicantPartiesClientAccountIdColumnAsync(cancellationToken);
            await EnsureL1ClientRequestClientAccountIdColumnAsync(cancellationToken);
            await EnsureL1RequestReviewsTableAsync(cancellationToken);
            await EnsureL1AgreementProposalTablesAsync(cancellationToken);
            await EnsureL1AccountEmployeeColumnsAsync(cancellationToken);
            return;
        }

        await using var context = new EnergyManagementDbContext(_connectionString);
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
        await databaseCreator.CreateTablesAsync(cancellationToken);
        await EnsureL1ApplicantPartiesClientAccountIdColumnAsync(cancellationToken);
        await EnsureL1ClientRequestClientAccountIdColumnAsync(cancellationToken);
        await EnsureL1RequestReviewsTableAsync(cancellationToken);
        await EnsureL1AgreementProposalTablesAsync(cancellationToken);
        await EnsureL1AccountEmployeeColumnsAsync(cancellationToken);
    }

    private async Task EnsureL1ApplicantPartiesClientAccountIdColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ApplicantParties', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ApplicantParties
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_L1ApplicantParties_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ApplicantParties', N'ClientAccountId') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_L1ApplicantParties_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.L1ApplicantParties'))
            BEGIN
                CREATE INDEX IX_L1ApplicantParties_ClientAccountId
                    ON dbo.L1ApplicantParties(ClientAccountId);
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


    private async Task EnsureL1ClientRequestClientAccountIdColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ClientRequests
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_L1ClientRequests_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ClientAccountId') IS NOT NULL
            BEGIN
                UPDATE requests
                SET ClientAccountId = applicantParties.ClientAccountId
                FROM dbo.L1ClientRequests AS requests
                INNER JOIN dbo.L1ApplicantParties AS applicantParties
                    ON requests.ApplicantPartyId = applicantParties.Id
                WHERE requests.ClientAccountId = 0;
            END

            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_L1ClientRequests_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.L1ClientRequests'))
            BEGIN
                CREATE INDEX IX_L1ClientRequests_ClientAccountId
                    ON dbo.L1ClientRequests(ClientAccountId);
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


    private async Task EnsureL1RequestReviewsTableAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.L1RequestReviews', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.L1RequestReviews
                (
                    RequestId bigint NOT NULL,
                    ClientAccountId bigint NOT NULL,
                    Status nvarchar(50) NOT NULL,
                    StartedByEmployeeId bigint NOT NULL,
                    StartedAt datetimeoffset NOT NULL,
                    CompletedByEmployeeId bigint NULL,
                    CompletedAt datetimeoffset NULL,
                    RejectionReason nvarchar(1000) NULL,
                    CONSTRAINT PK_L1RequestReviews PRIMARY KEY (RequestId),
                    CONSTRAINT FK_L1RequestReviews_L1ClientRequests_RequestId
                        FOREIGN KEY (RequestId) REFERENCES dbo.L1ClientRequests(Id)
                        ON DELETE CASCADE
                );
            END

            IF OBJECT_ID(N'dbo.L1RequestReviews', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1RequestReviews', N'RejectionReason') IS NULL
            BEGIN
                ALTER TABLE dbo.L1RequestReviews
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


    private async Task EnsureL1AgreementProposalTablesAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.L1AgreementProposalExchanges', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.L1AgreementProposalExchanges
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
                    CONSTRAINT PK_L1AgreementProposalExchanges PRIMARY KEY (Id),
                    CONSTRAINT FK_L1AgreementProposalExchanges_L1ClientRequests_RequestId
                        FOREIGN KEY (RequestId) REFERENCES dbo.L1ClientRequests(Id)
                        ON DELETE NO ACTION
                );

                CREATE INDEX IX_L1AgreementProposalExchanges_RequestId
                    ON dbo.L1AgreementProposalExchanges(RequestId);

                CREATE INDEX IX_L1AgreementProposalExchanges_ClientAccountId
                    ON dbo.L1AgreementProposalExchanges(ClientAccountId);
            END

            IF OBJECT_ID(N'dbo.L1AgreementProposalExchanges', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1AgreementProposalExchanges', N'ClientAccountId') IS NULL
            BEGIN
                ALTER TABLE dbo.L1AgreementProposalExchanges
                ADD ClientAccountId bigint NOT NULL
                    CONSTRAINT DF_L1AgreementProposalExchanges_ClientAccountId DEFAULT(0);
            END

            IF OBJECT_ID(N'dbo.L1AgreementProposalExchanges', N'U') IS NOT NULL
               AND NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_L1AgreementProposalExchanges_ClientAccountId'
                      AND object_id = OBJECT_ID(N'dbo.L1AgreementProposalExchanges'))
            BEGIN
                CREATE INDEX IX_L1AgreementProposalExchanges_ClientAccountId
                    ON dbo.L1AgreementProposalExchanges(ClientAccountId);
            END

            IF OBJECT_ID(N'dbo.L1AgreementProposalExchanges', N'U') IS NOT NULL
               AND OBJECT_ID(N'dbo.L1AgreementProposals', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.L1AgreementProposals
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
                    CONSTRAINT PK_L1AgreementProposals PRIMARY KEY (Id),
                    CONSTRAINT FK_L1AgreementProposals_L1AgreementProposalExchanges_AgreementProposalExchangeId
                        FOREIGN KEY (AgreementProposalExchangeId) REFERENCES dbo.L1AgreementProposalExchanges(Id)
                        ON DELETE CASCADE
                );

                CREATE INDEX IX_L1AgreementProposals_AgreementProposalExchangeId
                    ON dbo.L1AgreementProposals(AgreementProposalExchangeId);
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

    private async Task EnsureL1AccountEmployeeColumnsAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1Accounts', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1Accounts', N'EmployeeFullName_FirstName') IS NULL
            BEGIN
                ALTER TABLE dbo.L1Accounts
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

    private async Task EnsureL1ApplicantPartyCurrentVersionColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ApplicantParties', N'IsCurrentActiveVersion') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ApplicantParties
                ADD IsCurrentActiveVersion bit NOT NULL
                    CONSTRAINT DF_L1ApplicantParties_IsCurrentActiveVersion DEFAULT 1;
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

    private async Task EnsureL1ApplicantPartyVerificationStatusColumnAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ApplicantParties', N'VerificationStatus') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ApplicantParties
                ADD VerificationStatus nvarchar(50) NOT NULL
                    CONSTRAINT DF_L1ApplicantParties_VerificationStatus DEFAULT N'Unverified';
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
