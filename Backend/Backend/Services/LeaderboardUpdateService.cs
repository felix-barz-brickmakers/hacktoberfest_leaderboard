using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Backend.Services
{
    public class LeaderboardUpdateService : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        private readonly ILogger<LeaderboardUpdateService> _logger;
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardUpdateService(ILogger<LeaderboardUpdateService> logger, IGitHubClient gitHubClient, ILeaderboardService leaderboardService)
        {
            _logger = logger;
            _leaderboardService = leaderboardService;
            _timer = new Timer(SyncPRs, null, Timeout.Infinite, 0);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting GitHub PR Sync");
            _timer.Change(TimeSpan.Zero, TimeSpan.FromHours(4));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stopping GitHub PR Sync");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async void SyncPRs(object state)
        {
            try
            {
                _logger.LogInformation("Updating Leaderboard...");
                await _leaderboardService.Update();
                _logger.LogInformation("Leaderboard updated!");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Failed to update leaderboard from GitHub");
            }
        }
    }
}