using WebTest.Context;
using WebTest.Contracts;
using WebTest.Models.Players;
using WebTest.Models.Teams;
using WebTest.ViewModels.Teams;
using WebTest.ViewModels.Players;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace WebTest.Services
{
    public class TeamService : ITeamService
    {
        private readonly FifaAppContext _fifaAppContext;
        private readonly IPlayerService _playerService;

        public TeamService(FifaAppContext fifaAppContext, IPlayerService playerService)
        {
            _fifaAppContext = fifaAppContext;
            _playerService = playerService;
        }
        public async Task<TeamVM> AddTeamAsync(AddTeamVM addTeamVM)
        {
            Team newTeam = new Team
            {
                Name = addTeamVM.Name
            };

            _fifaAppContext.Teams.Add(newTeam);
            await _fifaAppContext.SaveChangesAsync();

            return new TeamVM
            {
                Id = newTeam.Id,
                Name = newTeam.Name
            };
        }

        public async Task<TeamVM> AddPlayerToTeamAsync(int playerId, int teamId)
        {
            Player player = await _playerService.GetPlayerModelAsync(playerId);
            Team team = await GetTeamModelAsync(teamId);

            if (team.Players.Contains(player))
            {
                throw new Exception($"Player {player.Name} already exists in team {team.Name}");
            }

            team.Players.Add(player);

            await _fifaAppContext.SaveChangesAsync();

            return await GetTeamVMAsync(teamId);
        }

        public async Task<TeamVM> RemovePlayerFromTeamAsync(int playerId, int teamId)
        {
            Player player = await _playerService.GetPlayerModelAsync(playerId);
            Team team = await _fifaAppContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

            if (!team.Players.Contains(player))
            {
                throw new Exception($"Team {team.Name} has no player with this name {player.Name}");
            }

            team.Players.Remove(player);
            await _fifaAppContext.SaveChangesAsync();

            return await GetTeamVMAsync(teamId);
        }

        public async Task<IEnumerable<TeamVM>> GetTeamsAsync()
        {
            IEnumerable<TeamVM> teams = _fifaAppContext.Teams.Select(x => new TeamVM
            {
                Id = x.Id,
                Name = x.Name,
                Players = x.Players.Select(x => new PlayerVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Nationality = x.Nationality,
                    Overall = x.Overall,
                    Surname = x.Surname
                }).ToList()
            });

            return teams;
        }

        public async Task<TeamVM> GetTeamVMAsync(int teamId)
        {
            TeamVM teamVm = await _fifaAppContext.Teams.Select(t => new TeamVM
            {
                Id = t.Id,
                Name = t.Name,
                Players = t.Players.Select(x => new PlayerVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Nationality = x.Nationality,
                    Overall = x.Overall,
                    Surname = x.Surname
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == teamId);

            return teamVm;
        }

        public async Task<Team> GetTeamModelAsync(int teamId)
        {
            Team team = await _fifaAppContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

            if (team != null)
            {
                return team;
            }
            else
            {
                throw new Exception("No team exists with this id");
            }
        }

        public async Task<TeamVM> EditTeamAsync(int teamId, EditTeamVM editTeamVM)
        {
            Team team = await _fifaAppContext.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

            team.Name = editTeamVM.Name;

            await _fifaAppContext.SaveChangesAsync();

            return await GetTeamVMAsync(teamId);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            Team team = await _fifaAppContext.Teams.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == teamId);
            team.Players = new List<Player>();
            _fifaAppContext.Teams.Remove(team);

            await _fifaAppContext.SaveChangesAsync();
        }
    }
}
