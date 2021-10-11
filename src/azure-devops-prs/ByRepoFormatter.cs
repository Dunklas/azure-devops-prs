using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevOpsPrs
{
    public class ByRepoFormatter
        : PullRequestsFormatter
    {
        public string Format(List<PullRequest> pullRequests)
        {
            var prsByRepo = pullRequests
                .OrderBy(pr => pr.Repository)
                .GroupBy(
                    pr => pr.Repository,
                    pr => pr,
                    (repo, pr) => new { Repository = repo, PullRequests = pr.ToList()}
                );

            var formattedRepos = new List<string>();
            foreach (var repoAndPrs in prsByRepo)
            {
                var sb = new StringBuilder();
                sb.Append($" {repoAndPrs.Repository}{Environment.NewLine}{Environment.NewLine}");
                var formattedPrs = repoAndPrs.PullRequests
                    .Select(pr => String.Format("{0,-6} {1,-30} {2,-30}", $" #{pr.Id}", pr.Title, pr.Url))
                    .ToList();
                sb.Append(String.Join("\n", formattedPrs));
                formattedRepos.Add(sb.ToString());
            }
            return $"{Environment.NewLine}{String.Join($"{Environment.NewLine}{Environment.NewLine}", formattedRepos)}{Environment.NewLine}";
        }
    }
}
