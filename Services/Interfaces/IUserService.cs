using Proiect.Models;
using Proiect.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(string userId);
        IEnumerable<User> GetAllUsers();
        User CreateUser(CreateUserDto userDto);
        bool DeleteUser(string userId);
        Address CreateAddress(Address address);

        User Register(RegisterDto registerDto);
        User Authenticate(LoginDto loginDto);

        GameStats GetGameStats(string userId);
    }
}
