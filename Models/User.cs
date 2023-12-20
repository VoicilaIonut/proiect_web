using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Proiect.Models
{
    public class User : IdentityUser
    {
        //public Guid UserId { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }

        [JsonIgnore]
        public Address Address { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public ICollection<Game> Player1Games { get; set; }
        [JsonIgnore]
        public ICollection<Game> Player2Games { get; set; }

        public IEnumerable<ShipCoord> ShipsCoord { get; set; }

        public IEnumerable<Move> Moves { get; set; }
    }
}
