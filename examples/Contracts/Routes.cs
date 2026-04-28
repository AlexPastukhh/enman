namespace Hospital.proj.Server.Contracts
{
    public static class Routes
    {
        public const string BaseSpaUrl = "https://localhost:5173";

        public const string AccountActivatedPage = BaseSpaUrl+AccountActivatedPath;
        public const string AccountActivatedPath = "/";
        public const string ChangePasswordPage = "https://localhost:5173/";

        public const string RegisterPage = BaseSpaUrl + RegisterPath;
        public const string RegisterPath = "/account/register";

        public const string ActivateAccountPage = BaseSpaUrl + ActivateAccountPath;
        public const string ActivateAccountPath = "/account/activate";

        public const string LoginPage = BaseSpaUrl + LoginPath;
        public const string LoginPath = "/account/login";

        public const string Smtp4Dev = "http://localhost:5000/";
        public const string Ethereal = "https://ethereal.email/login";
        public const string EmailDebug = "https://app.debugmail.io/app/login";

    }
}
