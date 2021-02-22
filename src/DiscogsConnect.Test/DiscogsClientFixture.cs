using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DiscogsConnect.Test
{
    public class DiscogsClientFixture
    {
        public IDiscogsClient DiscogsClient { get; }
        public DiscogsClientFixture()
            => DiscogsClient = new DiscogsClient(DiscogsOptionsFactory.Create());
    }

    [CollectionDefinition("DiscogsClient")]
    public class DiscogsClientCollection : ICollectionFixture<DiscogsClientFixture>
    {
    }
    public static class DiscogsOptionsFactory
    {
        public const string UserAgent = "DiscogsConnect/2.0";
        public static DiscogsOptions Create()
            => new DiscogsOptions
            {
                UserAgent = UserAgent,
                Token = Environment.GetEnvironmentVariable("DISCOGS_TOKEN")
            };

        static DiscogsOptionsFactory()
        {
            var path = @"Properties\launchSettings.json";

            if (File.Exists(path))
            {
                using (var launchSettingsFile = File.OpenText(path))
                using (var jsonReader = new JsonTextReader(launchSettingsFile))
                {
                    var jsonObject = JObject.Load(jsonReader);

                    jsonObject
                        .GetValue("profiles")
                        .SelectMany(profiles => profiles.Children())
                        .SelectMany(profile => profile.Children<JProperty>())
                        .Where(property => property.Name == "environmentVariables")
                        .SelectMany(property => property.Value.Children<JProperty>())
                        .ToList()
                        .ForEach(variable =>
                            Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString())
                        );
                }
            }
        }
    }
}