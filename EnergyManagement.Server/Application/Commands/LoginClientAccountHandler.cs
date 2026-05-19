using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application;
using EnergyManagement.Server.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Commands;

public sealed class LoginClientAccountHandler
    : IRequestHandler<LoginClientAccountCommand, Result<LoginClientAccountResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;

    public LoginClientAccountHandler(IAccountRepository accounts)
    {
        _accounts = accounts;
    }

    public async Task<Result<LoginClientAccountResponse, IReadOnlyList<Error>>> Handle(
        LoginClientAccountCommand command,
        CancellationToken cancellationToken)
    {
        var email = ValidatedInput.ValueOrThrow(
            Email.Create(command.Email),
            "Email was validated by FluentValidation but Email.Create failed.");

        var account = await _accounts.GetByEmailAsync(email, cancellationToken);
        if (account is null)
        {
            return InvalidCredentials();
        }

        var activationResult = account.EnsureActivated();
        if (activationResult.IsFailure)
        {
            return Result.Failure<LoginClientAccountResponse, IReadOnlyList<Error>>(
                activationResult.Error);
        }

        if (PasswordHash.VerifyPlainTextPassword(account.PasswordHash, command.Password).IsFailure)
        {
            return InvalidCredentials();
        }

        return Result.Success<LoginClientAccountResponse, IReadOnlyList<Error>>(
            new LoginClientAccountResponse(
                account.Id,
                account.Email.Value,
                account.Role.ToString(),
                account.IsActive,
                account));
    }

    private static Result<LoginClientAccountResponse, IReadOnlyList<Error>> InvalidCredentials()
    {
        return Result.Failure<LoginClientAccountResponse, IReadOnlyList<Error>>(
            [Errors.Account.PasswordIsWrong]);
    }
}
