using Proiect.Models;
using Proiect.Models.DTOs;


namespace Proiect.Services.Interfaces
{
    public interface IGameService
    {
        Game CreateGame(string player1_id);
        Game JoinGame(string gameId, string player2_id);
        Move? MakeAttack(Guid gameId, string userId, int row, int column);

        Task<List<Game>> GetAllGames();

        Game GetGame(Guid gameId, string userId);

        bool? PlaceShips(Guid gameId, string userId, List<PlaceShipDto> shipsDtos);

        Task<bool> DeleteGame(Guid gameId);
    }
}