using Domain.EnergyManagement.Common;
using EnergyManagement.Server.Api;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class UploadAgreementProposalDocumentFormValidator
    : AbstractValidator<UploadAgreementProposalDocumentForm>
{
    public const long MaxAgreementProposalDocumentBytes = 10 * 1024 * 1024;
    private static readonly string[] AllowedContentTypes =
    [
        "application/pdf",
        "application/octet-stream",
        string.Empty
    ];

    public UploadAgreementProposalDocumentFormValidator()
    {
        RuleFor(x => x.Document)
            .NotNull()
            .OverridePropertyName(FieldNames.AgreementProposalDocumentUpload.Document)
            .WithMessage(Error.Errors.General.ValueIsRequired.Code);

        When(x => x.Document is not null, () =>
        {
            RuleFor(x => x.Document!.FileName)
                .NotEmpty()
                .OverridePropertyName(FieldNames.AgreementProposalDocumentUpload.Document)
                .WithMessage(Error.Errors.General.ValueIsRequired.Code);

            RuleFor(x => x.Document!.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxAgreementProposalDocumentBytes)
                .OverridePropertyName(FieldNames.AgreementProposalDocumentUpload.DocumentSizeBytes)
                .WithMessage(Error.Errors.General.ValueIsInvalid.Code);

            RuleFor(x => x.Document)
                .Must(document => IsPdfDocument(document!.FileName, document.ContentType))
                .OverridePropertyName(FieldNames.AgreementProposalDocumentUpload.DocumentContentType)
                .WithMessage(Error.Errors.General.ValueIsInvalid.Code);
        });
    }

    private static bool IsPdfDocument(string fileName, string contentType)
    {
        var hasPdfExtension = Path.GetExtension(fileName).Equals(
            ".pdf",
            StringComparison.OrdinalIgnoreCase);

        var hasAllowedContentType = AllowedContentTypes.Contains(
            contentType?.Trim() ?? string.Empty,
            StringComparer.OrdinalIgnoreCase);

        return hasPdfExtension && hasAllowedContentType;
    }
}
