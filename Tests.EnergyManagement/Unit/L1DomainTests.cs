using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;
using static Domain.EnergyManagement.Common.Error.Errors.ClientRequestErrors;

namespace Tests.EnergyManagement.Unit;

public class L1DomainTests : ClientRequestUnitBase
{
    private static ClientAccount CreateClientAccount()
    {
        var email = Email.Create(ValidTestData.ValidEmail).Value;
        var password = Password.Create(ValidTestData.ValidPassword).Value;
        return ClientAccount.Create(email, password).Value;
    }

    private static IndividualApplicantParty CreateIndividualApplicantParty()
    {
        var account = CreateClientAccount();
        var (fullName, email, phone, _) = ValidTestData.GetAllIndividualsValues();
        var applicantParty = IndividualApplicantParty.Create(
            account,
            fullName,
            email,
            phone).Value;

        account.AddApplicantPartyOrThrow(applicantParty);
        return applicantParty;
    }

    [Fact]
    public void CreatesClientAccountSuccessfully()
    {
        var (_, email, _, password) = ValidTestData.GetAllIndividualsValues();

        var createAccount = ClientAccount.Create(email, password);

        createAccount.IsSuccess.Should().BeTrue();
        var account = createAccount.Value;
        account.Email.Should().Be(email);
        account.Password.Should().Be(password);
        account.Role.Should().Be(AccountRole.Client);
        account.IsActive.Should().BeTrue();
        account.ApplicantParties.Should().BeEmpty();
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void CantCreateClientAccountWithNullAuthData(
        bool isEmailNull,
        bool isPasswordNull)
    {
        var (_, email, _, password) = ValidTestData.GetAllIndividualsValues();
        if (isEmailNull)
        {
            email = null!;
        }

        if (isPasswordNull)
        {
            password = null!;
        }

        Func<Result<ClientAccount, IReadOnlyList<Error>>> createAccount =
            () => ClientAccount.Create(email, password);

        createAccount.Should().Throw<Exception>();
    }

    [Fact]
    public void CreatesIndividualApplicantPartyAndAddsItToClientAccount()
    {
        var account = CreateClientAccount();
        var (fullName, email, phone, _) = ValidTestData.GetAllIndividualsValues();

        var createApplicantParty = IndividualApplicantParty.Create(
            account,
            fullName,
            email,
            phone);
        var applicantParty = createApplicantParty.Value;
        account.AddApplicantPartyOrThrow(applicantParty);

        createApplicantParty.IsSuccess.Should().BeTrue();
        applicantParty.ClientAccount.Should().Be(account);
        applicantParty.ApplicantPartyType.Should().Be(ApplicantPartyType.Individual);
        applicantParty.FullName.Should().Be(fullName);
        applicantParty.Email.Should().Be(email);
        applicantParty.PhoneNumber.Should().Be(phone);
        applicantParty.GetDisplayName().Should().Be(
            $"{fullName.LastName} {fullName.FirstName} {fullName.MiddleName}");
        account.ApplicantParties.Should().ContainSingle()
            .Which.Should().BeSameAs(applicantParty);
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public void CreatesConnectionRequestSuccessfully(
        string requestDetails,
        Address address)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            address);

        createRequest.IsSuccess.Should().BeTrue();
        var request = createRequest.Value;
        request.Should().BeOfType<ConnectionRequest>();
        request.ApplicantParty.Should().Be(applicantParty);
        request.RequestType.Should().Be(ClientRequestType.Connection);
        request.Status.Should().Be(RequestStatus.Submitted);
        request.Details.Should().Be(requestDetails);
        request.ObjectAddress.Should().Be(address);
    }

    [Theory]
    [StringTestData(3001, 3500)]
    public void CantCreateConnectionRequestWithTooLongRequestDetails(
        string requestDetails)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            ValidTestData.GetAddressWithApartment());

        createRequest.IsFailure.Should().BeTrue();
        createRequest.Error.Should().Contain(ClientRequestTextIsTooLong);
    }

    [Theory]
    [StringTestData(0)]
    public void CantCreateConnectionRequestWithoutRequestDetails(
        string requestDetails)
    {
        var applicantParty = CreateIndividualApplicantParty();

        var createRequest = ConnectionRequest.Create(
            applicantParty,
            requestDetails,
            ValidTestData.GetAddressWithApartment());

        createRequest.IsFailure.Should().BeTrue();
        createRequest.Error.Should().Contain(ClientRequestTextIsRequired);
    }
}
