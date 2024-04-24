namespace FW_ASP.Net.Models
{
    public class AuthenticationConfiguration
    {
        public string AccessTokenSecret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
