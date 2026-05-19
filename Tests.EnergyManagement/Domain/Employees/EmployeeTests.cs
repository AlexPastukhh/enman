using Domain.EnergyManagement;
using FluentAssertions;
using Tests.EnergyManagement.TestHelpers.App;
using static Domain.EnergyManagement.Common.Error;

namespace Tests.EnergyManagement.Domain.Employees;

public class EmployeeTests
{
    [Fact]
    public void Create_creates_active_employee()
    {
        var createdAt = DateTimeOffset.UtcNow;

        var result = Employee.Create(
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
            ValidDomainTestData.FullName,
            createdAt,
            "TESTDOMAIN\\employee");

        result.IsSuccess.Should().BeTrue();
        result.Value.Role.Should().Be(AccountRole.Employee);
        result.Value.Email.Should().Be(ValidDomainTestData.Email);
        result.Value.PasswordHash.Should().Be(ValidDomainTestData.PasswordHash);
        result.Value.FullName.Should().Be(ValidDomainTestData.FullName);
        result.Value.WindowsLogin.Should().Be("TESTDOMAIN\\employee");
        result.Value.IsActive.Should().BeTrue();
        result.Value.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Create_fails_when_email_is_missing()
    {
        var result = Employee.Create(
            null!,
            ValidDomainTestData.PasswordHash,
            ValidDomainTestData.FullName,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.Account.EmailIsRequired);
    }

    [Fact]
    public void Create_fails_when_password_hash_is_missing()
    {
        var result = Employee.Create(
            ValidDomainTestData.Email,
            null!,
            ValidDomainTestData.FullName,
            DateTimeOffset.UtcNow);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(Errors.Account.PasswordIsRequired);
    }

    [Fact]
    public void Create_fails_when_full_name_is_missing()
    {
        var result = Employee.Create(
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
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
            ValidDomainTestData.Email,
            ValidDomainTestData.PasswordHash,
            ValidDomainTestData.FullName,
            DateTimeOffset.UtcNow).Value;
    }
}
