using System.Data;
using Microsoft.Data.SqlClient;
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
        string query =@"
        BEGIN TRANSACTION;
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
}
