using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;

namespace Domain.EnergyManagement.L1;

public abstract class L1Entity : Entity
{
    protected static long GuardPersistedReferenceId(
        L1Entity referencedEntity,
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
