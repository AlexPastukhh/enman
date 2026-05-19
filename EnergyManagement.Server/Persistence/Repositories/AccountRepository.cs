using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;
using EnergyManagement.Server.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EnergyManagement.Server.Persistence.Repositories;

public sealed class AccountRepository : IAccountRepository
{
    private readonly L1DbContext _context;

    public AccountRepository(L1DbContext context)
    {
        _context = context;
    }

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
    }

    public Task<Account?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _context.Accounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return _context.Accounts.FirstOrDefaultAsync(
            x => x.Email.Value == email.Value,
            cancellationToken);
    }

    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return _context.Accounts.AnyAsync(
            x => x.Email.Value == email.Value,
            cancellationToken);
    }
}
