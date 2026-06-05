using EnergyManagement.Tools.ClientConstants;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.ClientConstants;

public sealed class ClientConstantsJsonSerializerTests
{
    [Fact]
    public void Serializer_ProducesStableOutput()
    {
        var factory = new ClientConstantsSnapshotFactory();
        var serializer = new ClientConstantsJsonSerializer();

        var first = serializer.Serialize(factory.Create());
        var second = serializer.Serialize(factory.Create());

        first.ConstantsJson.Should().Be(second.ConstantsJson);
        first.ErrorCodesJson.Should().Be(second.ErrorCodesJson);
    }

    [Fact]
    public void Serializer_KeepsExpectedPropertyCasing()
    {
        var artifacts = new ClientConstantsJsonSerializer()
            .Serialize(new ClientConstantsSnapshotFactory().Create());

        artifacts.ConstantsJson.Should().Contain("\"AuthConstants\"");
        artifacts.ConstantsJson.Should().Contain("\"RegisterClientAccount\"");
        artifacts.ConstantsJson.Should().Contain("\"ApplicantPartyConstants\"");
        artifacts.ConstantsJson.Should().Contain("\"FieldName\"");
        artifacts.ConstantsJson.Should().Contain("\"DtoFieldName\"");
        artifacts.ErrorCodesJson.Should().Contain("\"ServerValidationError\"");
        artifacts.ErrorCodesJson.Should().Contain("\"FieldNameField\"");
        artifacts.ErrorCodesJson.Should().Contain("\"ErrorCodeField\"");
    }
}
