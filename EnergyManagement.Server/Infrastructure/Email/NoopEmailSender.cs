using EnergyManagement.Server.Application.Abstractions;

namespace EnergyManagement.Server.Infrastructure.Email;

public sealed class NoopEmailSender : IEmailSender
{
    public Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        // Intentionally do nothing in test/dev when SMTP is not configured.
        return Task.CompletedTask;
    }
}
