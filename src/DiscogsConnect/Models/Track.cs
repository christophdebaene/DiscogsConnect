using System.Collections.Generic;

using Newtonsoft.Json;

namespace DiscogsConnect;

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
        public string Join { get; set; }
        public string Role { get; set; }
        [JsonProperty("anv")] // Artist Name Variation: https://support.discogs.com/hc/en-us/articles/360005054753-Database-Guidelines-2-Artist#Artist_Name_Variation_ANV
        public string NameVariation { get; set; }
    }
}
