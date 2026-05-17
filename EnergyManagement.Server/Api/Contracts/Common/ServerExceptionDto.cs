using System.Text.Json.Serialization;

namespace EnergyManagement.Server.Api.Contracts.Common;

public sealed class ServerExceptionDto
{
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("stackTrace")]
    public string StackTrace { get; set; }

    public ServerExceptionDto(string message, string? stackTrace)
    {
        Message = message;
        StackTrace = stackTrace ?? "no stack trace available";
    }

    public static ServerExceptionDto FromException(Exception ex)
        => new(ex.Message, ex.StackTrace);
}
