using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevOpsPrs
{
    public class ByRepoPrinter
        : PullRequestsPrinter
    {
        public void Print(List<PullRequest> pullRequests)
        {
            var prsByRepo = pullRequests
                .OrderBy(pr => pr.Repository)
                .GroupBy(
                    pr => pr.Repository,
                    pr => pr,
                    (repo, pr) => new { Repository = repo, PullRequests = pr.ToList()}
                );

            var formattedRepos = new List<string>();
            Console.Write(Environment.NewLine);
            foreach (var repoAndPrs in prsByRepo)
            {
                Console.Write($" ___ {repoAndPrs.Repository} ___{Environment.NewLine}{Environment.NewLine}");
                repoAndPrs.PullRequests
                    .ForEach(pr =>
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(String.Format("{0,-6}", $" #{pr.Id} "));
                        Console.ResetColor();
                        Console.Write(String.Format("{0,-40} {1,-30}{2}", Truncate(pr.Title, 40), pr.Url, Environment.NewLine));
                    });
                Console.Write(Environment.NewLine);
            }
        }

        public string Truncate(string text, int wantedLength)
        {
            var finisher = "...";
            if (text.Length <= wantedLength)
            {
                return text;
            }
            return text.Substring(0, wantedLength - finisher.Length) + finisher;
        }
    }
}
