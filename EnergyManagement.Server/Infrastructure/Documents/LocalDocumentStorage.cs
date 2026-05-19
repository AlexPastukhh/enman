using EnergyManagement.Server.Application.Abstractions;

namespace EnergyManagement.Server.Infrastructure.Documents;

public sealed class LocalDocumentStorage : IDocumentStorage
{
    private const string AgreementProposalFolder = "agreement-proposals";

    private readonly string _rootPath;

    public LocalDocumentStorage(IWebHostEnvironment environment)
    {
        _rootPath = Path.Combine(
            environment.ContentRootPath,
            "App_Data",
            "Documents");

        Directory.CreateDirectory(_rootPath);
    }

    public async Task<StoredDocumentFile> SaveAsync(
        Stream content,
        string originalFileName,
        string contentType,
        long sizeBytes,
        CancellationToken cancellationToken)
    {
        var safeOriginalFileName = Path.GetFileName(originalFileName);
        var extension = Path.GetExtension(safeOriginalFileName);
        var storedFileName = $"{Guid.NewGuid():N}{extension}";
        var storageKey = Path.Combine(AgreementProposalFolder, storedFileName)
            .Replace("\\", "/");
        var absolutePath = GetSafeAbsolutePath(storageKey);

        Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

        await using var output = new FileStream(
            absolutePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None);

        await content.CopyToAsync(output, cancellationToken);

        return new StoredDocumentFile(
            storageKey,
            safeOriginalFileName,
            contentType,
            sizeBytes);
    }

    public Task<Stream> OpenReadAsync(
        string storageKey,
        CancellationToken cancellationToken)
    {
        _ = cancellationToken;

        var absolutePath = GetSafeAbsolutePath(storageKey);
        Stream stream = new FileStream(
            absolutePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);

        return Task.FromResult(stream);
    }

    private string GetSafeAbsolutePath(string storageKey)
    {
        var absolutePath = Path.GetFullPath(Path.Combine(_rootPath, storageKey));
        var rootPath = Path.GetFullPath(_rootPath);
        var rootPrefix = rootPath.EndsWith(Path.DirectorySeparatorChar)
            ? rootPath
            : rootPath + Path.DirectorySeparatorChar;

        if (!absolutePath.StartsWith(rootPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Invalid document storage key.");
        }

        return absolutePath;
    }
}
