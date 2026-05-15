using EnergyManagement.Tools.ClientConstants;
using FluentAssertions;

namespace EnergyManagement.Tools.Tests.ClientConstants;

public sealed class ClientConstantsWriterTests
{
    [Fact]
    public async Task Writer_WritesConstantsAndErrorCodesFiles()
    {
        using var temp = TempDirectory.Create();
        var artifacts = CreateArtifacts();

        await new ClientConstantsWriter().WriteAsync(artifacts, temp.Path);

        var constantsJson = await File.ReadAllTextAsync(
            Path.Combine(temp.Path, ClientConstantsArtifacts.ConstantsFileName));
        var errorCodesJson = await File.ReadAllTextAsync(
            Path.Combine(temp.Path, ClientConstantsArtifacts.ErrorCodesFileName));

        constantsJson.Should().Be(artifacts.ConstantsJson);
        errorCodesJson.Should().Be(artifacts.ErrorCodesJson);
    }

    [Fact]
    public async Task Writer_CreatesOutputDirectory_WhenItDoesNotExist()
    {
        using var temp = TempDirectory.Create();
        var outputDirectory = Path.Combine(temp.Path, "nested", "Shared");

        await new ClientConstantsWriter().WriteAsync(CreateArtifacts(), outputDirectory);

        Directory.Exists(outputDirectory).Should().BeTrue();
        File.Exists(Path.Combine(outputDirectory, ClientConstantsArtifacts.ConstantsFileName)).Should().BeTrue();
        File.Exists(Path.Combine(outputDirectory, ClientConstantsArtifacts.ErrorCodesFileName)).Should().BeTrue();
    }

    [Fact]
    public async Task Writer_Throws_WhenOutputPathIsAFile()
    {
        using var temp = TempDirectory.Create();
        var outputPath = Path.Combine(temp.Path, "Shared");
        await File.WriteAllTextAsync(outputPath, "not a directory");

        var act = () => new ClientConstantsWriter().WriteAsync(CreateArtifacts(), outputPath);

        await act.Should().ThrowAsync<IOException>();
    }

    private static ClientConstantsArtifacts CreateArtifacts()
        => new("constants", "errorcodes");
}
