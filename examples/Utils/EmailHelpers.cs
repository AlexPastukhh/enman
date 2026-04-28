namespace Hospital.proj.Server.Utils
{
    public static class EmailHelpers
    {
        public const string ChangePasswordMessageText =
            "To reset password click on";

        public const string ChangePasswordSubject =
            "Change password";

        public const string ChangePasswordLinkText =
            "link";

        public static string ChangePasswordMessage(string changePwdLink)=>
            $"<span >" +
                $"{ChangePasswordMessageText} "+
                $"<a href=\"{changePwdLink}\">" +
                    $"{ChangePasswordLinkText}" +
                $"</a>" +
            $"</span>";


        public const string ActivateAccountMessageText =
            "To activate your account click on";

        public const string ActivateAccountSubject =
            "Activate your account";

        public const string ActivateAccountLinkText =
            "linkdddddddddd";

        public static string ActivateAccountMessage(string activateAccLink) =>
            $"<span class=\"message\">" +
                $"{ActivateAccountMessageText} " +
                $"<a class=\"activation\" href=\"{activateAccLink}\">" +
                    $"{ActivateAccountLinkText}" +
                $"</a>"+
            $"</span>";

        


    }
}
