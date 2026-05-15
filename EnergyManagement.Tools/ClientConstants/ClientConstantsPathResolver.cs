namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsPathResolver
{
    public string ResolveOutputDirectory(string outputDirectory)
    {
        if (string.IsNullOrWhiteSpace(outputDirectory))
        {
            throw new ArgumentException("Output directory must not be empty.", nameof(outputDirectory));
        }

        return Path.GetFullPath(outputDirectory);
    }
}
