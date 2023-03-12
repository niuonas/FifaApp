using Microsoft.EntityFrameworkCore;
using WebTest.Context;
using WebTest.Contracts;
using WebTest.Models.Players;
using WebTest.ViewModels.Players;

#nullable disable
namespace WebTest.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FifaAppContext _appContext;
        public PlayerService(FifaAppContext fifaAppContext)
        {
            _appContext = fifaAppContext;
        }

        public async Task<PlayerVM> AddPlayerAsync(PlayerCreateDTO playerCreateDTO)
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
            return await GetPlayerVMAsync(player.Id);
        }

        public async Task<PlayerVM> FastAddAsync()
        {
            Random random = new Random();
            string[] names = { "Max", "Arnold", "Luca", "Vinicius", "Cristiano", "Lionel" };
            string[] surnames = { "Modric", "Becker", "Beckham", "Didier", "Pogba", "Deschamps" };
            string[] nationalities = { "Romania", "Germany" , "France", "Italy", "Japan", "China", "Argentina", "Brazil"};

            Player player = new Player
            {
                Name = names[random.Next(0, names.Length - 1)],
                Surname = surnames[random.Next(0, surnames.Length - 1)],
                Nationality = nationalities[random.Next(0, nationalities.Length - 1)],
                Overall = random.Next(50, 99)
            };

            _appContext.Add(player);
            await _appContext.SaveChangesAsync();
            return await GetPlayerVMAsync(player.Id);
        }

        public async Task<IEnumerable<PlayerVM>> GetPlayersAsync()
        {
            IEnumerable<PlayerVM> players = await _appContext.Players.Select(p => new PlayerVM
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Nationality = p.Nationality,
                Overall = p.Overall,
                Team = p.Team.Name ?? "Free contract"
            }).ToListAsync();

            return players;
        }

        public async Task<PlayerVM> GetPlayerVMAsync(int id)
        {
            return await _appContext.Players.Where(x => x.Id == id).Select(p => new PlayerVM
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Nationality = p.Nationality,
                Overall = p.Overall,
                Team = p.Team.Name ?? "Free Contract"
            }).FirstOrDefaultAsync();
        }

        public async Task<Player> GetPlayerModelAsync(int playerId)
        {
            Player player = await _appContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);

            if (player != null)
            {
                return player;
            }
            else
            {
                throw new ArgumentException("No player exists with this id");
            }
        }

        public async Task<PlayerVM> EditPlayerAsync(int playerId, EditPlayerDTO editPlayerDTO)
        {
            Player playerToBeEdited = await _appContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);

            if (playerToBeEdited != null)
            {
                playerToBeEdited.Name = editPlayerDTO.Name;
                playerToBeEdited.Surname = editPlayerDTO.Surname;
                playerToBeEdited.Nationality = editPlayerDTO.Nationality;
                playerToBeEdited.Overall = editPlayerDTO.Overall;
            }else
            {
                throw new ArgumentException("No player exists with this id!");
            }

            await _appContext.SaveChangesAsync();

            return await GetPlayerVMAsync(playerId);
        }

        public async Task DeletePlayerAsync(int playerId)
        {
            Player playerToBeDeleted = await _appContext.Players.FirstOrDefaultAsync(x => x.Id == playerId);

            if (playerToBeDeleted != null)
            {
                _appContext.Players.Remove(playerToBeDeleted);
            }
            else
            {
                throw new ArgumentException("No player exists with this id!");
            }

            await _appContext.SaveChangesAsync();
        }
    }
}
