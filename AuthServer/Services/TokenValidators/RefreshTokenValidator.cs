using AuthServer.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthServer.Services.TokenValidators
{
    public class RefreshTokenValidator(AuthenticationConfiguration authenticationConfiguration)
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration = authenticationConfiguration;

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_authenticationConfiguration.RefreshTokenSecret)),
                ValidIssuer = _authenticationConfiguration.Issuer,
                ValidAudience = _authenticationConfiguration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new();
            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
