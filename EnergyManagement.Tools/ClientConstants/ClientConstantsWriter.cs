namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsWriter
{
    public async Task WriteAsync(
        ClientConstantsArtifacts artifacts,
        string outputDirectory,
        CancellationToken cancellationToken = default)
    {
        if (File.Exists(outputDirectory))
        {
            throw new IOException($"Output path is a file: {outputDirectory}");
        }

        Directory.CreateDirectory(outputDirectory);

        await File.WriteAllTextAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ConstantsFileName),
            artifacts.ConstantsJson,
            cancellationToken);

        await File.WriteAllTextAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ErrorCodesFileName),
            artifacts.ErrorCodesJson,
            cancellationToken);
    }
}
