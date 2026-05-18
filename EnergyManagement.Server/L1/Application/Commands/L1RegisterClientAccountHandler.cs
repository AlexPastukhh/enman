using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application;
using EnergyManagement.Server.L1.Application.Abstractions;
using EnergyManagement.Server.L1.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.L1.Application.Commands;

public sealed class L1RegisterClientAccountHandler
    : IRequestHandler<L1RegisterClientAccountCommand, Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;
    private readonly L1DbContext _context;
    private readonly IRegistrationEmailNotificationService _registrationEmailNotifications;
    private readonly ILogger<L1RegisterClientAccountHandler> _logger;

    public L1RegisterClientAccountHandler(
        IAccountRepository accounts,
        L1DbContext context,
        IRegistrationEmailNotificationService registrationEmailNotifications,
        ILogger<L1RegisterClientAccountHandler> logger)
    {
        _accounts = accounts;
        _context = context;
        _registrationEmailNotifications = registrationEmailNotifications;
        _logger = logger;
    }

    public async Task<Result<L1RegisterClientAccountResponse, IReadOnlyList<Error>>> Handle(
        L1RegisterClientAccountCommand command,
        CancellationToken cancellationToken)
    {
        var email = ValidatedInput.ValueOrThrow(
            Email.Create(command.Email),
            "Email was validated by FluentValidation but Email.Create failed.");

        if (await _accounts.ExistsByEmailAsync(email, cancellationToken))
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(
                [Errors.Account.EmailIsRegisteredAlready]);
        }

        var passwordHash = ValidatedInput.ValueOrThrow(
            PasswordHash.CreateFromPlainTextPassword(command.Password),
            "Password was validated by FluentValidation but PasswordHash.CreateFromPlainTextPassword failed.");

        var accountResult = ClientAccount.Create(email, passwordHash);
        if (accountResult.IsFailure)
        {
            return Result.Failure<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(accountResult.Error);
        }

        _accounts.Add(accountResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        try
        {
            await _registrationEmailNotifications.SendRegistrationEmailAsync(
                accountResult.Value.Id,
                accountResult.Value.Email.Value,
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Registration email notification failed for account {AccountId}.",
                accountResult.Value.Id);
        }

        return Result.Success<L1RegisterClientAccountResponse, IReadOnlyList<Error>>(
            new L1RegisterClientAccountResponse(
                accountResult.Value.Id,
                accountResult.Value.Email.Value));
    }
}
