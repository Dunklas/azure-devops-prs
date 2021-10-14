using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevOpsPrs
{
    public class ByRepoPrinter
        : PullRequestsPrinter
    {
        private const int TITLE_MAX_LENGTH = 40; 
        private int prMaxLength;
        private int titleMaxLength;
        private Dictionary<string, List<PullRequest>> prsByRepo;

        public ByRepoPrinter(List<PullRequest> pullRequests)
        {
            prMaxLength = pullRequests
                .Select(pr => FormatPrId(pr.Id)) 
                .Select(formattedPr => formattedPr.Length)
                .Max();
            titleMaxLength = Math.Min(TITLE_MAX_LENGTH, pullRequests
                .Select(pr => pr.Title)
                .Select(title => title.Length)
                .Max());
            prsByRepo = pullRequests
                .OrderBy(pr => pr.Repository)
                .GroupBy(pr => pr.Repository)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public void Print()
        {
            Console.Write(Environment.NewLine);
            foreach (var repoAndPrs in prsByRepo)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($" ___ {repoAndPrs.Key} ___{Environment.NewLine}{Environment.NewLine}");
                Console.ResetColor();
                repoAndPrs.Value
                    .ForEach(pr =>
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {FormatPrId(pr.Id)} ".PadRight(prMaxLength + 2));
                        Console.ResetColor();
                        Console.Write($"{Truncate(pr.Title, titleMaxLength)} ".PadRight(titleMaxLength + 1));
                        Console.Write(pr.Url);
                        Console.Write(Environment.NewLine);
                    });
                Console.Write(Environment.NewLine);
            }
        }

        private string FormatPrId(int prId)
        {
            return $"#{prId}";
        }

        private string Truncate(string text, int wantedLength)
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
