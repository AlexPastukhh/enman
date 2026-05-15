using EnergyManagement.Tools.OpenApi;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.OpenApi;

public sealed class OpenApiJsonFormatterTests
{
    [Fact]
    public void Formatter_ProducesStableIndentedJson()
    {
        var formatter = new OpenApiJsonFormatter();
        const string rawJson = """{"openapi":"3.0.1","info":{"title":"API","version":"v1"}}""";

        var first = formatter.Format(rawJson);
        var second = formatter.Format(rawJson);

        first.Should().Be(second);
        first.Should().Contain("\n  \"info\": {");
        first.Should().EndWith("\n");
        first.Should().NotContain("\r\n");
    }
}
