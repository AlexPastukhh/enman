using EnergyManagement.Testing.TestDatabase;

namespace EnergyManagement.Tools.OpenApi;

public sealed class GenerateOpenApiCommand
{
    private readonly OpenApiServerProcessRunner _serverProcessRunner;
    private readonly OpenApiDocumentFetcher _documentFetcher;
    private readonly OpenApiJsonFormatter _jsonFormatter;
    private readonly OpenApiArtifactWriter _writer;
    private readonly OpenApiArtifactChecker _checker;

    public GenerateOpenApiCommand(
        OpenApiServerProcessRunner serverProcessRunner,
        OpenApiDocumentFetcher documentFetcher,
        OpenApiJsonFormatter jsonFormatter,
        OpenApiArtifactWriter writer,
        OpenApiArtifactChecker checker)
    {
        _serverProcessRunner = serverProcessRunner;
        _documentFetcher = documentFetcher;
        _jsonFormatter = jsonFormatter;
        _writer = writer;
        _checker = checker;
    }

    public async Task<int> ExecuteAsync(string[] args, CancellationToken cancellationToken = default)
    {
        if (!GenerateOpenApiOptions.TryParse(args, out var options, out var error))
        {
            Console.Error.WriteLine(error);
            GenerateOpenApiOptions.WriteUsage(Console.Error);
            return 1;
        }

        await using var serverProcess = _serverProcessRunner.Start(
            options.ProjectPath,
            options.ServerUrl,
            TestDatabaseDefaults.LocalDbConnectionString);

        var rawJson = await _documentFetcher.FetchAsync(
            options.ServerUrl,
            options.SwaggerPath,
            TimeSpan.FromSeconds(options.TimeoutSeconds),
            cancellationToken);

        var formattedJson = _jsonFormatter.Format(rawJson);
        var outputPath = Path.GetFullPath(options.OutputPath);

        if (options.Check)
        {
            var result = await _checker.CheckAsync(formattedJson, outputPath, cancellationToken);
            if (result.IsSuccess)
            {
                Console.WriteLine("OpenAPI artifact is up to date.");
                return 0;
            }

            Console.Error.WriteLine("OpenAPI artifact is missing or outdated:");
            foreach (var mismatchedFile in result.MismatchedFiles)
            {
                Console.Error.WriteLine($"- {mismatchedFile}");
            }

            return 1;
        }

        await _writer.WriteAsync(formattedJson, outputPath, cancellationToken);
        Console.WriteLine($"Generated OpenAPI artifact at {outputPath}.");
        return 0;
    }
}
