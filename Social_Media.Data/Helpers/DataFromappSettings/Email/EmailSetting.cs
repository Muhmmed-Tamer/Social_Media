namespace Social_Media.Data.Helpers.DataFromappSettings.Email
{
    public class EmailSetting
    {
        public string EmailFrom { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
