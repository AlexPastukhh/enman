using System.Text.Json;

namespace EnergyManagement.Tools.ClientConstants;

public sealed class ClientConstantsJsonSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public ClientConstantsArtifacts Serialize(ClientConstantsSnapshot snapshot)
    {
        return new ClientConstantsArtifacts(
            SerializeArtifact(snapshot.Constants),
            SerializeArtifact(snapshot.ErrorCodes));
    }

    public string SerializeArtifact<T>(T artifact)
        => JsonSerializer.Serialize(artifact, Options) + Environment.NewLine;
}
