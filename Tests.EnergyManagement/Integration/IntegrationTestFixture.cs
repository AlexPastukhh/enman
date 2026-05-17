using EnergyManagement.Testing.TestDatabase;

namespace Tests.EnergyManagement.Integration;

public class IntegrationTestFixture : IAsyncLifetime
{
    public string ConnectionString => TestDatabaseDefaults.LocalDbConnectionString;
    public WebAppFactory Factory { get; private set; } = null!;

    private TestDatabaseManager DatabaseManager => new(ConnectionString);

    public async Task DisposeAsync()
    {
        Factory.Dispose();
        await Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        Factory = new WebAppFactory(ConnectionString);
        await DatabaseManager.ResetAsync();
    }
}
