namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiArtifactWriter
{
    public async Task WriteAsync(
        string formattedJson,
        string outputPath,
        CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(outputPath, formattedJson, cancellationToken);
    }
}
