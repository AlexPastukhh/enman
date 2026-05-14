using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public abstract class Account : L1Entity
{
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public AccountRole Role { get; private set; }
    public AccountActivationState ActivationState =>
        IsActive
            ? AccountActivationState.Active
            : AccountActivationState.PendingActivation;
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    protected Account(
        Email email,
        PasswordHash passwordHash,
        AccountRole role,
        DateTimeOffset createdAt)
    {
        Guard.IsNotNull(email);
        Guard.IsNotNull(passwordHash);

        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        CreatedAt = createdAt;
    }

    protected Account()
    {
        Email = null!;
        PasswordHash = null!;
    }

    public UnitResult<IReadOnlyList<Error>> EnsureActivated()
    {
        if (ActivationState != AccountActivationState.Active)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.AccountNotActivated]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
