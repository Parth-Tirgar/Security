using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Model
{
    public class TokenProvider(IConfiguration configuration)
    {
        public string Create(int userId, string UserName)
        {
            string secretKey = configuration["Jwt:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, UserName),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Isuuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;

        }
    }
}
