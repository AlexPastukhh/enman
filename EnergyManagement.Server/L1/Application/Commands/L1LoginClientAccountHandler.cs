using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application;
using EnergyManagement.Server.L1.Application.Abstractions;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1LoginClientAccountHandler
    : IRequestHandler<L1LoginClientAccountCommand, Result<L1LoginClientAccountResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;

    public L1LoginClientAccountHandler(IAccountRepository accounts)
    {
        _accounts = accounts;
    }

    public async Task<Result<L1LoginClientAccountResponse, IReadOnlyList<Error>>> Handle(
        L1LoginClientAccountCommand command,
        CancellationToken cancellationToken)
    {
        var email = ValidatedInput.ValueOrThrow(
            Email.Create(command.Email),
            "Email was validated by FluentValidation but Email.Create failed.");

        var account = await _accounts.GetByEmailAsync(email, cancellationToken);
        if (account is not ClientAccount clientAccount)
        {
            return InvalidCredentials();
        }

        var activationResult = clientAccount.EnsureActivated();
        if (activationResult.IsFailure)
        {
            return Result.Failure<L1LoginClientAccountResponse, IReadOnlyList<Error>>(
                activationResult.Error);
        }

        if (PasswordHash.VerifyPlainTextPassword(clientAccount.PasswordHash, command.Password).IsFailure)
        {
            return InvalidCredentials();
        }

        return Result.Success<L1LoginClientAccountResponse, IReadOnlyList<Error>>(
            new L1LoginClientAccountResponse(
                clientAccount.Id,
                clientAccount.Email.Value,
                clientAccount.Role.ToString(),
                clientAccount.IsActive));
    }

    private static Result<L1LoginClientAccountResponse, IReadOnlyList<Error>> InvalidCredentials()
    {
        return Result.Failure<L1LoginClientAccountResponse, IReadOnlyList<Error>>(
            [Errors.Account.PasswordIsWrong]);
    }
}
