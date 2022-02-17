using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscogsConnect
{
    public class Track
    {
        public string Duration { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }

        [JsonProperty("type_")]
        public string Type { get; set; }

        [JsonProperty("extraartists")]
        public List<Artist> ExtraArtists { get; set; }

        public List<Artist> Artists { get; set; }

        public class Artist : Resource
        {
            public string Name { get; set; }

            [JsonProperty("anv")]
            public string NameVariation { get; set; }
            public string Join { get; set; }
            public string Role { get; set; }
        }
    }
}
