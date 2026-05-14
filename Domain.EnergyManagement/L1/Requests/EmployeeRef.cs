using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class EmployeeRef : ValueObject
{
    public long Id { get; }

    private EmployeeRef(long id)
    {
        Id = id;
    }

    private EmployeeRef()
    {
    }

    public static Result<EmployeeRef, IReadOnlyList<Error>> Create(long id)
    {
        if (id <= 0)
        {
            return Result.Failure<EmployeeRef, IReadOnlyList<Error>>(
                [Errors.L1Domain.ReviewerIsRequired]);
        }

        return Result.Success<EmployeeRef, IReadOnlyList<Error>>(new EmployeeRef(id));
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Id;
    }
}
