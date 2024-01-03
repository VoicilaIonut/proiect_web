using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Repositories.Interfaces;

namespace Proiect.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public Game CreateGame(User player1)
        {
            var game = new Game
            {
                GameId = Guid.NewGuid(),
                Player1Id = player1.Id,
                Player2Id = null,
                CurrentPlayer = 1,
                GameState = GameState.CREATED
            };

            _context.Games.Add(game);
            _context.SaveChanges();

            return game;
        }

        public void JoinGame(Game game, User player2)
        {
            game.Player2 = player2;
            _context.SaveChanges();
        }

        public void ChangeGameState(Game game, GameState newState)
        {
            game.GameState = newState;
            _context.SaveChanges();
        }

        /*public void UpdateCell(Cell cell, CellState newState)
        {
            cell.State = newState;
            _context.SaveChanges();
        }*/


        public void ChangeTurn(Game game)
        {
            game.CurrentPlayer = game.CurrentPlayer == 1 ? 2 : 1;
            _context.SaveChanges();
        }

        public async Task<List<Game>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }

        public Game GetGameById(Guid gameId)
        {
            return _context.Games.Find(gameId);
        }

        public int GetTotalGames(string userId)
        {
            return _context.Games.Count(g => g.Player1Id == userId || g.Player2Id == userId);
        }

        public int GetWins(string userId)
        {
            return _context.Games.Count(g => (g.Winner == 1 && g.Player1Id == userId) || (g.Winner == 2 && g.Player2Id == userId));
        }

        public int GetLosses(string userId)
        {
            return _context.Games.Count(g => (g.Winner == 1 && g.Player2Id == userId) || (g.Winner == 2 && g.Player1Id == userId));
        }

        public int GetInProgress(string userId)
        {
            return _context.Games.Count(g => (g.Player1Id == userId || g.Player2Id == userId) && g.GameState == GameState.ACTIVE);
        }

        /*public void ChangeCellState(Cell cell, CellState state)
        {
            cell.State = state;
            _context.SaveChanges();
        }*/

        public void AddShip(Game game, User user, int row, int column)
        {
            var ship = new ShipCoord
            {
                ShipId = Guid.NewGuid(),
                GameId = game.GameId,
                UserId = user.Id,
                Row = row,
                Column = column,
            };
            _context.ShipsCoord.Add(ship);
            _context.SaveChanges();
        }

        public Game GetGameStateForUser(Guid gameId, string userId)
        {
            var game = _context.Games
               .Include(g => g.Moves)
               .Include(g => g.ShipsCoord)
               .Select(g => new Game
               {
                   // Copy the properties of the game
                   GameId = g.GameId,
                   Player1Id = g.Player1Id,
                   Player2Id = g.Player2Id,
                   CurrentPlayer = g.CurrentPlayer,
                   GameState = g.GameState,
                   Moves = g.Moves,
                   // Filter the ships
                   ShipsCoord = g.ShipsCoord.Where(s => s.UserId == userId && s.GameId == gameId).ToList()
               })
               .FirstOrDefault(g => g.GameId == gameId);
            if (game.ShipsCoord == null)
            {
                Console.WriteLine("No ships found");
            } else {
                foreach (ShipCoord ship in game.ShipsCoord) {
                    Console.WriteLine(string.Format("Ship: {0} {1} {2} {3} {4}", ship.ShipId, ship.GameId, ship.UserId, ship.Row, ship.Column));
                }
            }
            return game;

        }

        public bool CheckIfAllShipsPlaced(Game game)
        {
            var player1Ships = _context.ShipsCoord.Any(s => s.GameId == game.GameId && s.UserId == game.Player1Id);
            var player2Ships = _context.ShipsCoord.Any(s => s.GameId == game.GameId && s.UserId == game.Player2Id);

            return player1Ships && player2Ships;
        }

        public bool CheckIfMoveExist(Guid gameId, string userId, int row, int column)
        {
            return _context.Moves.Any(m => m.GameId == gameId && m.UserId == userId && m.Row == row && m.Column == column);
        }

        public List<ShipCoord> GetMyShips(Game game, string userId)
        {
            return _context.ShipsCoord.Where(s => s.GameId == game.GameId && s.UserId == userId).ToList();
        }

        public List<ShipCoord> GetOponentShips(Game game, string userId)
        {
            return _context.ShipsCoord.Where(s => s.GameId == game.GameId && s.UserId != userId).ToList();
        }


        public bool CheckIfHit(Guid gameId, string userId, int row, int column)
        {
            return _context.ShipsCoord.Any(s => s.GameId == gameId && s.UserId != userId && s.Row == row && s.Column == column);
        }

        public Move AddMove(Guid gameId, string userId, int row, int column, bool hit)
        {
            var move = new Move
            {
                MoveId = Guid.NewGuid(),
                GameId = gameId,
                UserId = userId,
                Result = hit ? 'Y' : 'N',
                Row = row,
                Column = column
            };
            _context.Moves.Add(move);
            if (hit)
            {
                var ship = _context.ShipsCoord.FirstOrDefault(s => s.GameId == gameId && s.UserId != userId.ToString() && s.Row == row && s.Column == column);
                ship.Hit = true;
                _context.ShipsCoord.Update(ship);
            }
            _context.SaveChanges();
            return move;
        }

        public bool HasShipInGame(Game game, string userId)
        {
            return _context.ShipsCoord.Any(s => s.GameId == game.GameId && s.UserId == userId && s.Hit == false);
        }

        public void SetWinner(Game game, int winner)
        {
            game.Winner = winner;
            _context.SaveChanges();
        }

        public async Task<bool> DeleteGame(Guid gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
            {
                return false;
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}