using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proiect.Services.Interfaces
{
    public interface IJwtTokenService
    {
        public string GenerateJwtToken(IdentityUser user, IList<string> roles);

        /*
        public ClaimsPrincipal? DecryptJwtToken(string token);
        public string? GetUserIdFromToken(string token);
        */

    }
}
