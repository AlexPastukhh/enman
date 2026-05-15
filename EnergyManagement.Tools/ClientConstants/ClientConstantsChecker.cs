namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsChecker
{
    public async Task<ClientConstantsCheckResult> CheckAsync(
        ClientConstantsArtifacts artifacts,
        string outputDirectory,
        CancellationToken cancellationToken = default)
    {
        var mismatchedFiles = new List<string>();

        await CheckFileAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ConstantsFileName),
            artifacts.ConstantsJson,
            mismatchedFiles,
            cancellationToken);

        await CheckFileAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ErrorCodesFileName),
            artifacts.ErrorCodesJson,
            mismatchedFiles,
            cancellationToken);

        return mismatchedFiles.Count == 0
            ? ClientConstantsCheckResult.Success()
            : ClientConstantsCheckResult.Failure(mismatchedFiles);
    }

    private static async Task CheckFileAsync(
        string path,
        string expectedContent,
        ICollection<string> mismatchedFiles,
        CancellationToken cancellationToken)
    {
        if (!File.Exists(path))
        {
            mismatchedFiles.Add(path);
            return;
        }

        var actualContent = await File.ReadAllTextAsync(path, cancellationToken);

        if (NormalizeLineEndings(actualContent) != NormalizeLineEndings(expectedContent))
        {
            mismatchedFiles.Add(path);
        }
    }

    private static string NormalizeLineEndings(string content)
        => content.Replace("\r\n", "\n", StringComparison.Ordinal);
}
