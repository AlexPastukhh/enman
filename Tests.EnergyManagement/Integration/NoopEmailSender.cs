using EnergyManagement.Server.Application.Abstractions;

namespace Tests.EnergyManagement.Integration;

internal sealed class NoopEmailSender : IEmailSender
{
    public Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
