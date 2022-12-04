using WebTest.Models.Players;
using WebTest.ViewModels.Players;

namespace WebTest.Contracts
{
    public interface IPlayerService
    {
        Task<PlayerVM> AddPlayerAsync(PlayerCreateDTO playerCreateDTO);
        Task<IEnumerable<PlayerVM>> GetPlayersAsync();
        Task<PlayerVM> GetPlayerVMAsync(int id);
        Task<Player> GetPlayerModelAsync(int playerId);
        Task<PlayerVM> EditPlayerAsync(int playerId, EditPlayerDTO editPlayerDTO);
        Task DeletePlayerAsync(int playerId);
    }
}
