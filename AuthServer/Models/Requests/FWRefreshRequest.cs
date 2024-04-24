using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models.Requests
{
    public class FWRefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
