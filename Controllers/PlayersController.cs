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
    }
}