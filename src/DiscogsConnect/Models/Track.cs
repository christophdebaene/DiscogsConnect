using Newtonsoft.Json;
using System.Collections.Generic;

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
        }
    }
}