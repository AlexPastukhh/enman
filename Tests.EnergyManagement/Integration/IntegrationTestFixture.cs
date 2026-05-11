using System.Data;
using EnergyManagement.Server.L1.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Tests.EnergyManagement.TestHelpers;

namespace Tests.EnergyManagement.Integration;

public class IntegrationTestFixture : IAsyncLifetime
{
    private const string BaselineClientEmail = "fixture.client@example.com";

    public string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestEnergyManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
    public WebAppFactory Factory { get; private set; } = null!;
    public TestIndividualActor Client { get; private set; } = null!;

    public async Task DisposeAsync()
    {
       await Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        Factory = new WebAppFactory(ConnectionString);
        await ClearDatabase();
        Client = await DatabaseHelpers.CreateRegisteredIndividualAsync(
            Factory,
            BaselineClientEmail,
            ValidTestData.ValidPassword);
    }

    public async Task ClearDatabase()
    {
        await EnsureL1TablesCreatedAsync();

        string query =@"
        BEGIN TRANSACTION;
        IF OBJECT_ID(N'dbo.L1ClientRequests', N'U') IS NOT NULL DELETE FROM dbo.L1ClientRequests;
        IF OBJECT_ID(N'dbo.L1ApplicantParties', N'U') IS NOT NULL DELETE FROM dbo.L1ApplicantParties;
        IF OBJECT_ID(N'dbo.L1Accounts', N'U') IS NOT NULL DELETE FROM dbo.L1Accounts;
        DELETE FROM dbo.IndividualClients;
        DELETE FROM dbo.Clients;
        COMMIT TRANSACTION;";

        using var connection = new SqlConnection(ConnectionString);
        var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    private async Task EnsureL1TablesCreatedAsync()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(
            "SELECT CASE WHEN OBJECT_ID(N'dbo.L1Accounts', N'U') IS NULL THEN 0 ELSE 1 END",
            connection);

        var scalar = await command.ExecuteScalarAsync()
            ?? throw new InvalidOperationException("Could not check L1 table existence.");
        var l1TablesExist = (int)scalar == 1;
        if (l1TablesExist)
        {
            return;
        }

        await using var context = new L1DbContext(ConnectionString);
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();
        await databaseCreator.CreateTablesAsync();
    }
}
