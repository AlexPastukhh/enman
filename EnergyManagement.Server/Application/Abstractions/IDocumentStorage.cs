namespace EnergyManagement.Server.Application.Abstractions;

public interface IDocumentStorage
{
    Task<StoredDocumentFile> SaveAsync(
        Stream content,
        string originalFileName,
        string contentType,
        long sizeBytes,
        CancellationToken cancellationToken);

    Task<Stream> OpenReadAsync(
        string storageKey,
        CancellationToken cancellationToken);
}

public sealed record StoredDocumentFile(
    string StorageKey,
    string OriginalFileName,
    string ContentType,
    long SizeBytes);
