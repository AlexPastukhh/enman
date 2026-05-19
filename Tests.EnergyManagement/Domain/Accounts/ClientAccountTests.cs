using Domain.EnergyManagement.Common;
using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Accounts;

public class ClientAccountTests
{
    [Fact]
    public void Register_creates_active_account()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = ClientAccount.Register(
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
            createdAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(ValidDomainTestData.Email);
        result.Value.PasswordHash.Should().Be(ValidDomainTestData.PasswordHash);
        result.Value.Role.Should().Be(AccountRole.Client);
        result.Value.ActivationState.Should().Be(AccountActivationState.Active);
        result.Value.IsActive.Should().BeTrue();
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Register_rejects_missing_email()
    {
        var result = ClientAccount.Register(
            null!,
            ValidDomainTestData.PasswordHash,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.Account.EmailIsRequired);
    }

    [Fact]
    public void EnsureActivated_succeeds_for_active_account()
    {
        var account = CreateActiveAccount();

        var result = account.EnsureActivated();

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void EnsureActivated_fails_for_non_active_account()
    {
        var account = CreateActiveAccount();
        SetIsActive(account, false);

        var result = account.EnsureActivated();

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.AccountNotActivated);
    }

    private static ClientAccount CreateActiveAccount()
    {
        return ClientAccount.Register(
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
            DateTimeOffset.UtcNow).Value;
    }

    private static void SetIsActive(
        ClientAccount account,
        bool isActive)
    {
        typeof(Account)
            .GetProperty(nameof(Account.IsActive))!
            .SetValue(account, isActive);
    }
}
