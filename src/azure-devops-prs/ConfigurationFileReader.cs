using System;
using System.IO;

namespace AzureDevOpsPrs
{
    public class ConfigurationFileReader
    {

        public string Read()
        {
            var path = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "azure-devops-prs",
                "config.json"
            );
            if (!File.Exists(path))
            {
                throw new MissingConfigurationException($"No configuration file found at: {path}");
            }
            return String.Join("\n", File.ReadAllLines(path));
        }

    }

}
