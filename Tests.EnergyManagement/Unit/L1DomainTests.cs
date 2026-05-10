using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.L1;

namespace Tests.EnergyManagement.Unit;

public class L1DomainTests
{
    private static ClientAccount CreateClientAccount()
    {
        return ClientAccount.Create(
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash).Value;
    }

    [Fact]
    public void CreatesClientAccountSuccessfully()
    {
        var email = L1ValidTestData.Email;
        var passwordHash = L1ValidTestData.PasswordHash;

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
        var email = L1ValidTestData.Email;
        var passwordHash = L1ValidTestData.PasswordHash;
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

        Func<Result<IndividualApplicantParty, IReadOnlyList<Error>>> createApplicantParty =
            () => IndividualApplicantParty.Create(
                account,
                L1ValidTestData.FullName,
                L1ValidTestData.Email,
                L1ValidTestData.PhoneNumber);

        createApplicantParty.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CantCreateConnectionRequestWithoutApplicantParty()
    {
        Func<Result<ConnectionRequest, IReadOnlyList<Error>>> createWithoutApplicantParty =
            () => ConnectionRequest.Create(
                null!,
                L1ValidTestData.RequestDetails,
                L1ValidTestData.Address);

        createWithoutApplicantParty.Should().Throw<Exception>();
    }
}
