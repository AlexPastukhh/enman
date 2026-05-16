using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.L1Domain;
using Tests.EnergyManagement.TestHelpers.L1;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.Requests;

public class ConnectionRequestCreationTests
{
    [Fact]
    public void Create_creates_in_review_request()
    {
        var applicant = CreatePersistedApplicant();

        var result = ConnectionRequest.Create(
            applicant,
            L1ValidTestData.RequestDetails,
            L1ValidTestData.Address);

        result.IsSuccess.Should().BeTrue();
        result.Value.ApplicantPartyId.Should().Be(applicant.Id);
        result.Value.RequestType.Should().Be(ClientRequestType.Connection);
        result.Value.Status.Should().Be(RequestStatus.InReview);
        result.Value.Details.Should().Be(L1ValidTestData.RequestDetails);
        result.Value.ObjectAddress.Should().Be(L1ValidTestData.Address);
        result.Value.ReviewDecision.Should().BeNull();
    }

    [Fact]
    public void Create_fails_without_details()
    {
        var result = ConnectionRequest.Create(
            CreatePersistedApplicant(),
            " ",
            L1ValidTestData.Address);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.ClientRequestErrors.ClientRequestTextIsRequired);
    }

    [Fact]
    public void Create_fails_without_object_address()
    {
        var result = ConnectionRequest.Create(
            CreatePersistedApplicant(),
            L1ValidTestData.RequestDetails,
            null!);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.RequestObjectAddressIsRequired);
    }

    [Fact]
    public void Create_allows_transient_applicant_party_for_atomic_create()
    {
        var applicant = CreateApplicant();

        var result = ConnectionRequest.Create(
            applicant,
            L1ValidTestData.RequestDetails,
            L1ValidTestData.Address);

        result.IsSuccess.Should().BeTrue();
        result.Value.ApplicantParty.Should().BeSameAs(applicant);
        result.Value.ApplicantPartyId.Should().Be(0);
    }

    private static IndividualApplicantParty CreatePersistedApplicant()
    {
        return CreateApplicant().WithId(42);
    }

    private static IndividualApplicantParty CreateApplicant()
    {
        return IndividualApplicantParty.Create(
            clientAccountId: 10,
            L1ValidTestData.FullName,
            L1ValidTestData.Email,
            L1ValidTestData.PhoneNumber,
            DateTimeOffset.UtcNow).Value;
    }
}
