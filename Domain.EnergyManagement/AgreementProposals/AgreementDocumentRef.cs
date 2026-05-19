using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class AgreementDocumentRef : ValueObject
{
    public string StorageKey { get; private set; }

    public string OriginalFileName { get; private set; }

    public string ContentType { get; private set; }

    public long SizeBytes { get; private set; }

    private AgreementDocumentRef(
        string storageKey,
        string originalFileName,
        string contentType,
        long sizeBytes)
    {
        StorageKey = storageKey;
        OriginalFileName = originalFileName;
        ContentType = contentType;
        SizeBytes = sizeBytes;
    }

    private AgreementDocumentRef()
    {
        StorageKey = null!;
        OriginalFileName = null!;
        ContentType = null!;
    }

    public static Result<AgreementDocumentRef, IReadOnlyList<Error>> Create(
        string storageKey,
        string originalFileName,
        string contentType,
        long sizeBytes)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(storageKey))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentStorageKeyIsRequired);
        }

        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentFileNameIsRequired);
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            errors.Add(Errors.L1Domain.AgreementDocumentContentTypeIsRequired);
        }

        if (sizeBytes <= 0)
        {
            errors.Add(Errors.L1Domain.AgreementDocumentSizeIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<AgreementDocumentRef, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<AgreementDocumentRef, IReadOnlyList<Error>>(
            new AgreementDocumentRef(
                storageKey.Trim(),
                originalFileName.Trim(),
                contentType.Trim(),
                sizeBytes));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return StorageKey;
        yield return OriginalFileName;
        yield return ContentType;
        yield return SizeBytes;
    }
}
