namespace EnergyManagement.Server.Application.Abstractions;

public interface IRegistrationEmailNotificationService
{
    Task SendRegistrationEmailAsync(
        long accountId,
        string email,
        CancellationToken cancellationToken);
}
