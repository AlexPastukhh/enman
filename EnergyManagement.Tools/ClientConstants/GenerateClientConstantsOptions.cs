namespace EnergyManagement.Tools.ClientConstants;

public sealed record GenerateClientConstantsOptions(
    string OutputDirectory,
    bool Check)
{
    private const string CommandName = "generate-client-constants";

    public static bool TryParse(
        string[] args,
        out GenerateClientConstantsOptions options,
        out string error)
    {
        options = new GenerateClientConstantsOptions(string.Empty, false);
        error = string.Empty;

        if (args.Length == 0 || !string.Equals(args[0], CommandName, StringComparison.Ordinal))
        {
            error = $"Expected command '{CommandName}'.";
            return false;
        }

        string? outputDirectory = null;
        var check = false;

        for (var i = 1; i < args.Length; i++)
        {
            var arg = args[i];

            if (string.Equals(arg, "--out", StringComparison.Ordinal))
            {
                if (i + 1 >= args.Length || args[i + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    error = "Missing value for --out.";
                    return false;
                }

                outputDirectory = args[++i];
                continue;
            }

            if (string.Equals(arg, "--check", StringComparison.Ordinal))
            {
                check = true;
                continue;
            }

            error = $"Unknown argument '{arg}'.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(outputDirectory))
        {
            error = "Missing required --out option.";
            return false;
        }

        options = new GenerateClientConstantsOptions(outputDirectory, check);
        return true;
    }

    public static void WriteUsage(TextWriter writer)
    {
        writer.WriteLine("Usage:");
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared");
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- generate-client-constants --out Shared --check");
    }
}
