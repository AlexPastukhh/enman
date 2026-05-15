namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiCheckResult
{
    private OpenApiCheckResult(bool isSuccess, IReadOnlyList<string> mismatchedFiles)
    {
        IsSuccess = isSuccess;
        MismatchedFiles = mismatchedFiles;
    }

    public bool IsSuccess { get; }
    public IReadOnlyList<string> MismatchedFiles { get; }

    public static OpenApiCheckResult Success()
    {
        return new OpenApiCheckResult(true, Array.Empty<string>());
    }

    public static OpenApiCheckResult Failure(IReadOnlyList<string> mismatchedFiles)
    {
        return new OpenApiCheckResult(false, mismatchedFiles);
    }
}
