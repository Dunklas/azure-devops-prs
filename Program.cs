using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System;
using System.Threading.Tasks;

namespace ado_prs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var credentials = new VssBasicCredential("", "<-PAT->");
            var uri = new Uri("<-URL->");
            var connection = new VssConnection(uri, credentials);

            using (GitHttpClient gitClient = connection.GetClient<GitHttpClient>())
            {
                var pullRequests = await gitClient.GetPullRequestsByProjectAsync("<-PROJECT->", new GitPullRequestSearchCriteria
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
