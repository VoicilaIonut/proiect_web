using Proiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Proiect.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetById(string userId);

        User GetByUsername(string username);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Delete(User user);
        void CreateAddress(Address address);
    }
}
