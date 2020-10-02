using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Octokit;

namespace Backend.Services
{
    public interface ILeaderboardService
    {
        IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard();
        Task Update();
    }

    public class LeaderboardService : ILeaderboardService
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly ILogger<LeaderboardService> _logger;
        private readonly int _searchYear;
        private readonly IReadOnlyList<string> _usernames;

        private readonly object _leaderboardLock = new object();
        private List<LeaderboardEntryModel> _leaderboard = new List<LeaderboardEntryModel>();

        public LeaderboardService(IGitHubClient gitHubClient, IConfiguration configuration, ILogger<LeaderboardService> logger)
        {
            _gitHubClient = gitHubClient;
            _logger = logger;
            _searchYear = int.Parse(configuration["Year"]);
            _usernames = configuration.GetSection("Usernames").Get<List<string>>();
        }

        public IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard()
        {
            lock (_leaderboardLock)
            {
                return _leaderboard.ToAsyncEnumerable();
            }
        }

        public async Task Update()
        {
            var updatedBoard = await FetchPrs()
                .OrderByDescending(model => model.PrCount)
                .ToListAsync();
            lock (_leaderboardLock)
            {
                _leaderboard = updatedBoard;
            }
        }

        private async IAsyncEnumerable<LeaderboardEntryModel> FetchPrs()
        {
            foreach (var username in _usernames)
            {
                await AwaitRateLimit();
                
                var user = await _gitHubClient.User.Get(username);
                var result = await _gitHubClient.Search.SearchIssues(new SearchIssuesRequest
                {
                    Author = username,
                    Type = IssueTypeQualifier.PullRequest,
                    Is = new [] {IssueIsQualifier.Public},
                    Created = DateRange.Between(
                        new DateTimeOffset(_searchYear, 10, 1, 0, 0, 0, TimeSpan.Zero), 
                        new DateTimeOffset(_searchYear, 10, 31, 23, 59, 59, TimeSpan.Zero)),
                    Exclusions = new SearchIssuesRequestExclusions {
                        Labels = new []{"invalid"},
                    },
                    PerPage = 1,
                });
                yield return new LeaderboardEntryModel
                {
                    Name = user.Name,
                    PrCount = result.TotalCount,
                    Username = user.Login,
                    AvatarUrl = user.AvatarUrl,
                };
            }
        }

        private async Task AwaitRateLimit()
        {
            var rateLimit = _gitHubClient.GetLastApiInfo()?.RateLimit;
            _logger.LogDebug($"Remaining requests: {rateLimit?.Remaining.ToString() ?? "unknown"}");
            if (rateLimit == null || rateLimit.Remaining > 0)
            {
                return;
            }
            var dtOffset = rateLimit.Reset.Subtract(DateTimeOffset.Now).Add(TimeSpan.FromSeconds(1));
            if (dtOffset > TimeSpan.Zero)
            {
                _logger.LogInformation($"Sleeping for {dtOffset} until rate limit resets");
                await Task.Delay(dtOffset);
            }
        }
    }
}