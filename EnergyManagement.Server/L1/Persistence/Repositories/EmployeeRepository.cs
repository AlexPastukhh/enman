using Domain.EnergyManagement.L1;
using EnergyManagement.Server.L1.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.L1.Persistence.Repositories;

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
}
