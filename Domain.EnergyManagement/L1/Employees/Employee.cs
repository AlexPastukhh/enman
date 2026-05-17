using CSharpFunctionalExtensions;
using Domain.EnergyManagement.Common;
using Domain.EnergyManagement.DocumentManaging;
using static Domain.EnergyManagement.Common.Error;

namespace Domain.EnergyManagement.L1;

public sealed class Employee : Account
{
    public FullName FullName { get; private set; }

    private Employee()
    {
        FullName = null!;
    }

    private Employee(
        Email email,
        PasswordHash passwordHash,
        FullName fullName,
        DateTimeOffset createdAt)
        : base(email, passwordHash, AccountRole.Employee, createdAt)
    {
        FullName = fullName;
    }

    public static Result<Employee, IReadOnlyList<Error>> Create(
        Email email,
        PasswordHash passwordHash,
        FullName fullName,
        DateTimeOffset createdAt)
    {
        var errors = new List<Error>();

        if (email is null)
        {
            errors.Add(Errors.Account.EmailIsRequired);
        }

        if (passwordHash is null)
        {
            errors.Add(Errors.Account.PasswordIsRequired);
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
            new Employee(email!, passwordHash!, fullName!, createdAt));
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
        if (Role != AccountRole.Employee)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsRequired]);
        }

        if (!IsActive)
        {
            return UnitResult.Failure<IReadOnlyList<Error>>(
                [Errors.L1Domain.EmployeeIsNotActive]);
        }

        return UnitResult.Success<IReadOnlyList<Error>>();
    }
}
