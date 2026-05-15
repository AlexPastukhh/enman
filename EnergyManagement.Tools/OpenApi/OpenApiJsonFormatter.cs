using System.Text.Json;

namespace EnergyManagement.Tools.OpenApi;

public sealed class OpenApiJsonFormatter
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true
    };

    public string Format(string json)
    {
        using var document = JsonDocument.Parse(json);
        return NormalizeLineEndings(JsonSerializer.Serialize(document.RootElement, Options)) + "\n";
    }

    private static string NormalizeLineEndings(string content)
        => content.Replace("\r\n", "\n", StringComparison.Ordinal);
}
