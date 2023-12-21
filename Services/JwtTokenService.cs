using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Proiect.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proiect.Services
{
    public class JwtTokenService : IJwtTokenService
    {

        public string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_secret_key_that_is_at_least_32_characters_long");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            foreach (var role in roles)
            {
                Console.WriteLine(role);
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        /*
         public ClaimsPrincipal? DecryptJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_secret_key_that_is_at_least_32_characters_long");

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        } 
         
        public string? GetUserIdFromToken(string token)
        {
            var principal = DecryptJwtToken(token);

            if (principal == null)
            {
                Console.WriteLine("Token could not be decrypted");
                return null;
            }

            var claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                Console.WriteLine("Token does not contain a NameIdentifier claim");
                return null;
            }

            if (!Guid.TryParse(claim.Value, out var userId))
            {
                Console.WriteLine("NameIdentifier claim could not be parsed to a Guid");
                return null;
            }

            return userId.ToString();
        }
        
         
         */
    }
}
