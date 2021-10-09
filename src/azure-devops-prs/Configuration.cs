using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AzureDevOpsPrs
{
    public class Configuration
    {
        public Uri Url { get; }
        public string PersonalAccessToken { get; }
        public string Project { get; }

        public Configuration()
        {
            var jsonConfig = Load();
            Url = new Uri(jsonConfig.GetValueOrDefault("url"));
            PersonalAccessToken = jsonConfig.GetValueOrDefault("pat");
            Project = jsonConfig.GetValueOrDefault("project");
        }

        private Dictionary<string, string> Load()
        {
            var path = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "azure-devops-prs",
                "config.json"
            );
            var raw = String.Join("\n", File.ReadAllLines(path));
            return JsonSerializer.Deserialize<Dictionary<string, string>>(raw);
        }
    }
}