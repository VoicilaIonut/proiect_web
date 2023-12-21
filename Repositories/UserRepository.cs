using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;
using Proiect.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User GetById(string userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Address).ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }


        public void CreateAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

    }
}