using Xunit;
using AzureDevOpsPrs;
using System.Collections.Generic;

namespace AzureDevOpsPrsTest
{
    public class ByRepoFormatterTest
    {
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

            var formattedRepos = new ByRepoFormatter()
                .Format(pullRequests);
            var awesomeIndex = formattedRepos.IndexOf("AwesomeRepository");
            var greatIndex = formattedRepos.IndexOf("GreatRepository");
            var whoopingIndex = formattedRepos.IndexOf("WhoopingRepository");

            Assert.True(awesomeIndex < greatIndex, "AwesomeRepository should be before GreatRepository");
            Assert.True(greatIndex < whoopingIndex, "GreatRepository should be before WhoopingRepository");
        }        
    }
}
