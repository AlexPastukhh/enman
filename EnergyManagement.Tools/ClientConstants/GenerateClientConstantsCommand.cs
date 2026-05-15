namespace EnergyManagement.Tools.ClientConstants;

public sealed class GenerateClientConstantsCommand
{
    private readonly ClientConstantsPathResolver _pathResolver;
    private readonly ClientConstantsSnapshotFactory _snapshotFactory;
    private readonly ClientConstantsJsonSerializer _serializer;
    private readonly ClientConstantsWriter _writer;
    private readonly ClientConstantsChecker _checker;

    public GenerateClientConstantsCommand(
        ClientConstantsPathResolver pathResolver,
        ClientConstantsSnapshotFactory snapshotFactory,
        ClientConstantsJsonSerializer serializer,
        ClientConstantsWriter writer,
        ClientConstantsChecker checker)
    {
        _pathResolver = pathResolver;
        _snapshotFactory = snapshotFactory;
        _serializer = serializer;
        _writer = writer;
        _checker = checker;
    }

    public async Task<int> ExecuteAsync(string[] args, CancellationToken cancellationToken = default)
    {
        if (!GenerateClientConstantsOptions.TryParse(args, out var options, out var error))
        {
            Console.Error.WriteLine(error);
            GenerateClientConstantsOptions.WriteUsage(Console.Error);
            return 1;
        }

        var outputDirectory = _pathResolver.ResolveOutputDirectory(options.OutputDirectory);
        var artifacts = _serializer.Serialize(_snapshotFactory.Create());

        if (options.Check)
        {
            var result = await _checker.CheckAsync(artifacts, outputDirectory, cancellationToken);

            if (result.IsSuccess)
            {
                Console.WriteLine("Client constants artifacts are up to date.");
                return 0;
            }

            Console.Error.WriteLine("Client constants artifacts are missing or outdated:");
            foreach (var mismatchedFile in result.MismatchedFiles)
            {
                Console.Error.WriteLine($"- {mismatchedFile}");
            }

            return 1;
        }

        await _writer.WriteAsync(artifacts, outputDirectory, cancellationToken);
        Console.WriteLine($"Generated client constants artifacts in {outputDirectory}.");
        return 0;
    }
}
