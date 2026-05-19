using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using EnergyManagement.Server.Api;
using FluentValidation;

namespace EnergyManagement.Server.Api.Validation;

public sealed class SendAgreementProposalVersionDtoValidator
    : AbstractValidator<SendAgreementProposalVersionDto>
{
    public SendAgreementProposalVersionDtoValidator()
    {
        RuleFor(x => x.Document)
            .NotNull()
            .OverridePropertyName(FieldNames.AgreementProposalVersion.Document)
            .WithMessage(Error.Errors.General.ValueIsRequired.Code);

        When(x => x.Document is not null, () =>
        {
            RuleFor(x => x.Document!.StorageKey)
                .NotEmpty()
                .OverridePropertyName(FieldNames.AgreementProposalVersion.DocumentStorageKey)
                .WithMessage(Error.Errors.General.ValueIsRequired.Code);

            RuleFor(x => x.Document!.OriginalFileName)
                .NotEmpty()
                .OverridePropertyName(FieldNames.AgreementProposalVersion.DocumentOriginalFileName)
                .WithMessage(Error.Errors.General.ValueIsRequired.Code);

            RuleFor(x => x.Document!.ContentType)
                .NotEmpty()
                .OverridePropertyName(FieldNames.AgreementProposalVersion.DocumentContentType)
                .WithMessage(Error.Errors.General.ValueIsRequired.Code);

            RuleFor(x => x.Document!.SizeBytes)
                .GreaterThan(0)
                .OverridePropertyName(FieldNames.AgreementProposalVersion.DocumentSizeBytes)
                .WithMessage(Error.Errors.General.ValueIsInvalid.Code);
        });

        RuleFor(x => x.Comment)
            .MaximumLength(ProposalComment.MaxLength)
            .OverridePropertyName(FieldNames.AgreementProposalVersion.Comment)
            .WithMessage(Error.Errors.L1Domain.ProposalCommentIsTooLong.Code)
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));
    }
}
