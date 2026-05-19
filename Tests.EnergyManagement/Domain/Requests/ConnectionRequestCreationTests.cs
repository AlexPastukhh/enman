using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.Domain;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Requests;

public class ConnectionRequestCreationTests
{
    [Fact]
    public void Create_creates_in_review_request()
    {
        var applicant = CreatePersistedApplicant();

        var result = ConnectionRequest.Create(
            applicant,
            ValidDomainTestData.RequestDetails,
            ValidDomainTestData.Address);

        result.IsSuccess.Should().BeTrue();
        result.Value.ApplicantPartyId.Should().Be(applicant.Id);
        result.Value.RequestType.Should().Be(ClientRequestType.Connection);
        result.Value.Status.Should().Be(RequestStatus.InReview);
        result.Value.Details.Should().Be(ValidDomainTestData.RequestDetails);
        result.Value.ObjectAddress.Should().Be(ValidDomainTestData.Address);
        result.Value.Review.Should().BeNull();
    }

    [Fact]
    public void Create_fails_without_details()
    {
        var result = ConnectionRequest.Create(
            CreatePersistedApplicant(),
            " ",
            ValidDomainTestData.Address);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
    }

    [Fact]
    public void Create_fails_without_object_address()
    {
        var result = ConnectionRequest.Create(
            CreatePersistedApplicant(),
            ValidDomainTestData.RequestDetails,
            null!);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestObjectAddressIsRequired);
    }

    [Fact]
    public void Create_guards_transient_applicant_party()
    {
        var applicant = CreateApplicant();

        var result = ConnectionRequest.Create(
            applicant,
            ValidDomainTestData.RequestDetails,
            ValidDomainTestData.Address);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.ApplicantPartyMustBePersisted);
    }

    private static IndividualApplicantParty CreatePersistedApplicant()
    {
        return CreateApplicant().WithId(42);
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
