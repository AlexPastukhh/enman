using CSharpFunctionalExtensions;
using Hospital.proj.Domain.Common;
using Hospital.proj.Domain.Users;

namespace Hospital.proj.Server.Infrastructure
{
    public interface IEmailService
    {
        Task<UnitResult<Error>> SendEmailAsync(
            Email toEmail,
            string subject,
            string message,
            CancellationToken cancel=default);
    }
}