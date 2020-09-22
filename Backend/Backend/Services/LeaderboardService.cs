using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace Backend.Services
{
    public interface ILeaderboardService
    {
        Task AddUser(string username);
        IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard();
    }

    public class LeaderboardService : ILeaderboardService
    {
        private readonly IGitHubClient _gitHubClient;

        private readonly int _searchYear;

        private readonly ISet<string> _usernames = new HashSet<string>();

        public LeaderboardService(IGitHubClient gitHubClient, IConfiguration configuration)
        {
            _gitHubClient = gitHubClient;
            _searchYear = int.Parse(configuration["Year"]);
        }

        public Task AddUser(string username)
        {
            _usernames.Add(username);
            return Task.CompletedTask;
        }

        public IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard()
        {
            return FetchPrs().OrderByDescending(model => model.PrCount);
        }

        private async IAsyncEnumerable<LeaderboardEntryModel> FetchPrs()
        {
            foreach (var username in _usernames)
            {
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
                    PrCount = result.TotalCount
                };
            }
        }
    }
}