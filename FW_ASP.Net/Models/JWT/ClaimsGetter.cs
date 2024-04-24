using AuthServer.Models.Responce;
using FW_ASP.Net.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FW_ASP.Net.Models.JWT
{
    public class ClaimsGetter : IClaimsGetter
    {         
        //private readonly AuthenticationConfiguration _authenticationConfiguration;

        public ClaimsGetter() //AuthenticationConfiguration authenticationConfiguration)
        {
            //_authenticationConfiguration = authenticationConfiguration;
        }

        public string GetClaim(string token, string claimName)
        {
            SecurityToken validatedToken;
            TokenValidationParameters p = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("_l3FUokeIa5RPkrMQ4ZGWHDA9F1qZt-n_EZWmJdm-LI5Saq5Apmp8Mgph3B-WlHDy0yyjMnBwIcOU2txD23CAk4XM-6k-IlP6CRMKXdvZNftW5MR-zTqqS28wb0dZFtN5waJv_X9MtP2zcJ1sMpr46mHwQAz4W_nfXzPlil_2fU")),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal claims = new JwtSecurityTokenHandler()
                .ValidateToken(token, p, out validatedToken);
            var t = claims.Claims;
            string result = string.Empty;
            foreach (var claim in t)
            {
                if (claim.Type == claimName)
                    result = claim.Value;
            }
            return result;
        }
    }
}
