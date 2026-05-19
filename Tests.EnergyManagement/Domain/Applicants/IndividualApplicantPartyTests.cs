using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Applicants;

public class IndividualApplicantPartyTests
{
    [Fact]
    public void Create_creates_unverified_non_default_applicant()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = IndividualApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.FullName,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            createdAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.ClientAccountId.Should().Be(10);
        result.Value.ApplicantPartyType.Should().Be(ApplicantPartyType.Individual);
        result.Value.Type.Should().Be(ApplicantPartyType.Individual);
        result.Value.VerificationStatus.Should().Be(ApplicantPartyVerificationStatus.Unverified);
        result.Value.IsCurrentActiveVersion.Should().BeFalse();
        result.Value.Email.Should().Be(ValidDomainTestData.Email);
        result.Value.PhoneNumber.Should().Be(ValidDomainTestData.PhoneNumber);
        result.Value.FullName.Should().Be(ValidDomainTestData.FullName);
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_fails_for_invalid_applicant_data()
    {
        var result = IndividualApplicantParty.Create(
            clientAccountId: 0,
            null!,
            null!,
            null!,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ClientAccountIsRequired);
        result.Error.Should().Contain(Errors.Account.FirstNameIsRequired);
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

    [Fact]
    public void MarkInactiveVersion_marks_current_flag_false()
    {
        var applicant = CreateApplicant();
        applicant.MarkAsCurrentDefaultTemplate();

        var result = applicant.MarkInactiveVersion();

        result.IsSuccess.Should().BeTrue();
        applicant.IsCurrentActiveVersion.Should().BeFalse();
    }

    [Fact]
    public void MarkAsCurrentDefaultTemplate_marks_current_flag_true()
    {
        var applicant = CreateApplicant();

        var result = applicant.MarkAsCurrentDefaultTemplate();

        result.IsSuccess.Should().BeTrue();
        applicant.IsCurrentActiveVersion.Should().BeTrue();
    }

    private static IndividualApplicantParty CreateApplicant()
    {
        return IndividualApplicantParty.Create(
            clientAccountId: 10,
            ValidDomainTestData.FullName,
            ValidDomainTestData.Email,
            ValidDomainTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value;
    }
}
