using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class LegalEntityApplicantPartyTests
{
    [Fact]
    public void Create_creates_unverified_legal_entity_applicant()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = LegalEntityApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.OrganizationName,
            ValidDomainTestData.LegalEntityInn,
            ValidDomainTestData.Kpp,
            ValidDomainTestData.Ogrn,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            createdAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.ClientAccountId.Should().Be(10);
        result.Value.ApplicantPartyType.Should().Be(ApplicantPartyType.LegalEntity);
        result.Value.Type.Should().Be(ApplicantPartyType.LegalEntity);
        result.Value.VerificationStatus.Should().Be(ApplicantPartyVerificationStatus.Unverified);
        result.Value.IsCurrentActiveVersion.Should().BeFalse();
        result.Value.OrganizationName.Should().Be(ValidDomainTestData.OrganizationName);
        result.Value.Inn.Should().Be(ValidDomainTestData.LegalEntityInn);
        result.Value.Kpp.Should().Be(ValidDomainTestData.Kpp);
        result.Value.Ogrn.Should().Be(ValidDomainTestData.Ogrn);
        result.Value.Email.Should().Be(ValidDomainTestData.Email);
        result.Value.PhoneNumber.Should().Be(ValidDomainTestData.PhoneNumber);
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void GetDisplayName_returns_organization_name()
    {
        var applicant = CreateApplicant();

        applicant.GetDisplayName().Should().Be("Test Organization");
    }

    [Fact]
    public void Create_fails_for_invalid_applicant_data()
    {
        var result = LegalEntityApplicantParty.Create(
            clientAccountId: 0,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientAccountIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.OrganizationNameIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.InnIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.KppIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.OgrnIsRequired);
        result.Error.Should().Contain(Errors.Account.EmailIsRequired);
        result.Error.Should().Contain(Errors.Account.PhoneNumberIsRequired);
    }

    [Fact]
    public void MarkVerified_succeeds_when_minimum_data_present()
    {
        var applicant = CreateApplicant();

        var result = applicant.MarkVerified();

        result.IsSuccess.Should().BeTrue();
        applicant.VerificationStatus.Should().Be(ApplicantPartyVerificationStatus.Verified);
    }

    private static LegalEntityApplicantParty CreateApplicant()
    {
        return LegalEntityApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.OrganizationName,
            ValidDomainTestData.LegalEntityInn,
            ValidDomainTestData.Kpp,
            ValidDomainTestData.Ogrn,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value;
    }
}
