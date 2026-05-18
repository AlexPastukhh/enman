using System.Net;
using System.Net.Mail;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.Extensions.Options;

namespace EnergyManagement.Server.L1.Infrastructure.Email;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly IOptions<SmtpEmailOptions> _options;

    public SmtpEmailSender(IOptions<SmtpEmailOptions> options)
    {
        _options = options;
    }

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        var options = _options.Value;

        if (string.IsNullOrWhiteSpace(options.Host))
        {
            throw new InvalidOperationException("SMTP host is not configured.");
        }

        if (string.IsNullOrWhiteSpace(options.From))
        {
            throw new InvalidOperationException("SMTP from address is not configured.");
        }

        using var smtp = new SmtpClient(options.Host, options.Port)
        {
            EnableSsl = options.EnableSsl
        };

        if (!string.IsNullOrWhiteSpace(options.UserName))
        {
            smtp.Credentials = new NetworkCredential(options.UserName, options.Password);
        }

        using var mail = new MailMessage(
            from: options.From,
            to: message.To,
            subject: message.Subject,
            body: message.Body);

        cancellationToken.ThrowIfCancellationRequested();
        await smtp.SendMailAsync(mail);
    }
}
