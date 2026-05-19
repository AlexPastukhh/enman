using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement;

public abstract class DomainEntity : Entity
{
    protected static long GuardPersistedReferenceId(
        DomainEntity referencedEntity,
        string parameterName)
    {
        Guard.IsNotNull(referencedEntity);

        if (referencedEntity.Id <= 0)
        {
            throw new ArgumentException(
                "Referenced aggregate must already be persisted and have Id > 0.",
                parameterName);
        }

        return referencedEntity.Id;
    }
}
