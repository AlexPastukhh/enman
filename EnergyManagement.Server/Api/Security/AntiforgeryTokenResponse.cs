using System.Text.Json.Serialization;

namespace EnergyManagement.Server.Api.Security;

public sealed record AntiforgeryTokenResponse(
    [property: JsonPropertyName("requestToken")] string RequestToken);
