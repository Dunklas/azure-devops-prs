using Xunit;
using AzureDevOpsPrs;
using System.Collections.Generic;

namespace AzureDevOpsPrsTest
{
    public class ByRepoFormatterTest
    {

        private ByRepoFormatter formatter;

        public ByRepoFormatterTest()
        {
            formatter = new ByRepoFormatter();
        }

        [Fact]
        public void ShouldOrderByRepoAlphabetically()
        {
            var pullRequests = new List<PullRequest>
            {
                new PullRequest.Builder()
                    .SetRepository("GreatRepository")
                    .SetTitle("Great pull request")
                    .Build(),
                new PullRequest.Builder()
                    .SetRepository("WhoopingRepository")
                    .SetTitle("Whooping pull request")
                    .Build(),
                new PullRequest.Builder()
                    .SetRepository("AwesomeRepository")
                    .SetTitle("Awesome pull request")
                    .Build(),
            };

            var formattedRepos = formatter.Format(pullRequests);
            var awesomeIndex = formattedRepos.IndexOf("AwesomeRepository");
            var greatIndex = formattedRepos.IndexOf("GreatRepository");
            var whoopingIndex = formattedRepos.IndexOf("WhoopingRepository");

            Assert.True(awesomeIndex < greatIndex, "AwesomeRepository should be before GreatRepository");
            Assert.True(greatIndex < whoopingIndex, "GreatRepository should be before WhoopingRepository");
        }

        [Fact]
        public void TruncateSomethingLong()
        {
            var input = "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them";
            Assert.Equal("One...", formatter.Truncate(input, 6));
            Assert.Equal("One Ring to rule them all, One...", formatter.Truncate(input, 33));
            Assert.Equal("One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them", formatter.Truncate(input, 106));
        }
    }
}
