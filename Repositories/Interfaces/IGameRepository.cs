using Proiect.Models;
using Proiect.Models.DTOs;

namespace Proiect.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Game CreateGame(User player1);
        void JoinGame(Game game, User player2);
        //void UpdateCell(Cell cell, CellState newState);

        void ChangeTurn(Game game);
        Task<List<Game>> GetAllGames();
        Game GetGameById(Guid gameId);

        Game GetGameStateForUser(Guid gameId, string userId);
        void ChangeGameState(Game game, GameState newState);

        public int GetTotalGames(string userId);

        public int GetWins(string userId);

        public int GetLosses(string userId);

        public int GetInProgress(string userId);

        public void AddShip(Game game, User user, int row, int column);

        bool CheckIfAllShipsPlaced(Game game);

        bool CheckIfMoveExist(Guid gameId, string userId, int row, int column);

        bool CheckIfHit(Guid gameId, string userId, int row, int column);

        Move AddMove(Guid gameId, string userId, int row, int column, bool hit);

        List<ShipCoord> GetMyShips(Game game, string userId);
        List<ShipCoord> GetOponentShips(Game game, string userId);

        bool HasShipInGame(Game game, string userId);

        void SetWinner(Game game, int winner);

        Task<bool> DeleteGame(Guid gameId);
    }
}
