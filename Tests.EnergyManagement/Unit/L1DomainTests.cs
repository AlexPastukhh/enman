using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers;

namespace Tests.EnergyManagement.Unit;

public class L1DomainTests
{
    private static ClientAccount CreateClientAccount()
    {
        var email = Email.Create(ValidTestData.ValidEmail).Value;
        var passwordHash = CreatePasswordHash();

        return ClientAccount.Create(email, passwordHash).Value;
    }

    private static PasswordHash CreatePasswordHash()
    {
        var password = Password.Create(ValidTestData.ValidPassword).Value;
        return PasswordHash.ConvertFromString(password.Hash);
    }

    [Fact]
    public void CreatesClientAccountSuccessfully()
    {
        var (_, email, _, _) = ValidTestData.GetAllIndividualsValues();
        var passwordHash = CreatePasswordHash();

        var createAccount = ClientAccount.Create(email, passwordHash);

        createAccount.IsSuccess.Should().BeTrue();
        var account = createAccount.Value;
        account.Email.Should().Be(email);
        account.PasswordHash.Should().Be(passwordHash);
        account.Role.Should().Be(AccountRole.Client);
        account.IsActive.Should().BeTrue();
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void CantCreateClientAccountWithNullAuthData(
        bool isEmailNull,
        bool isPasswordNull)
    {
        var (_, email, _, _) = ValidTestData.GetAllIndividualsValues();
        var passwordHash = CreatePasswordHash();
        if (isEmailNull)
        {
            email = null!;
        }

        if (isPasswordNull)
        {
            passwordHash = null!;
        }

        Func<Result<ClientAccount, IReadOnlyList<Error>>> createAccount =
            () => ClientAccount.Create(email, passwordHash);

        createAccount.Should().Throw<Exception>();
    }

    [Fact]
    public void CantCreateIndividualApplicantPartyForTransientClientAccount()
    {
        var account = CreateClientAccount();
        var (fullName, email, phone, _) = ValidTestData.GetAllIndividualsValues();

        Func<Result<IndividualApplicantParty, IReadOnlyList<Error>>> createApplicantParty =
            () => IndividualApplicantParty.Create(
                account,
                fullName,
                email,
                phone);

        createApplicantParty.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CantCreateConnectionRequestWithoutApplicantParty()
    {
        Func<Result<ConnectionRequest, IReadOnlyList<Error>>> createWithoutApplicantParty =
            () => ConnectionRequest.Create(
                null!,
                ValidTestData.RequestDetails,
                ValidTestData.GetAddressWithApartment());

        createWithoutApplicantParty.Should().Throw<Exception>();
    }
}
