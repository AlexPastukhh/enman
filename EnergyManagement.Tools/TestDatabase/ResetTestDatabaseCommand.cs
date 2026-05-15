using EnergyManagement.Testing.TestDatabase;
using Microsoft.Data.SqlClient;

namespace EnergyManagement.Tools.TestDatabase;

public sealed class ResetTestDatabaseCommand
{
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
            Console.WriteLine($"Resetting test database {DescribeTarget(connectionString)}.");
            var manager = new TestDatabaseManager(connectionString);
            await manager.ResetAsync(cancellationToken);
            Console.WriteLine("Test database reset completed.");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("Test database reset failed:");
            Console.Error.WriteLine(exception.Message);
            return 1;
        }
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
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- reset-test-db [--connection <connection string>]");
    }

    private static string DescribeTarget(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        return $"Data Source='{builder.DataSource}', Initial Catalog='{builder.InitialCatalog}'";
    }
}
