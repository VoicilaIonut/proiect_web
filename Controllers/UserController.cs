using Proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Proiect.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Proiect.Services.Interfaces;


namespace Proiect.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(Guid userId)
        {
            var user = _userService.GetUserById(userId.ToString());
            Console.WriteLine(user.UserName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            foreach(var user in users)
            {
                Console.WriteLine(user.UserName);
            }
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto)
        {
            var user = _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
        }


        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            var existingUser = _userService.GetUserById(userId.ToString());
            if (existingUser == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(userId.ToString());

            return NoContent();
        }
    }
}