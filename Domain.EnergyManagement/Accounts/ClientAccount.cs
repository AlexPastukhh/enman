using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement;

public sealed class ClientAccount : Account
{
    private ClientAccount(
        Email email,
        PasswordHash passwordHash,
        DateTimeOffset createdAt)
        : base(email, passwordHash, AccountRole.Client, createdAt)
    {
    }

    private ClientAccount()
    {
    }

    public static Result<ClientAccount, IReadOnlyList<Error>> Register(
        Email email,
        PasswordHash passwordHash,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (email is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (passwordHash is null)
        {
            errors.Add(Errors.Account.PasswordIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<ClientAccount, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<ClientAccount, IReadOnlyList<Error>>(
            new ClientAccount(email!, passwordHash!, createdAt));
    }

    public static Result<ClientAccount, IReadOnlyList<Error>> Create(
        Email email,
        PasswordHash passwordHash)
    {
        return Register(email, passwordHash, DateTimeOffset.UtcNow);
    }
}
