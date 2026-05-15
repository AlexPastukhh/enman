namespace EnergyManagement.Tools.ClientConstants;

public sealed record ClientConstantsArtifacts(
    string ConstantsJson,
    string ErrorCodesJson)
{
    public const string ConstantsFileName = "constants.json";
    public const string ErrorCodesFileName = "errorcodes.json";
}
