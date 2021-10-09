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

        public async Task<List<string>> ListPullRequests(string project)
        {
            using (GitHttpClient gitClient = _connection.GetClient<GitHttpClient>())
            {
                var pullRequests = await gitClient.GetPullRequestsByProjectAsync(project, new GitPullRequestSearchCriteria
                {
                    Status = PullRequestStatus.Active
                });
                return pullRequests
                    .Select(pr => FormatPr(project, pr))
                    .ToList();
            }
        }
        private string FormatPr(string project, GitPullRequest pr)
        {
            return $"{pr.PullRequestId}: {pr.Title} ({pr.Repository.Name})\n{UrlForPr(project, pr.PullRequestId)}";
        }
        private string UrlForPr(string project, int prId)
        {
            return $"{_url}/_git/{project}/pullrequest/{prId}";
        }
    }
}
