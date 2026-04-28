namespace Hospital.proj.Server.Utils
{
    public interface IVerificationLinkFactory
    {
        string CreateLink(string action, string controller, string code, string scheme = "https");
    }
}