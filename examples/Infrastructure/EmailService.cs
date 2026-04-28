using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;
using Hospital.proj.Server.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Channels;

namespace Hospital.proj.Server.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly EmailOptions _emailOptions;
        public EmailService(ILogger<EmailService> logger, 
            IOptions<EmailOptions> emailOptions)
        {
            _logger = logger;
            _emailOptions = emailOptions.Value;
        }
        public async Task<UnitResult<Error>> SendEmailAsync(
            Email toEmail,
            string subject,
            string message,
            CancellationToken cancel = default)
          {

            var client =CreateClient(toEmail);

            var mailMsg = CreateMessage(toEmail,subject,message);

            try
            {
                await client.SendMailAsync(mailMsg, cancel);
            }
            catch(SmtpFailedRecipientException ex)
            {
                SmtpStatusCode status = ex.StatusCode;
                if (status == SmtpStatusCode.MailboxUnavailable)
                {

                    _logger.EmailAddressDoesntExistOrUnavailable(toEmail);
                    return Errors.Infrastructure.EmailAddressDoesntExistOrUnavailabe;
                    
                }
                else
                {
                    _logger.LogError(ex,"Exception occured during attempt to send " +
                        "{message} to {toEmail}", mailMsg,toEmail);
                    return Errors.General.InternalServerError;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception from {methodName}",nameof(client.SendMailAsync));
                throw;
            }

            return UnitResult.Success<Error>();

        }

        private SmtpClient CreateClient(Email toEmail)
        {
            var client = new SmtpClient(_emailOptions.Host, _emailOptions.Port)
            {
                EnableSsl = _emailOptions.IsSslEnabled,
               
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _emailOptions.Username,
                    _emailOptions.Password)

            };

            return client;
        }

        private MailMessage CreateMessage(Email toEmail,string subject,string message)
        {
            var mailMsg = new MailMessage(
            from: _emailOptions.From,
            to: toEmail.Value,
            subject: subject,
            body: message
            );

            mailMsg.IsBodyHtml=true;
            
            return mailMsg;
        }
    }
}
