using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Proiect.Models.DTOs;
using Proiect.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Proiect.Services;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using Proiect.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Proiect.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AccountController(IUserService userService, UserManager<User> userManager, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _jwtTokenService.GenerateJwtToken(user, roles);
                return Ok(new { accessToken = token });
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // Assign the default role to the new user
                await _userManager.AddToRoleAsync(user, "User");
                Console.WriteLine("User created a new account with password and role=User.");
                return Ok();
            }

            return BadRequest(result);
        }

        [HttpGet("details/me")]
        public async Task<IActionResult> GetMyDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }
            GameStats GameStats = _userService.GetGameStats(userId);
            GameStats.Roles = await _userManager.GetRolesAsync(user);
            return Ok(GameStats);
        }
    }
}
