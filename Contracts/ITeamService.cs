using WebTest.Models.Players;
using WebTest.Models.Teams;
using WebTest.ViewModels.Players;
using WebTest.ViewModels.Teams;

namespace WebTest.Contracts
{
    public interface ITeamService
    {
        Task<TeamVM> AddTeamAsync(AddTeamVM addTeamVM);
        Task<TeamVM> AddPlayerToTeamAsync(int teamId, int playerId);
        Task<TeamVM> RemovePlayerFromTeamAsync(int playerId, int teamId);
        Task<IEnumerable<TeamVM>> GetTeamsAsync();
        Task<TeamVM> GetTeamVMAsync(int teamId);
        Task<Team> GetTeamModelAsync(int teamId);
        Task<TeamVM> EditTeamAsync(int teamId, EditTeamVM editTeamVM);
        Task DeleteTeamAsync(int teamId);
    }
}
