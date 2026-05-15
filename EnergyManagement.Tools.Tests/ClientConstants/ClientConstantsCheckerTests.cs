using EnergyManagement.Tools.ClientConstants;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.ClientConstants;

public sealed class ClientConstantsCheckerTests
{
    [Fact]
    public async Task Checker_ReturnsSuccess_WhenFilesMatchGeneratedArtifacts()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();
        await WriteArtifactsAsync(temp.Path, artifacts);

        var result = await new ClientConstantsChecker().CheckAsync(artifacts, temp.Path);

        result.IsSuccess.Should().BeTrue();
        result.MismatchedFiles.Should().BeEmpty();
    }

    [Fact]
    public async Task Checker_ReturnsFailure_WhenErrorCodesFileIsOutdated()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();
        await WriteArtifactsAsync(temp.Path, artifacts with { ErrorCodesJson = "old errorcodes" });

        var result = await new ClientConstantsChecker().CheckAsync(artifacts, temp.Path);

        result.IsSuccess.Should().BeFalse();
        result.MismatchedFiles.Should().ContainSingle()
            .Which.Should().EndWith(ClientConstantsArtifacts.ErrorCodesFileName);
    }

    [Fact]
    public async Task Checker_ReturnsFailure_WhenConstantsFileIsOutdated()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();
        await WriteArtifactsAsync(temp.Path, artifacts with { ConstantsJson = "old constants" });

        var result = await new ClientConstantsChecker().CheckAsync(artifacts, temp.Path);

        result.IsSuccess.Should().BeFalse();
        result.MismatchedFiles.Should().ContainSingle()
            .Which.Should().EndWith(ClientConstantsArtifacts.ConstantsFileName);
    }

    [Fact]
    public async Task Checker_ReturnsFailure_WhenFileIsMissing()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();
        await File.WriteAllTextAsync(
            Path.Combine(temp.Path, ClientConstantsArtifacts.ConstantsFileName),
            artifacts.ConstantsJson);

        var result = await new ClientConstantsChecker().CheckAsync(artifacts, temp.Path);

        result.IsSuccess.Should().BeFalse();
        result.MismatchedFiles.Should().ContainSingle()
            .Which.Should().EndWith(ClientConstantsArtifacts.ErrorCodesFileName);
    }

    [Fact]
    public async Task Checker_DoesNotModifyFiles_WhenMismatchExists()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();
        var existingArtifacts = artifacts with { ConstantsJson = "old constants" };
        await WriteArtifactsAsync(temp.Path, existingArtifacts);
        var constantsPath = Path.Combine(temp.Path, ClientConstantsArtifacts.ConstantsFileName);
        var originalLastWriteTime = File.GetLastWriteTimeUtc(constantsPath);

        await Task.Delay(20);
        var result = await new ClientConstantsChecker().CheckAsync(artifacts, temp.Path);

        result.IsSuccess.Should().BeFalse();
        var constantsJson = await File.ReadAllTextAsync(constantsPath);
        constantsJson.Should().Be(existingArtifacts.ConstantsJson);
        File.GetLastWriteTimeUtc(constantsPath).Should().Be(originalLastWriteTime);
    }

    private static async Task WriteArtifactsAsync(string outputDirectory, ClientConstantsArtifacts artifacts)
    {
        Directory.CreateDirectory(outputDirectory);
        await File.WriteAllTextAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ConstantsFileName),
            artifacts.ConstantsJson);
        await File.WriteAllTextAsync(
            Path.Combine(outputDirectory, ClientConstantsArtifacts.ErrorCodesFileName),
            artifacts.ErrorCodesJson);
    }

    private static ClientConstantsArtifacts CreateArtifacts()
        => new("constants", "errorcodes");
}
