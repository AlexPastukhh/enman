using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application;
using EnergyManagement.Server.Application.Abstractions;
using EnergyManagement.Server.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using static Domain.EnergyManagement.Common.Error;

namespace EnergyManagement.Server.Application.Commands;

public sealed class RegisterClientAccountHandler
    : IRequestHandler<RegisterClientAccountCommand, Result<RegisterClientAccountResponse, IReadOnlyList<Error>>>
{
    private readonly IAccountRepository _accounts;
    private readonly EnergyManagementDbContext _context;
    private readonly IRegistrationEmailNotificationService _registrationEmailNotifications;
    private readonly ILogger<RegisterClientAccountHandler> _logger;

    public RegisterClientAccountHandler(
        IAccountRepository accounts,
        EnergyManagementDbContext context,
        IRegistrationEmailNotificationService registrationEmailNotifications,
        ILogger<RegisterClientAccountHandler> logger)
    {
        _accounts = accounts;
        _context = context;
        _registrationEmailNotifications = registrationEmailNotifications;
        _logger = logger;
    }

    public async Task<Result<RegisterClientAccountResponse, IReadOnlyList<Error>>> Handle(
        RegisterClientAccountCommand command,
        CancellationToken cancellationToken)
    {
        var email = ValidatedInput.ValueOrThrow(
            Email.Create(command.Email),
            "Email was validated by FluentValidation but Email.Create failed.");

        if (await _accounts.ExistsByEmailAsync(email, cancellationToken))
        {
            return Result.Failure<RegisterClientAccountResponse, IReadOnlyList<Error>>(
                [Errors.Account.EmailIsRegisteredAlready]);
        }

        var passwordHash = ValidatedInput.ValueOrThrow(
            PasswordHash.CreateFromPlainTextPassword(command.Password),
            "Password was validated by FluentValidation but PasswordHash.CreateFromPlainTextPassword failed.");

        var accountResult = ClientAccount.Create(email, passwordHash);
        if (accountResult.IsFailure)
        {
            return Result.Failure<RegisterClientAccountResponse, IReadOnlyList<Error>>(accountResult.Error);
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

        return Result.Success<RegisterClientAccountResponse, IReadOnlyList<Error>>(
            new RegisterClientAccountResponse(
                accountResult.Value.Id,
                accountResult.Value.Email.Value));
    }
}
