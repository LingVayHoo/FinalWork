namespace AuthServer.Models.Auth
{
    public class AuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; } = string.Empty;
        public double AccessTokenExpirationMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double RefreshTokenExpirationMinutes { get; set; }
        public string RefreshTokenSecret { get; set; } = string.Empty;
    }
}
