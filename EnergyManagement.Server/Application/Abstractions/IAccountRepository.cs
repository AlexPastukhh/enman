using Domain.EnergyManagement.DocumentManaging;
using Domain.EnergyManagement;

namespace EnergyManagement.Server.Application.Abstractions;

public interface IAccountRepository
{
    void Add(Account account);

    Task<Account?> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
}
