using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace AzureDevOpsPrs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new Configuration
            (
                new ConfigurationFileReader().Read()
            );
            var credentials = new VssBasicCredential("", config.PersonalAccessToken);
            var connection = new VssConnection(config.Url, credentials);

            using (GitHttpClient gitClient = connection.GetClient<GitHttpClient>())
            {
                var pullRequests = await gitClient.GetPullRequestsByProjectAsync(config.Project, new GitPullRequestSearchCriteria
                {
                    Status = PullRequestStatus.Active
                });
                var output = String.Join("\n\n", pullRequests
                    .Select(pr => FormatPr(config, pr))
                    .ToList());
                Console.WriteLine(output);
            }

        }

        private static string FormatPr(Configuration config, GitPullRequest pr)
        {
            return $"{pr.PullRequestId}: {pr.Title} ({pr.Repository.Name})\n{UrlForPr(config, pr.PullRequestId)}";
        }
        private static string UrlForPr(Configuration config, int prId)
        {
            return $"{config.Url}/_git/{config.Project}/pullrequest/{prId}";
        }
    }
}
