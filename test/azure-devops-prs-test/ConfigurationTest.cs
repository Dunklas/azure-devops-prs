using Xunit;
using AzureDevOpsPrs;
using System.Collections.Generic;
using System.Text.Json;
using System;
using System.Linq;

namespace AzureDevOpsPrsTest
{
    public class ConfigurationTest
    {
        [Fact]
        public void ShouldThrowOnInvalidJson()
        {
            Assert.Throws<InvalidJsonException>(() =>
            {
                new Configuration("{\"missing\": \"endbracket\"");
            });
        }

        [Theory]
        [InlineData("url")]
        [InlineData("pat")]
        [InlineData("project")]
        [InlineData("url", "pat", "project")]
        public void ShouldThrowOnMissingProperty(params string[] propertiesToExclude)
        {
            Assert.Throws<MissingPropertyException>(() =>
            {
                new Configuration(createConfig(propertiesToExclude));
            });
        }

        [Fact]
        public void ShouldThrowOnInvalidUrl()
        {
            Assert.Throws<InvalidValueException>(() =>
            {
                new Configuration(createConfig(new List<(string, string)>
                {
                    ("url", "not/a-valid.url"),
                    ("pat", "some-pat"),
                    ("project", "some-proj")
                }));
            });
        }

        private string createConfig(string[] keysToExclude)
        {
            var properties = new List<(string key, string value)>
            {
                ("url", "https://some-azure-devops-url.com"),
                ("pat", "some-pat"),
                ("project", "some-project")
            }.Where(prop => !keysToExclude.Contains(prop.key))
             .ToList();
            return createConfig(properties);
        }

        private string createConfig(List<(string key, string value)> properties)
        {
            var config = properties
                .Aggregate(new Dictionary<string, string>(), (dict, pair) =>
                {
                    dict.Add(pair.key, pair.value);
                    return dict;
                });
            return JsonSerializer.Serialize(config);
        }
    }
}
