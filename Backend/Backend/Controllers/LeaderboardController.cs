using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _leaderboardService.GenerateLeaderboard().ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<NoContentResult> Register([FromForm] string username)
        {
            await _leaderboardService.AddUser(username);
            return NoContent();
        }
    }
}