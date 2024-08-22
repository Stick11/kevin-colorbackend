using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ColorBackend.Util
{
    public class TokenService
    {
        private static readonly string? _issuer;
        private static readonly string? _audience;
        private static readonly string? _secretKey;

        static TokenService()
        {

            _issuer = Environment.GetEnvironmentVariable("VE_JWT_ISSUER_TOKEN");
            _audience = Environment.GetEnvironmentVariable("VE_JWT_AUDIENCE_TOKEN");
            _secretKey = Environment.GetEnvironmentVariable("VE_JWT_SECRET_KEY");
        }

        public static string GenerateToken(string userId)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(4), 
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
