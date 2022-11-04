using Microsoft.EntityFrameworkCore;
using WebTest.Context;
using WebTest.Contracts;
using WebTest.Models.Players;
using WebTest.ViewModels.Players;

namespace WebTest.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FifaAppContext _appContext;
        public PlayerService(FifaAppContext fifaAppContext)
        {
            _appContext = fifaAppContext;
        }

        public async Task AddPlayerAsync(PlayerCreateDTO playerCreateDTO)
        {
            Player player = new Player
            {
                Name = playerCreateDTO.Name,
                Surname = playerCreateDTO.Surname,
                Nationality = playerCreateDTO.Nationality,
                Overall = playerCreateDTO.Overall
            };

            _appContext.Add(player);
            await _appContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerVM>> GetPlayersAsync()
        {
            IEnumerable<PlayerVM> players = await _appContext.Players.Select(p => new PlayerVM
            {
                Name = p.Name,
                Surname = p.Surname,
                Nationality = p.Nationality,
                Overall = p.Overall
            }).ToListAsync();

            return players;
        }
    }
}
