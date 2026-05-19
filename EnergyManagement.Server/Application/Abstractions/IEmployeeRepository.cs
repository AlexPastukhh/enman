using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<Employee?> GetByWindowsLoginAsync(string windowsLogin, CancellationToken cancellationToken);
}
