using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System;
using System.Threading.Tasks;

namespace AzureDevOpsPrs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new Configuration();
            var credentials = new VssBasicCredential("", config.PersonalAccessToken);
            var connection = new VssConnection(config.Url, credentials);

            using (GitHttpClient gitClient = connection.GetClient<GitHttpClient>())
            {
                var pullRequests = await gitClient.GetPullRequestsByProjectAsync(config.Project, new GitPullRequestSearchCriteria
                {
                    Status = PullRequestStatus.Active
                });
                pullRequests.ForEach(pr => {
                    Console.WriteLine($"{pr.PullRequestId}: {pr.Title} ({pr.Repository.Name})\n{UrlForPr(config.Url, config.Project, pr.PullRequestId)}/\n\n");
                });
            }

        }

        private static string UrlForPr(Uri url, string project, int prId)
        {
            return $"{url}/_git/{project}/pullrequest/{prId}";
        }
    }
}
