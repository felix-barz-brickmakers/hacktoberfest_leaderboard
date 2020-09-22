using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly ILogger<LeaderboardController> _logger;

        public LeaderboardController(ILogger<LeaderboardController> logger, ILeaderboardService leaderboardService)
        {
            _logger = logger;
            _leaderboardService = leaderboardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardEntryModel>>> Get()
        {
            var result = new List<LeaderboardEntryModel>();
            await foreach (var entry in _leaderboardService.GenerateLeaderboard(new[] {"Skycoder42"}))
            {
                result.Add(entry);
            }
            return Ok(result);
        }
    }
}