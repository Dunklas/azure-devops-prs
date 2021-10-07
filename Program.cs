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
                    Console.WriteLine($"{pr.PullRequestId}: {pr.Title} ({pr.Repository.Name})\n{pr.Url}\n\n");
                });
            }

        }
    }
}
