using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace DiscogsConnect;
public class Track
{
    public string Duration { get; set; }
    public string Position { get; set; }
    public string Title { get; set; }

    [JsonPropertyName("type_")]
    public string Type { get; set; }

    [JsonPropertyName("extraartists")]
    public List<Artist> ExtraArtists { get; set; }
    public List<Artist> Artists { get; set; }

    public string GetFormattedArtists() =>
        Artists.Select(artist =>
        {
            var join = !string.IsNullOrEmpty(artist.Join)
                ? artist.Join switch
                {
                    "," => ", ",
                    _ => $" {artist.Join} " // &, Feat., Vs., ...
                }
                : string.Empty;

            return $"{artist.Name}{join}";
        }).Aggregate((result, e) => $"{result}{e}");

    public class Artist : Resource
    {
        public string Name { get; set; }
        public string Join { get; set; }
        public string Role { get; set; }
        [JsonPropertyName("anv")] // Artist Name Variation: https://support.discogs.com/hc/en-us/articles/360005054753-Database-Guidelines-2-Artist#Artist_Name_Variation_ANV
        public string NameVariation { get; set; }
    }
}
