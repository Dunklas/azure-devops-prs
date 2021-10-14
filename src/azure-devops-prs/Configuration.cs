using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AzureDevOpsPrs
{
    public class Configuration
    {
        public Uri Url { get; }
        public string PersonalAccessToken { get; }
        public string Project { get; }

        public Configuration(string jsonConfig)
        {
            var json = ReadJson(jsonConfig);
            PersonalAccessToken = GetValue(json, "pat");
            Project = GetValue(json, "project");
            Uri url;
            if (!Uri.TryCreate(GetValue(json, "url"), UriKind.Absolute, out url))
            {
                throw new InvalidValueException($"Configuration file contains an invalid value: {GetValue(json, "url")}");
            }
            Url = url;
        }

        private string GetValue(Dictionary<string, string> json, string key)
        {
            string value = null;
            if (!json.TryGetValue(key, out value))
            {
                throw new MissingPropertyException($"Configuration file is missing required property: {key}");
            }
            return value;
        }

        private Dictionary<string, string> ReadJson(string jsonConfig)
        {
            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonConfig);
            }
            catch (JsonException)
            {
                throw new InvalidJsonException("Configuration file contains invalid json");
            }
        }
    }
}