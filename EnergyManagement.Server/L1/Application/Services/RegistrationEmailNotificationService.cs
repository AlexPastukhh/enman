using EnergyManagement.Server.L1.Application.Abstractions;

namespace EnergyManagement.Server.L1.Application.Services;

public sealed class RegistrationEmailNotificationService : IRegistrationEmailNotificationService
{
    private readonly IEmailSender _emailSender;

    public RegistrationEmailNotificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task SendRegistrationEmailAsync(
        long accountId,
        string email,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var message = new EmailMessage(
            email,
            "Registration completed",
            "Your account has been registered successfully.");

        return _emailSender.SendAsync(message, cancellationToken);
    }
}
