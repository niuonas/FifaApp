using Microsoft.AspNetCore.Mvc;
using WebTest.Contracts;
using WebTest.Models.Teams;
using WebTest.ViewModels.Players;
using WebTest.ViewModels.Teams;

namespace WebTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<Team> _logger;

        public TeamsController(ITeamService teamService, ILogger<Team> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<TeamVM>> AddTeamAsync(AddTeamVM addTeamVM)
        {
            try
            {
                return await _teamService.AddTeamAsync(addTeamVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamVM>>> GetTeamsAsync()
        {
            try
            {
                IEnumerable<TeamVM> teams = await _teamService.GetTeamsAsync();
                return teams.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVM>> GetTeamAsync(int id)
        {
            try
            {
                return await _teamService.GetTeamVMAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("addPlayer/{teamId}")]
        public async Task<ActionResult<TeamVM>> AddPlayerToTeamAsync(int teamId, PlayerVM playerVM)
        {
            try
            {
                return await _teamService.AddPlayerToTeamAsync(teamId, playerVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("removePlayer/{playerId}/{teamId}")]
        public async Task<ActionResult<TeamVM>> RemovePlayerFromTeamAsync(int playerId, int teamId)
        {
            try
            {
                return await _teamService.RemovePlayerFromTeamAsync(playerId, teamId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("id")]
        public async Task<ActionResult<TeamVM>> EditTeamAsync(int teamId, EditTeamVM editTeamVM)
        {
            try
            {
                return await _teamService.EditTeamAsync(teamId, editTeamVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTeamAsync(int teamId)
        {
            try
            {
                await _teamService.DeleteTeamAsync(teamId);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
