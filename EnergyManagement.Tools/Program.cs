using EnergyManagement.Tools.ClientConstants;
using EnergyManagement.Tools.TestDatabase;

if (args.Length == 0)
{
    Console.Error.WriteLine("Missing command.");
    WriteUsage(Console.Error);
    return 1;
}

return args[0] switch
{
    "generate-client-constants" => await new GenerateClientConstantsCommand(
        new ClientConstantsPathResolver(),
        new ClientConstantsSnapshotFactory(),
        new ClientConstantsJsonSerializer(),
        new ClientConstantsWriter(),
        new ClientConstantsChecker()).ExecuteAsync(args),
    "reset-test-db" => await new ResetTestDatabaseCommand().ExecuteAsync(args),
    _ => UnknownCommand(args[0])
};

static int UnknownCommand(string command)
{
    Console.Error.WriteLine($"Unknown command: {command}");
    WriteUsage(Console.Error);
    return 1;
}

static void WriteUsage(TextWriter writer)
{
    writer.WriteLine("Usage:");
    writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- generate-client-constants --out <directory> [--check]");
    writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- reset-test-db [--connection <connection string>]");
}
