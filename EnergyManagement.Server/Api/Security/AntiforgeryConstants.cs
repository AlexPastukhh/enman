namespace EnergyManagement.Server.Api.Security;

public static class AntiforgeryConstants
{
    public const string HeaderName = "X-CSRF-TOKEN";
    public const string FailureCode = "security.antiforgery.validation.failed";
    public const string FailureType = "https://enman.local/problems/security/antiforgery-validation-failed";
    public const string FailureTitle = "Antiforgery validation failed";
    public const string FailureDetail = "Antiforgery token is missing, expired, or invalid.";
}
