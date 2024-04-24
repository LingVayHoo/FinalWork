using Microsoft.AspNetCore.Identity;

namespace AuthServer.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string Role { get; set; } = string.Empty;
    }
}
