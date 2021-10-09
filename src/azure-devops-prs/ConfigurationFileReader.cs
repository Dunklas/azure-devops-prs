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
            return String.Join("\n", File.ReadAllLines(path));
        }

    }

}
