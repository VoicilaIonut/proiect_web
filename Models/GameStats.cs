namespace Proiect.Models
{
    public class GameStats
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesLost { get; set; }
        public int GamesWon { get; set; }
        public int CurrentlyGamesPlaying { get; set; }
    }
}
