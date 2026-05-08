namespace Tests.EnergyManagement.TestHelpers;

public static class TestDatabaseConnection
{
    private const string DefaultConnectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestEnergyManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public static string ConnectionString =>
        Environment.GetEnvironmentVariable("ConnectionStrings__Test")
        ?? DefaultConnectionString;
}
