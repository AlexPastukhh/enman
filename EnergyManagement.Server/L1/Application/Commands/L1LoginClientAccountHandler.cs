using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Security;
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
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return InvalidCredentials();
        }

        var account = await _accounts.GetByEmailAsync(emailResult.Value, cancellationToken);
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

        if (!L1PasswordHasher.VerifyPassword(clientAccount.PasswordHash, command.Password))
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
