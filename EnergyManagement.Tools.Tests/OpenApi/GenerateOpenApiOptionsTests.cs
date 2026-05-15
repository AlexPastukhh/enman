using EnergyManagement.Tools.OpenApi;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.OpenApi;

public sealed class GenerateOpenApiOptionsTests
{
    [Fact]
    public void TryParse_ParsesRequiredOutAndDefaults()
    {
        var parsed = GenerateOpenApiOptions.TryParse(
            new[] { "generate-openapi", "--out", "Shared/openapi.json" },
            out var options,
            out var error);

        parsed.Should().BeTrue(error);
        options.OutputPath.Should().Be("Shared/openapi.json");
        options.Check.Should().BeFalse();
        options.ServerUrl.Should().Be(GenerateOpenApiOptions.DefaultServerUrl);
        options.SwaggerPath.Should().Be(GenerateOpenApiOptions.DefaultSwaggerPath);
        options.ProjectPath.Should().Be(GenerateOpenApiOptions.DefaultProjectPath);
        options.TimeoutSeconds.Should().Be(GenerateOpenApiOptions.DefaultTimeoutSeconds);
    }

    [Fact]
    public void TryParse_ParsesCheckAndOptionalValues()
    {
        var parsed = GenerateOpenApiOptions.TryParse(
            new[]
            {
                "generate-openapi",
                "--out",
                "artifact.json",
                "--check",
                "--server-url",
                "https://localhost:7250/",
                "--swagger-path",
                "swagger/custom.json",
                "--project",
                "Server.csproj",
                "--timeout-seconds",
                "5"
            },
            out var options,
            out var error);

        parsed.Should().BeTrue(error);
        options.Check.Should().BeTrue();
        options.ServerUrl.Should().Be("https://localhost:7250");
        options.SwaggerPath.Should().Be("/swagger/custom.json");
        options.ProjectPath.Should().Be("Server.csproj");
        options.TimeoutSeconds.Should().Be(5);
    }

    [Fact]
    public void TryParse_ReturnsFailure_WhenOutIsMissing()
    {
        var parsed = GenerateOpenApiOptions.TryParse(
            new[] { "generate-openapi" },
            out _,
            out var error);

        parsed.Should().BeFalse();
        error.Should().Contain("--out");
    }
}
