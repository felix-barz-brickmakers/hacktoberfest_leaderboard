using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.Extensions.Configuration;
using Octokit;

namespace Backend.Services
{
    public interface ILeaderboardService
    {
        IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard(IEnumerable<string> usernames);
    }

    public class LeaderboardService : ILeaderboardService
    {
        private readonly IGitHubClient _gitHubClient;

        private readonly int _searchYear;

        public LeaderboardService(IGitHubClient gitHubClient, IConfiguration configuration)
        {
            _gitHubClient = gitHubClient;
            _searchYear = int.Parse(configuration["Year"]);
        }

        public async IAsyncEnumerable<LeaderboardEntryModel> GenerateLeaderboard(IEnumerable<string> usernames)
        {
            foreach (var username in usernames)
            {
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
                    Name = username,
                    Email = username,
                    PrCount = result.TotalCount
                };
            }
        }
    }
}