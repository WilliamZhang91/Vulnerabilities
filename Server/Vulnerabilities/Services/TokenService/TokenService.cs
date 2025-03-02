using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vulnerabilities.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(int id)
        {
            var claims = new[]
            {
                new Claim("UserId", id.ToString())
            };

            var validIssuer = _configuration["Jwt:ValidIssuer"];
            var validAudience = _configuration["Jwt:ValidAudience"];
            var issuerSigningKey = _configuration["Jwt:IssuerSigningKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
