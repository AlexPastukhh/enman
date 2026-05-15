using EnergyManagement.Tools.OpenApi;
using EnergyManagement.Tools.Tests.ClientConstants;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.OpenApi;

public sealed class OpenApiArtifactCheckerTests
{
    [Fact]
    public async Task Checker_ReturnsSuccess_WhenFileMatches()
    {
        using var tempDirectory = TempDirectory.Create();
        var outputPath = Path.Combine(tempDirectory.Path, "openapi.json");
        await File.WriteAllTextAsync(outputPath, "{\n  \"openapi\": \"3.0.1\"\n}\n");

        var result = await new OpenApiArtifactChecker().CheckAsync(
            "{\n  \"openapi\": \"3.0.1\"\n}\n",
            outputPath);

        result.IsSuccess.Should().BeTrue();
        result.MismatchedFiles.Should().BeEmpty();
    }

    [Fact]
    public async Task Checker_ReturnsFailure_WhenFileMissing()
    {
        using var tempDirectory = TempDirectory.Create();
        var outputPath = Path.Combine(tempDirectory.Path, "openapi.json");

        var result = await new OpenApiArtifactChecker().CheckAsync("{}", outputPath);

        result.IsSuccess.Should().BeFalse();
        result.MismatchedFiles.Should().ContainSingle(outputPath);
    }

    [Fact]
    public async Task Checker_ReturnsFailure_WhenFileOutdated()
    {
        using var tempDirectory = TempDirectory.Create();
        var outputPath = Path.Combine(tempDirectory.Path, "openapi.json");
        await File.WriteAllTextAsync(outputPath, "{\n  \"old\": true\n}\n");

        var result = await new OpenApiArtifactChecker().CheckAsync(
            "{\n  \"old\": false\n}\n",
            outputPath);

        result.IsSuccess.Should().BeFalse();
        result.MismatchedFiles.Should().ContainSingle(outputPath);
    }

    [Fact]
    public async Task Checker_DoesNotModifyFile_InCheckMode()
    {
        using var tempDirectory = TempDirectory.Create();
        var outputPath = Path.Combine(tempDirectory.Path, "openapi.json");
        const string originalContent = "{\n  \"old\": true\n}\n";
        await File.WriteAllTextAsync(outputPath, originalContent);

        await new OpenApiArtifactChecker().CheckAsync(
            "{\n  \"old\": false\n}\n",
            outputPath);

        var contentAfterCheck = await File.ReadAllTextAsync(outputPath);
        contentAfterCheck.Should().Be(originalContent);
    }
}
