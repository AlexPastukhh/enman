using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class Employee : L1Entity
{
    public long AccountId { get; private set; }

    public FullName FullName { get; private set; }

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    private Employee()
    {
        FullName = null!;
    }

    private Employee(
        long accountId,
        FullName fullName,
        DateTimeOffset createdAt)
    {
        AccountId = accountId;
        FullName = fullName;
        IsActive = true;
        CreatedAt = createdAt;
    }

    public static Result<Employee, IReadOnlyList<Error>> Create(
        long accountId,
        FullName fullName,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (accountId <= 0)
        {
            errors.Add(Errors.L1Domain.EmployeeAccountIsRequired);
        }

        if (fullName is null)
        {
            errors.Add(Errors.L1Domain.EmployeeFullNameIsRequired);
        }

        if (errors.Count > 0)
        {
            return Result.Failure<Employee, IReadOnlyList<Error>>(errors);
        }

        return Result.Success<Employee, IReadOnlyList<Error>>(
            new Employee(accountId, fullName!, createdAt));
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanReview()
    {
        return EnsureActive();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanStartAgreementExchange()
    {
        return EnsureActive();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanSendAgreementProposal()
    {
        return EnsureActive();
    }

    public UnitResult<IReadOnlyList<Error>> EnsureCanFinalRefuseAgreement()
    {
        return EnsureActive();
    }

    private UnitResult<IReadOnlyList<Error>> EnsureActive()
    {
        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
