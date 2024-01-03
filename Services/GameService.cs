using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Repositories.Interfaces;
using Proiect.Services.Interfaces;
using System.Collections.Generic;

namespace Proiect.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public GameService(IGameRepository gameRepository, IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public Game CreateGame(string player1_id)
        {
            var player1 = _userRepository.GetById(player1_id);
            if (player1 == null)
            {
                Console.WriteLine("Player1 invalid");
                return null;
            }

            return _gameRepository.CreateGame(player1);
        }

        public Game JoinGame(string gameId, string player2_id)
        {
            var player2 = _userRepository.GetById(player2_id);
            if (player2 == null)
            {
                Console.WriteLine("Player2 invalid");
                return null;
            }

            var game = _gameRepository.GetGameById(Guid.Parse(gameId));
            if (game == null)
            {
                Console.WriteLine("Game not found");
                return null;
            }

            if (game.Player1Id == player2_id)
            {
                Console.WriteLine("Player2 is the same as Player1");
                return null;
            }
            
            if (game.Player2 != null)
            {
                Console.WriteLine("Game already has 2 players");
                return null;
            }
            if (game.GameState != GameState.CREATED)
            {
                Console.WriteLine("Game already started");
                return null;
            }

            _gameRepository.ChangeGameState(game, GameState.MAP_CONFIG);
            _gameRepository.JoinGame(game, player2);
            return game;
        }

        // return null = invalid flow or the new state
        public Move? MakeAttack(Guid gameId, string userId, int row, int column)
        {
            var game = _gameRepository.GetGameById(gameId);
            if (game == null)
            {
                Console.WriteLine("Game not found");
                return null;
            }
            if (game.GameState != GameState.ACTIVE)
            {
                Console.WriteLine("Game not active");
                return null;
            }
            if (game.CurrentPlayer == 1 && game.Player1Id != userId)
            {
                Console.WriteLine("It's not your turn");
                return null;
            }
            if (game.CurrentPlayer == 2 && game.Player2Id != userId)
            {
                Console.WriteLine("It's not your turn");
                return null;
            }

            var move = _gameRepository.CheckIfMoveExist(gameId, userId, row, column);
            if (move == true)
            {
                Console.WriteLine("Already did that move.");
                return null;
            }
            var hit = _gameRepository.CheckIfHit(gameId, userId, row, column);
            var ans = _gameRepository.AddMove(gameId, userId, row, column, hit);

            _gameRepository.ChangeTurn(game);
            if (!_gameRepository.HasShipInGame(game, game.Player1Id))
            {
                _gameRepository.ChangeGameState(game, GameState.FINISHED);
                _gameRepository.SetWinner(game, 1);
            }
            if (!_gameRepository.HasShipInGame(game, game.Player2Id))
            {
                _gameRepository.ChangeGameState(game, GameState.FINISHED);
                _gameRepository.SetWinner(game, 2);
            }
            return ans;
        }
    
        public Task<List<Game>> GetAllGames()
        {
            return _gameRepository.GetAllGames();
        }

        public Game GetGame(Guid gameId, string userId)
        {
            return _gameRepository.GetGameStateForUser(gameId, userId);
        }
    
        public bool? PlaceShips(Guid gameId, string userId, List<PlaceShipDto> shipsDtos)
        {
            var game = _gameRepository.GetGameById(gameId);
            if (game == null)
            {
                Console.WriteLine("GameId is null");
                return false;
            }
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return false;
            }
            if (game.GameState != GameState.MAP_CONFIG)
            {
                Console.WriteLine("Game is not in MAP_CONFIG state");
                return false;
            }

            if (game.Player1Id != userId && game.Player2Id != userId)
            {
                Console.WriteLine("User is not part of the game");
                return false;
            }

            if (!_gameRepository.GetMyShips(game, userId).IsNullOrEmpty())
            {
                Console.WriteLine("User already placed the ships");
                return false;
            }

            bool[,] board = new bool[10, 10];
            foreach(var shipDto in shipsDtos)
            {
                if (shipDto.Row < 0 || shipDto.Row > 9 || shipDto.Column < 0 || shipDto.Column > 9)
                {
                    Console.WriteLine("Invalid coordinates");
                    return false;
                }
                if (shipDto.IsVertical)
                {
                    if (shipDto.Row + shipDto.Length > 10 || shipDto.Row + shipDto.Length < 0)
                    {
                        Console.WriteLine("Invalid coordinates");
                        return false;
                    }
                    for (int i = shipDto.Row; i < shipDto.Row + shipDto.Length; i++)
                    {
                        if (board[i, shipDto.Column])
                        {
                            Console.WriteLine("Invalid coordinates " + i + " " + shipDto.Column);
                            return false;
                        }
                        board[i, shipDto.Column] = true;
                    }
                }
                else
                {
                    if (shipDto.Column + shipDto.Length > 10 || shipDto.Column + shipDto.Length < 0)
                    {
                        Console.WriteLine("Invalid coordinates");
                        return false;
                    }
                    for (int i = shipDto.Column; i < shipDto.Column + shipDto.Length; i++)
                    {
                        if (board[shipDto.Row, i])
                        {
                            Console.WriteLine("Invalid coordinates " + shipDto.Row + "  " + i);
                            return false;
                        }
                        board[shipDto.Row, i] = true;
                    }
                }
            }
            // add ships to db
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (board[i,j])
                    {
                        _gameRepository.AddShip(game, user, i, j);
                    }
                }
            }
            if (_gameRepository.CheckIfAllShipsPlaced(game))
            {
                _gameRepository.ChangeGameState(game, GameState.ACTIVE);
            }   
            return true;
        }

        public async Task<bool> DeleteGame(Guid gameId)
        {
            return await _gameRepository.DeleteGame(gameId);
        }
    }
}   