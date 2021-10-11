using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOpsPrs
{
    public class AzureDevOpsClient 
    {

        private Uri _url;
        private VssConnection _connection;

        public AzureDevOpsClient(Uri url, string pat)
        {
            _url = url;
            _connection = new VssConnection(url, new VssBasicCredential("", pat));
        }

        public async Task<List<PullRequest>> ListPullRequests(string project)
        {
            using (GitHttpClient gitClient = _connection.GetClient<GitHttpClient>())
            {
                var pullRequests = await gitClient.GetPullRequestsByProjectAsync(project, new GitPullRequestSearchCriteria
                {
                    Status = PullRequestStatus.Active
                });
                return pullRequests
                    .Select(pr => ToPullRequest(pr, project))
                    .ToList();
            }
        }

        private PullRequest ToPullRequest(GitPullRequest azurePr, string project)
        {
            return new PullRequest.Builder()
                .SetId(azurePr.PullRequestId)
                .SetTitle(azurePr.Title)
                .SetDescription(azurePr.Description)
                .SetCreatedAt(azurePr.CreationDate)
                .SetCreatedBy(azurePr.CreatedBy.DisplayName)
                .SetUrl(new Uri($"{_url}/{project}/_git/{azurePr.Repository.Name}/pullrequest/{azurePr.PullRequestId}"))
                .SetRepository(azurePr.Repository.Name)
                .SetStatus(azurePr.Status.ToString())
                .Build();
        }
    }
}
