namespace EnergyManagement.Server.Infrastructure.Email;

public sealed class SmtpEmailOptions
{
    public const string SectionName = "Email:Smtp";

    public string Host { get; init; } = string.Empty;

    public int Port { get; init; } = 587;

    public bool EnableSsl { get; init; } = true;

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string From { get; init; } = string.Empty;
}
