using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Repositories.Interfaces;
using Proiect.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public UserService(IUserRepository userRepository, IGameRepository gameRepository)
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository; 
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User CreateUser(CreateUserDto userDto)
        {


            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                PasswordHash = userDto.Password
            };

            _userRepository.Add(user);
            var address = new Address
            {
                UserId = user.Id,
                AddressId = Guid.NewGuid(),
                Street = userDto.Street,
                City = userDto.City,
                Country = userDto.Country,
               
            };
            _userRepository.CreateAddress(address);
            return user;
        }

        public bool DeleteUser(string userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return false;
            }
            _userRepository.Delete(user);
            return true;
        }

        public Address CreateAddress(Address address)
        {
            _userRepository.CreateAddress(address);
            return address;
        }

        public User Authenticate(LoginDto loginDto)
        {
            var user = _userRepository.GetByUsername(loginDto.Username);
            if (user == null)
            {
                Console.WriteLine("User not found");
                return null;
            }
            if (user.PasswordHash != loginDto.Password)
            {
                return null;
            }
            return user;
        }

        public User Register(RegisterDto registerDto)
        {
            var user = _userRepository.GetByUsername(registerDto.Username);
            if (user != null)
            {
                Console.WriteLine("User already exists");
                return null;
            }
            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password
            };
            _userRepository.Add(newUser);
            var address = new Address
            {
                UserId = newUser.Id,
                AddressId = Guid.NewGuid(),
                Street = registerDto.Street,
                City = registerDto.City,
                Country = registerDto.Country
            };
            _userRepository.CreateAddress(address);
            return newUser;
        }

        public GameStats GetGameStats(string userId)
        {
            var gamesPlayed = _gameRepository.GetTotalGames(userId);
            var gamesWon = _gameRepository.GetWins(userId);
            var gamesLost = _gameRepository.GetLosses(userId);
            var currentlyGamesPlaying = _gameRepository.GetInProgress(userId);
            var user = _userRepository.GetById(userId);

            return new GameStats
            {
                UserId = userId,
                Email = user.Email,
                GamesPlayed = gamesPlayed,
                GamesWon = gamesWon,
                GamesLost = gamesLost,
                CurrentlyGamesPlaying = currentlyGamesPlaying
            };
        }
    }
}
