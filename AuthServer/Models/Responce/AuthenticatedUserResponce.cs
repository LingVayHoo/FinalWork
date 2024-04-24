namespace AuthServer.Models.Responce
{
    public class AuthenticatedUserResponce
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
