using Domain.EnergyManagement;
using FluentAssertions;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.AgreementProposals;

public class AgreementDocumentRefTests
{
    [Fact]
    public void Create_succeeds_with_valid_data()
    {
        var result = AgreementDocumentRef.Create(
            "agreement/file.pdf",
            "agreement.pdf",
            "application/pdf",
            100);

        result.IsSuccess.Should().BeTrue();
        result.Value.StorageKey.Should().Be("agreement/file.pdf");
        result.Value.OriginalFileName.Should().Be("agreement.pdf");
        result.Value.ContentType.Should().Be("application/pdf");
        result.Value.SizeBytes.Should().Be(100);
    }

    [Fact]
    public void Create_trims_string_values()
    {
        var result = AgreementDocumentRef.Create(
            " agreement/file.pdf ",
            " agreement.pdf ",
            " application/pdf ",
            100);

        result.Value.StorageKey.Should().Be("agreement/file.pdf");
        result.Value.OriginalFileName.Should().Be("agreement.pdf");
        result.Value.ContentType.Should().Be("application/pdf");
    }

    [Fact]
    public void Create_fails_when_required_fields_are_blank()
    {
        var result = AgreementDocumentRef.Create(" ", " ", " ", 100);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentStorageKeyIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentFileNameIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentContentTypeIsRequired);
    }

    [Fact]
    public void Create_fails_when_size_is_not_positive()
    {
        var result = AgreementDocumentRef.Create(
            "agreement/file.pdf",
            "agreement.pdf",
            "application/pdf",
            0);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AgreementDocumentSizeIsRequired);
    }
}
