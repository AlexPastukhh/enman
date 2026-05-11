using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement.L1;

namespace EnergyManagement.Server.L1.Application.Abstractions;

public interface IAccountRepository
{
    void Add(Account account);

    Task<Account?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
}
