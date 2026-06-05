using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class IndividualEntrepreneurApplicantPartyTests
{
    [Fact]
    public void Create_creates_unverified_individual_entrepreneur_applicant()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = IndividualEntrepreneurApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.FullName,
            ValidDomainTestData.IndividualEntrepreneurInn,
            ValidDomainTestData.Ogrnip,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            createdAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.ClientAccountId.Should().Be(10);
        result.Value.ApplicantPartyType.Should().Be(ApplicantPartyType.IndividualEntrepreneur);
        result.Value.Type.Should().Be(ApplicantPartyType.IndividualEntrepreneur);
        result.Value.VerificationStatus.Should().Be(ApplicantPartyVerificationStatus.Unverified);
        result.Value.IsCurrentActiveVersion.Should().BeFalse();
        result.Value.FullName.Should().Be(ValidDomainTestData.FullName);
        result.Value.Inn.Should().Be(ValidDomainTestData.IndividualEntrepreneurInn);
        result.Value.Ogrnip.Should().Be(ValidDomainTestData.Ogrnip);
        result.Value.Email.Should().Be(ValidDomainTestData.Email);
        result.Value.PhoneNumber.Should().Be(ValidDomainTestData.PhoneNumber);
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void GetDisplayName_returns_ip_prefixed_full_name()
    {
        var applicant = CreateApplicant();

        applicant.GetDisplayName().Should().Be("IP Doe John Michael");
    }

    [Fact]
    public void Create_fails_for_invalid_applicant_data()
    {
        var result = IndividualEntrepreneurApplicantParty.Create(
            clientAccountId: 0,
            null!,
            null!,
            null!,
            null!,
            null!,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientAccountIsRequired);
        result.Error.Should().Contain(Errors.Account.FirstNameIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.InnIsRequired);
        result.Error.Should().Contain(Errors.L1Domain.OgrnipIsRequired);
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

    private static IndividualEntrepreneurApplicantParty CreateApplicant()
    {
        return IndividualEntrepreneurApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.FullName,
            ValidDomainTestData.IndividualEntrepreneurInn,
            ValidDomainTestData.Ogrnip,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value;
    }
}
