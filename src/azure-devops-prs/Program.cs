using System;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevOpsPrs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Configuration config = null;
            try
            {
                config = new Configuration(new ConfigurationFileReader().Read());
            }
            catch (ConfigurationException e)
            {
                Console.Error.WriteLine(e.Message);
                Environment.Exit(1);
            }

            var client = new AzureDevOpsClient(config.Url, config.PersonalAccessToken);
            var prs = await client.ListPullRequests(config.Project);
            Console.WriteLine(String.Join("\n\n", prs
                .Select(pr => FormatPr(pr))));
        }

        
        private static string FormatPr(PullRequest pr)
        {
            return $"{pr.Id}: {pr.Title} ({pr.Repository})\n{pr.Url}";
        }
    }
}
