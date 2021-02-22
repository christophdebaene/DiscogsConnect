using System.Collections.Generic;
using Newtonsoft.Json;

namespace DiscogsConnect
{
    public class Release : Resource
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string Status { get; set; }
        public DataQuality DataQuality { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public string Released { get; set; }
        public string ReleasedFormatted { get; set; }
        public string Notes { get; set; }
        public int FormatQuantity { get; set; }
        public Community Community { get; set; }
        public List<string> Styles { get; set; }
        public List<string> Genres { get; set; }
        public List<Entity> Labels { get; set; }
        public List<Entity> Series { get; set; }
        public List<Entity> Companies { get; set; }

        [JsonProperty("extraartists")]
        public List<Artist> ExtraArtists { get; set; }

        public List<Video> Videos { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Format> Formats { get; set; }
        public List<Image> Images { get; set; }

        [JsonProperty("tracklist")]
        public List<Track> Tracks { get; set; }

        public List<Identifier> Identifiers { get; set; }
        public int NumForSale { get; set; }
        public float? LowestPrice { get; set; }
        public string Thumb { get; set; }
        public int MasterId { get; set; }
        public int EstimatedWeight { get; set; }

        public class Artist : Resource
        {
            public string Tracks { get; set; }
            public string Role { get; set; }
            public string Anv { get; set; }
            public string Join { get; set; }
            public string Name { get; set; }
            public string ThumbnailUrl { get; set; }
        }

        public class Identifier
        {
            public string Type { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }
    }
}