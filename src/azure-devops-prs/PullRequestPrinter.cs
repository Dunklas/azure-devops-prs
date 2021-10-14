using System.Collections.Generic;

namespace AzureDevOpsPrs
{
    public interface PullRequestsPrinter
    {
        void Print(List<PullRequest> pullRequests);
    }
}