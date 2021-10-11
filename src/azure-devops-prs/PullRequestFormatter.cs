using System.Collections.Generic;

namespace AzureDevOpsPrs
{
    public interface PullRequestsFormatter
    {
        string Format(List<PullRequest> pullRequests);
    }
}