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
            await _playerService.AddPlayerAsync(playerCreateDTO);
            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerVM>> GetAsync()
        {
            return await _playerService.GetPlayersAsync();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePlayerAsync(int id, EditPlayerDTO editPlayerDTO)
        {
            try
            {
                await _playerService.EditPlayerAsync(id, editPlayerDTO);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}