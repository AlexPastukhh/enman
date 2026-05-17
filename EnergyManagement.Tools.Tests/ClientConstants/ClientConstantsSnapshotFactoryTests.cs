using System.Reflection;
using EnergyManagement.Server.Api.Contracts.Common;
using EnergyManagement.Tools.ClientConstants;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.ClientConstants;

public sealed class ClientConstantsSnapshotFactoryTests
{
    [Fact]
    public void CreateErrorCodesSnapshot_ContainsRequiredTopLevelSections()
    {
        var snapshot = new ClientConstantsSnapshotFactory().CreateErrorCodesSnapshot();

        snapshot.Email.Should().NotBeNull();
        snapshot.Password.Should().NotBeNull();
        snapshot.PasswordConfirmation.Should().NotBeNull();
        snapshot.Phone.Should().NotBeNull();
        snapshot.ServerValidationError.Should().NotBeNull();
    }

    [Fact]
    public void CreateErrorCodesSnapshot_ContainsServerValidationErrorContract()
    {
        var snapshot = new ClientConstantsSnapshotFactory().CreateErrorCodesSnapshot();

        snapshot.ServerValidationError.FieldNameField.Should().Be(nameof(ServerValidationError.FieldName));
        snapshot.ServerValidationError.ErrorCodeField.Should().Be(nameof(ServerValidationError.ErrorCode));
    }

    [Fact]
    public void ErrorCodes_AreUnique()
    {
        var snapshot = new ClientConstantsSnapshotFactory().CreateErrorCodesSnapshot();
        var values = GetErrorCodeValues(snapshot).ToList();

        values.Should().OnlyHaveUniqueItems();
    }

    private static IEnumerable<string> GetErrorCodeValues(ErrorObject snapshot)
    {
        foreach (var topLevelProperty in snapshot.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (topLevelProperty.Name == nameof(ErrorObject.ServerValidationError))
            {
                continue;
            }

            var section = topLevelProperty.GetValue(snapshot);
            if (section is null)
            {
                continue;
            }

            foreach (var valueProperty in section.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (valueProperty.PropertyType == typeof(string) &&
                    valueProperty.GetValue(section) is string value)
                {
                    yield return value;
                }
            }
        }
    }
}
