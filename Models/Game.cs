using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public enum CellState
{
    Empty,
    Ship,
    Hit,
    Miss,
}

public enum GameState
{
    CREATED, // 0
    MAP_CONFIG, // 1
    ACTIVE, // 2
    FINISHED // 3
}

namespace Proiect.Models
{

    public class Game
    {
        public Guid GameId { get; set; }
        public GameState GameState { get; set; }
        public string Player1Id { get; set; }
        public User Player1 { get; set; }
        public string? Player2Id { get; set; }
        public User? Player2 { get; set; }
        public int CurrentPlayer { get; set; }

        public int Winner { get; set; }

        public ICollection<ShipCoord> ShipsCoord { get; set; }

        public ICollection<Move> Moves{ get; set; }
    }


    public class ShipCoord
    {   
        public Guid ShipId { get; set; }
        public Guid GameId { get; set; }
        public string UserId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public bool Hit { get; set; }
        // navigation
        [JsonIgnore]
        public Game Game { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }

    public class Move
    {
        public Guid MoveId { get; set; }
        public Guid GameId { get; set; }
        public string UserId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public char Result { get; set; } // 'Y' or 'N'

        // navigation
        [JsonIgnore]
        public Game Game { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
