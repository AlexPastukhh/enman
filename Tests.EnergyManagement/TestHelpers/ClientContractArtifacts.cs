using System.Text.Json.Nodes;

namespace Tests.EnergyManagement.TestHelpers;

public sealed class ClientContractArtifacts
{
    private ClientContractArtifacts(JsonObject constants, JsonObject errorCodes)
    {
        Constants = constants;
        ErrorCodes = errorCodes;
    }

    public JsonObject Constants { get; }
    public JsonObject ErrorCodes { get; }

    public static async Task<ClientContractArtifacts> LoadAsync(CancellationToken cancellationToken = default)
    {
        var sharedDirectory = Path.Combine(FindRepositoryRoot(), "Shared");

        return new ClientContractArtifacts(
            await ReadJsonObjectAsync(Path.Combine(sharedDirectory, "constants.json"), cancellationToken),
            await ReadJsonObjectAsync(Path.Combine(sharedDirectory, "errorcodes.json"), cancellationToken));
    }

    private static async Task<JsonObject> ReadJsonObjectAsync(
        string path,
        CancellationToken cancellationToken)
    {
        await using var stream = File.OpenRead(path);
        return await JsonNode.ParseAsync(stream, cancellationToken: cancellationToken) as JsonObject
            ?? throw new InvalidOperationException($"Client contract artifact is not a JSON object: {path}");
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "EnergyManagement.sln")) &&
                Directory.Exists(Path.Combine(directory.FullName, "Shared")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not find repository root for client contract artifacts.");
    }
}
