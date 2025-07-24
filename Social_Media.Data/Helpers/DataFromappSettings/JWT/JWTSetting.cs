namespace Social_Media.Data.Helpers.DataFromappSettings.JWT
{
    public class JWTSetting
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int ExpiresOn { get; set; }
        public int RefreshTokenExpiredOn { get; set; }
    }
}
