namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiArtifactChecker
{
    public async Task<OpenApiCheckResult> CheckAsync(
        string expectedJson,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        if (!File.Exists(outputPath))
        {
            return OpenApiCheckResult.Failure(new[] { outputPath });
        }

        var actualJson = await File.ReadAllTextAsync(outputPath, cancellationToken);
        return NormalizeLineEndings(actualJson) == NormalizeLineEndings(expectedJson)
            ? OpenApiCheckResult.Success()
            : OpenApiCheckResult.Failure(new[] { outputPath });
    }

    private static string NormalizeLineEndings(string content)
        => content.Replace("\r\n", "\n", StringComparison.Ordinal);
}
