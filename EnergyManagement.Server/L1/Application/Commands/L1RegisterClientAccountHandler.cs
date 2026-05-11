using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Application.Security;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1RegisterClientAccountHandler
    : IRequestHandler<L1RegisterClientAccountCommand, Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;
    private readonly L1DbContext _context;

    public L1RegisterClientAccountHandler(
        IAccountRepository accounts,
        L1DbContext context)
    {
        _accounts = accounts;
        _context = context;
    }

    public async Task<Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>> Handle(
        L1RegisterClientAccountCommand command,
        CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(emailResult.Error);
        }

        if (await _accounts.ExistsByEmailAsync(emailResult.Value, cancellationToken))
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(
                [Errors.Account.EmailIsRegisteredAlready]);
        }

        var passwordHashResult = L1PasswordHasher.HashPassword(command.Password);
        if (passwordHashResult.IsFailure)
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(passwordHashResult.Error);
        }

        var accountResult = ClientAccount.Create(emailResult.Value, passwordHashResult.Value);
        if (accountResult.IsFailure)
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(accountResult.Error);
        }

        _accounts.Add(accountResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(
            new L1RegisterClientAccountResponse(
                accountResult.Value.Id,
                accountResult.Value.Email.Value));
    }
}
