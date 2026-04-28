namespace Hospital.proj.Server.Utils
{
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public class EmailOptions
    {
        public int Port { get; set; }
        public string  Host{ get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public bool IsSslEnabled { get; set; }
        public string From { get; set; }
    }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
}
