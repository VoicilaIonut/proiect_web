using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Services;
using Proiect.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proiect.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IJwtTokenService _jwtTokenService;

        public GameController(IGameService gameService, IJwtTokenService jwtTokenService)
        {
            _gameService = gameService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public IActionResult CreateGame()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("user not found");
            }

            Console.WriteLine(userId);
            var game = _gameService.CreateGame(userId);
            if (game == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(game);
        }
        [HttpGet("{gameId}")]
        public async Task<ActionResult<Game>> GetGame(Guid gameId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("user not found");
            }

            var game = _gameService.GetGame(gameId, userId);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost("join/{gameId}")]
        public IActionResult JoinGame(Guid gameId, [FromBody] JoinGameDto gameDto)
        {
            Console.WriteLine(gameDto.UserId);
            Console.WriteLine(gameId.ToString());
            var game = _gameService.JoinGame(gameId.ToString(), gameDto.UserId);
            if (game == null) { return NotFound(); }
            return Ok(game);
        }

        [HttpPost("{gameId}/move")]
        public IActionResult MakeAttack(Guid gameId, [FromBody] MoveDto move)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("user not found");
            }

            var ans = _gameService.MakeAttack(gameId, userId, move.Row, move.Column);
            if (ans == null)
            {
                return BadRequest("Invalid move! Maybe it isn't your turn?");
            }
            Console.WriteLine("Hit: " + ans.Result);
            return Ok(ans);
        }

        [HttpGet("games")]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            var games = await _gameService.GetAllGames();
            return Ok(games.ToList());
        }

        [HttpPatch("{gameId}")]
        public IActionResult PlaceShips(Guid gameId, [FromBody] List<PlaceShipDto> ships)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound("User not found.");
            }
            if (ships.Count != 2)
            {
                return BadRequest("Invalid number of ships.");
            }

            var res = _gameService.PlaceShips(gameId, userId, ships);
            if (res == null) { return NotFound(); }

            return Ok(res == true ? "Succes" : "Invalid");
        }

        [HttpDelete("{gameId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            var result = await _gameService.DeleteGame(gameId);

            if (!result)
            {
                return NotFound();
            }

            return Ok("Succes!");
        }
    }
}
