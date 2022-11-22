using Microsoft.AspNetCore.Mvc;
using WebTest.Contracts;
using WebTest.ViewModels.Players;

namespace WebTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayerService _playerService;

        public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(PlayerCreateDTO playerCreateDTO)
        {
            try
            {
                await _playerService.AddPlayerAsync(playerCreateDTO);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }           
        }

        [HttpGet]
        public async Task<ActionResult<List<PlayerVM>>> GetAsync()
        {
            try
            {
                IEnumerable<PlayerVM> players =  await _playerService.GetPlayersAsync();
                return players.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PlayerVM>> UpdatePlayerAsync(int id, EditPlayerDTO editPlayerDTO)
        {
            try
            {
                PlayerVM player = await _playerService.EditPlayerAsync(id, editPlayerDTO);
                return player;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayerAsync(int id)
        {
            try
            {
                await _playerService.DeletePlayerAsync(id);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}