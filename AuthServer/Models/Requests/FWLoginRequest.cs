using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models.Requests
{
    public class FWLoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        //public string? ReturnUrl { get; set; }
    }
}
