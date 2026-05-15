namespace EnergyManagement.Server.Api.Routes;

public static class AuthRoutes
{
    public const string Controller = "api/auth";
    public const string RegisterIndividual = "registerIndividual";
    public const string RegisterIndividualPath = $"{Controller}/{RegisterIndividual}";
    public const string ProvideIndividualClientsData = "provideIndividualClientsData";
    public const string ProvideIndividualClientsDataPath = $"{Controller}/{ProvideIndividualClientsData}";
    public const string Login = "login";
    public const string LoginPath = $"{Controller}/{Login}";
    public const string GetUser = "getUser";
    public const string GetUserPath = $"{Controller}/{GetUser}";
}
