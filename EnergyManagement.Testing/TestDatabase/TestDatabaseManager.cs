using System.Data;
using EnergyManagement.Server;
using EnergyManagement.Server.L1.Persistence;
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
        await EnsureAppTablesCreatedAsync(cancellationToken);
        await EnsureL1TablesCreatedAsync(cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        const string query = """
            BEGIN TRANSACTION;
            IF OBJECT_ID(N'dbo.L1RequestReviews', N'U') IS NOT NULL DELETE FROM dbo.L1RequestReviews;
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL DELETE FROM dbo.L1ClientRequests;
            IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL DELETE FROM dbo.L1ApplicantParties;
            IF OBJECT_ID(N'dbo.L1Employees', N'U') IS NOT NULL DELETE FROM dbo.L1Employees;
            IF OBJECT_ID(N'dbo.L1Accounts', N'U') IS NOT NULL DELETE FROM dbo.L1Accounts;
            IF OBJECT_ID(N'dbo.RequestReviews', N'U') IS NOT NULL DELETE FROM dbo.RequestReviews;
            IF OBJECT_ID(N'dbo.Requests', N'U') IS NOT NULL DELETE FROM dbo.Requests;
            IF OBJECT_ID(N'dbo.IndividualClients', N'U') IS NOT NULL DELETE FROM dbo.IndividualClients;
            IF OBJECT_ID(N'dbo.Clients', N'U') IS NOT NULL DELETE FROM dbo.Clients;
            IF OBJECT_ID(N'dbo.Managers', N'U') IS NOT NULL DELETE FROM dbo.Managers;
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

    private async Task EnsureAppTablesCreatedAsync(CancellationToken cancellationToken)
    {
        var clientsExists = await TableExistsAsync("Clients", cancellationToken);
        var individualClientsExists = await TableExistsAsync("IndividualClients", cancellationToken);

        if (clientsExists && individualClientsExists)
        {
            return;
        }

        if (clientsExists || individualClientsExists)
        {
            throw new InvalidOperationException(
                "The AppDbContext test schema is partial. Expected both Clients and IndividualClients to exist before reset.");
        }

        await using var context = new AppDbContext(_connectionString);
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
        await databaseCreator.CreateTablesAsync(cancellationToken);
    }

    private async Task EnsureL1TablesCreatedAsync(CancellationToken cancellationToken)
    {
        if (await TableExistsAsync("L1Accounts", cancellationToken))
        {
            await EnsureL1ApplicantPartyCurrentVersionColumnAsync(cancellationToken);
            await EnsureL1ApplicantPartyVerificationStatusColumnAsync(cancellationToken);
            await EnsureL1ClientRequestReviewColumnsAsync(cancellationToken);
            await EnsureL1RequestReviewsTableAsync(cancellationToken);
            await EnsureL1EmployeesTableAsync(cancellationToken);
            return;
        }

        await using var context = new L1DbContext(_connectionString);
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
        await databaseCreator.CreateTablesAsync(cancellationToken);
        await EnsureL1ClientRequestReviewColumnsAsync(cancellationToken);
        await EnsureL1RequestReviewsTableAsync(cancellationToken);
        await EnsureL1EmployeesTableAsync(cancellationToken);
    }

    private async Task EnsureL1ClientRequestReviewColumnsAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ReviewDecision') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ClientRequests
                ADD ReviewDecision nvarchar(50) NULL;
            END

            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ReviewDecidedAt') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ClientRequests
                ADD ReviewDecidedAt datetimeoffset NULL;
            END

            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ReviewReviewerId') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ClientRequests
                ADD ReviewReviewerId bigint NULL;
            END

            IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL
               AND COL_LENGTH(N'dbo.L1ClientRequests', N'ReviewRejectionReason') IS NULL
            BEGIN
                ALTER TABLE dbo.L1ClientRequests
                ADD ReviewRejectionReason nvarchar(1000) NULL;
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


    private async Task EnsureL1EmployeesTableAsync(CancellationToken cancellationToken)
    {
        const string query = """
            IF OBJECT_ID(N'dbo.L1Employees', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.L1Employees
                (
                    Id bigint IDENTITY(1,1) NOT NULL,
                    AccountId bigint NOT NULL,
                    FullName_FirstName nvarchar(100) NOT NULL,
                    FullName_MiddleName nvarchar(100) NOT NULL,
                    FullName_LastName nvarchar(100) NOT NULL,
                    IsActive bit NOT NULL,
                    CreatedAt datetimeoffset NOT NULL,
                    CONSTRAINT PK_L1Employees PRIMARY KEY (Id)
                );
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
