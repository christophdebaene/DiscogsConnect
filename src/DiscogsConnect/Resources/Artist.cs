using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DiscogsConnect
{
    public class Artist : Resource
    {
        public string Name { get; set; }
        public string ReleasesUrl { get; set; }
        public string Uri { get; set; }
        public string Realname { get; set; }
        public string Profile { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataQuality DataQuality { get; set; }

        [JsonProperty("namevariations")]
        public List<string> NameVariations { get; set; }

        public List<Alias> Aliases { get; set; }
        public List<string> Urls { get; set; }
        public List<Image> Images { get; set; }

        public class Alias : Resource
        {
            public string Name { get; set; }
        }
    }

    public class ArtistRelease : Resource
    {
        public string Thumb { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int MainRelease { get; set; }
        public string Role { get; set; }
        public int Year { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType Type { get; set; }
    }
}