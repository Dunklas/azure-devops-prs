using System.Collections.Generic;
using AzureDevOpsPrs;
using Xunit;

namespace AzureDevOpsPrsTest
{
    public class ByRepoPrinterTest
    {
        [Fact]
        public void ShouldHandleEmptyListOfPullRequests()
        {
            var prs = new List<PullRequest>();
            var exception = Record.Exception(() => new ByRepoPrinter(prs));
            Assert.Null(exception);
        }
    }
}