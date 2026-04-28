namespace Hospital.proj.Server.Contracts
{
    public class RegisterDto
    {
        public NameDto Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        #region emptyConstructor
#pragma warning disable CS8618
        public RegisterDto() { }
#pragma warning restore CS8618
        #endregion

    }

    public class NameDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        #region emptyConstructor
#pragma warning disable CS8618
        public NameDto() { }
#pragma warning restore CS8618
        #endregion
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        #region emptyConstructor
#pragma warning disable CS8618
        public LoginDto() { }
#pragma warning restore CS8618
        #endregion
    }


    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        #region emptyConstructor
#pragma warning disable CS8618
        public ChangePasswordDto() { }
#pragma warning restore CS8618
        #endregion
    }
}
