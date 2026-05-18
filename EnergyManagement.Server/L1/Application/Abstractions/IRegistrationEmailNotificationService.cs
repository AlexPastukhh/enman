namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IRegistrationEmailNotificationService
{
    Task SendRegistrationEmailAsync(
        long accountId,
        string email,
        CancellationToken cancellationToken);
}
