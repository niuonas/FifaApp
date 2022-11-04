using WebTest.ViewModels.Players;

namespace WebTest.Contracts
{
    public interface IPlayerService
    {
        Task AddPlayerAsync(PlayerCreateDTO playerCreateDTO);
        Task<IEnumerable<PlayerVM>> GetPlayersAsync();
    }
}
