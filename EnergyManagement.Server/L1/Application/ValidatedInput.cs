using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;

namespace EnergyManagement.Server.L1.Application;

internal static class ValidatedInput
{
    public static TValue ValueOrThrow<TValue>(
        Result<TValue, IReadOnlyList<Error>> result,
        string message)
    {
        if (result.IsFailure)
        {
            throw new InvalidOperationException(message);
        }

        return result.Value;
    }
}
