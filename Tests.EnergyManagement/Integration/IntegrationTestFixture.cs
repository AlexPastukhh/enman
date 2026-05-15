using EnergyManagement.Testing.TestDatabase;
using Tests.EnergyManagement.TestHelpers;

namespace Tests.EnergyManagement.Integration;

public class IntegrationTestFixture : IAsyncLifetime
{
    private const string BaselineClientEmail = "fixture.client@example.com";

    public string ConnectionString => TestDatabaseDefaults.LocalDbConnectionString;
    public WebAppFactory Factory { get; private set; } = null!;
    public TestIndividualActor Client { get; private set; } = null!;

    private TestDatabaseManager DatabaseManager => new(ConnectionString);

    public async Task DisposeAsync()
    {
       await Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        Factory = new WebAppFactory(ConnectionString);
        await DatabaseManager.ResetAsync();
        Client = await DatabaseHelpers.CreateRegisteredIndividualAsync(
            Factory,
            BaselineClientEmail,
            ValidTestData.ValidPassword);
    }
}
