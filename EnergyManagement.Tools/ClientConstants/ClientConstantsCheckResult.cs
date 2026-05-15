namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsCheckResult
{
    public ClientConstantsCheckResult(IReadOnlyList<string> mismatchedFiles)
    {
        MismatchedFiles = mismatchedFiles;
    }

    public IReadOnlyList<string> MismatchedFiles { get; }

    public bool IsSuccess => MismatchedFiles.Count == 0;

    public static ClientConstantsCheckResult Success()
        => new(Array.Empty<string>());

    public static ClientConstantsCheckResult Failure(IReadOnlyList<string> mismatchedFiles)
        => new(mismatchedFiles);
}
