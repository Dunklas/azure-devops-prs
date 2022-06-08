using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevOpsPrs
{
    public class ByRepoPrinter
        : PullRequestsPrinter
    {
        private const int TitleMaxLength = 40;
        private readonly int _prMaxLength;
        private readonly int _titleMaxLength;
        private readonly Dictionary<string, List<PullRequest>> _prsByRepo;

        public ByRepoPrinter(IReadOnlyCollection<PullRequest> pullRequests)
        {
            if (!pullRequests.Any())
            {
                _prMaxLength = 0;
                _titleMaxLength = 0;
                _prsByRepo = new Dictionary<string, List<PullRequest>>();
            }
            _prMaxLength = pullRequests
                .Select(pr => FormatPrId(pr.Id))
                .Select(formattedPr => formattedPr.Length)
                .Max();
            _titleMaxLength = Math.Min(TitleMaxLength, pullRequests
                .Select(pr => pr.Title)
                .Select(title => title.Length)
                .Max());
            _prsByRepo = pullRequests
                .OrderBy(pr => pr.Repository)
                .GroupBy(pr => pr.Repository)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public void Print()
        {
            Console.Write(Environment.NewLine);
            foreach (var repoAndPrs in _prsByRepo)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($" ___ {repoAndPrs.Key} ___{Environment.NewLine}{Environment.NewLine}");
                Console.ResetColor();
                repoAndPrs.Value
                    .ForEach(pr =>
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {FormatPrId(pr.Id)} ".PadRight(_prMaxLength + 2));
                        Console.ResetColor();
                        Console.Write($"{Truncate(pr.Title, _titleMaxLength)} ".PadRight(_titleMaxLength + 1));
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
