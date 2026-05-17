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
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
            L1ValidTestData.FullName,
            createdAt,
            "TESTDOMAIN\\employee");

        result.IsSuccess.Should().BeTrue();
        result.Value.Role.Should().Be(AccountRole.Employee);
        result.Value.Email.Should().Be(L1ValidTestData.Email);
        result.Value.PasswordHash.Should().Be(L1ValidTestData.PasswordHash);
        result.Value.FullName.Should().Be(L1ValidTestData.FullName);
        result.Value.WindowsLogin.Should().Be("TESTDOMAIN\\employee");
        result.Value.IsActive.Should().BeTrue();
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_fails_when_email_is_missing()
    {
        var result = Employee.Create(
            null!,
            L1ValidTestData.PasswordHash,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.Account.EmailIsRequired);
    }

    [Fact]
    public void Create_fails_when_password_hash_is_missing()
    {
        var result = Employee.Create(
            L1ValidTestData.Email,
            null!,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.Account.PasswordIsRequired);
    }

    [Fact]
    public void Create_fails_when_full_name_is_missing()
    {
        var result = Employee.Create(
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
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
            L1ValidTestData.Email,
            L1ValidTestData.PasswordHash,
            L1ValidTestData.FullName,
            DateTimeOffset.UtcNow).Value;
    }
}
