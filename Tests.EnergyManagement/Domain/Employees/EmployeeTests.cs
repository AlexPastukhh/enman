using Domain.EnergyManagement.L1;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.L1;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.L1Domain.Employees;

public class EmployeeTests
{
    [Fact]
    public void Create_creates_active_employee()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = Employee.Create(
            accountId: 10,
            L1ValidTestData.FullName,
            createdAt);

        result.IsSuccess.Should().BeTrue();
        result.Value.AccountId.Should().Be(10);
        result.Value.FullName.Should().Be(L1ValidTestData.FullName);
        result.Value.IsActive.Should().BeTrue();
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_fails_when_account_id_is_missing()
    {
        var result = Employee.Create(
            accountId: 0,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeAccountIsRequired);
    }

    [Fact]
    public void Create_fails_when_full_name_is_missing()
    {
        var result = Employee.Create(
            accountId: 10,
            null!,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.L1Domain.EmployeeFullNameIsRequired);
    }

    [Fact]
    public void Capability_methods_succeed_for_active_employee()
    {
        var employee = CreateEmployee();

        employee.EnsureCanReview().IsSuccess.Should().BeTrue();
        employee.EnsureCanStartAgreementExchange().IsSuccess.Should().BeTrue();
        employee.EnsureCanSendAgreementProposal().IsSuccess.Should().BeTrue();
        employee.EnsureCanFinalRefuseAgreement().IsSuccess.Should().BeTrue();
    }

    private static Employee CreateEmployee()
    {
        return Employee.Create(
            accountId: 10,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow).Value;
    }
}
