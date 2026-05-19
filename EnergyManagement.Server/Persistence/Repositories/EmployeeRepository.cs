using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.Persistence.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly L1DbContext _context;

    public EmployeeRepository(L1DbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _context.Employees.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Employee?> GetByWindowsLoginAsync(string windowsLogin, CancellationToken cancellationToken)
    {
        var normalized = windowsLogin.Trim();
        return _context.Employees.FirstOrDefaultAsync(
            x => x.WindowsLogin == normalized,
            cancellationToken);
    }
}
