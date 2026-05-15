namespace EnergyManagement.Tools.OpenApi;

public sealed record GenerateOpenApiOptions(
    string OutputPath,
    bool Check,
    string ServerUrl,
    string SwaggerPath,
    string ProjectPath,
    int TimeoutSeconds)
{
    private const string CommandName = "generate-openapi";

    public const string DefaultServerUrl = "https://127.0.0.1:7250";
    public const string DefaultSwaggerPath = "/swagger/v1/swagger.json";
    public const string DefaultProjectPath = "EnergyManagement.Server/EnergyManagement.Server.csproj";
    public const int DefaultTimeoutSeconds = 120;

    public static bool TryParse(
        string[] args,
        out GenerateOpenApiOptions options,
        out string error)
    {
        options = new GenerateOpenApiOptions(
            string.Empty,
            false,
            DefaultServerUrl,
            DefaultSwaggerPath,
            DefaultProjectPath,
            DefaultTimeoutSeconds);
        error = string.Empty;

        if (args.Length == 0 || !string.Equals(args[0], CommandName, StringComparison.Ordinal))
        {
            error = $"Expected command '{CommandName}'.";
            return false;
        }

        string? outputPath = null;
        var check = false;
        var serverUrl = DefaultServerUrl;
        var swaggerPath = DefaultSwaggerPath;
        var projectPath = DefaultProjectPath;
        var timeoutSeconds = DefaultTimeoutSeconds;

        for (var i = 1; i < args.Length; i++)
        {
            var arg = args[i];

            if (string.Equals(arg, "--check", StringComparison.Ordinal))
            {
                check = true;
                continue;
            }

            if (RequiresValue(arg))
            {
                if (i + 1 >= args.Length || args[i + 1].StartsWith("--", StringComparison.Ordinal))
                {
                    error = $"Missing value for {arg}.";
                    return false;
                }

                var value = args[++i];
                switch (arg)
                {
                    case "--out":
                        outputPath = value;
                        break;
                    case "--server-url":
                        serverUrl = value.TrimEnd('/');
                        break;
                    case "--swagger-path":
                        swaggerPath = value.StartsWith("/", StringComparison.Ordinal)
                            ? value
                            : "/" + value;
                        break;
                    case "--project":
                        projectPath = value;
                        break;
                    case "--timeout-seconds":
                        if (!int.TryParse(value, out timeoutSeconds) || timeoutSeconds <= 0)
                        {
                            error = "--timeout-seconds must be a positive integer.";
                            return false;
                        }

                        break;
                }

                continue;
            }

            error = $"Unknown argument '{arg}'.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(outputPath))
        {
            error = "Missing required --out option.";
            return false;
        }

        options = new GenerateOpenApiOptions(
            outputPath,
            check,
            serverUrl,
            swaggerPath,
            projectPath,
            timeoutSeconds);
        return true;
    }

    public static void WriteUsage(TextWriter writer)
    {
        writer.WriteLine("Usage:");
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json");
        writer.WriteLine("  dotnet run --project EnergyManagement.Tools -- generate-openapi --out Shared/openapi.json --check");
    }

    private static bool RequiresValue(string arg)
    {
        return arg is "--out" or "--server-url" or "--swagger-path" or "--project" or "--timeout-seconds";
    }
}
