using AuthServer.Models.Responce;

namespace FW_ASP.Net.Interfaces
{
    public interface IClaimsGetter
    {
        string GetClaim(string token, string claimName);
    }
}
