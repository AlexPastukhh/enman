using System.Reflection;
using CSharpFunctionalExtensions;

namespace Tests.EnergyManagement.Domain;

internal static class L1TestEntityExtensions
{
    public static T WithId<T>(this T entity, long id)
        where T : Entity
    {
        var idProperty = FindIdProperty(entity.GetType());
        idProperty.SetValue(entity, id);

        return entity;
    }

    private static PropertyInfo FindIdProperty(Type type)
    {
        while (type is not null)
        {
            var property = type.GetProperty(
                "Id",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (property is not null)
            {
                return property;
            }

            type = type.BaseType!;
        }

        throw new InvalidOperationException("Entity Id property was not found.");
    }
}
