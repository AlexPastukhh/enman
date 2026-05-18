using EnergyManagement.Server.L1.Application.Abstractions;

namespace Tests.EnergyManagement.Integration.L1.Auth;

internal sealed class FakeEmailSender : IEmailSender
{
    private readonly List<EmailMessage> _sentMessages = new();

    public IReadOnlyList<EmailMessage> SentMessages => _sentMessages;

    public int SendAttempts { get; private set; }

    public bool ThrowOnSend { get; set; }

    public Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        SendAttempts++;
        _sentMessages.Add(message);

        if (ThrowOnSend)
        {
            throw new InvalidOperationException("Fake email sender failed.");
        }

        return Task.CompletedTask;
    }

    public void Clear()
    {
        _sentMessages.Clear();
        SendAttempts = 0;
    }
}
