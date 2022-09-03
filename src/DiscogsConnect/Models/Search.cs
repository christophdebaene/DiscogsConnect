using System.Collections.Generic;

using Newtonsoft.Json;

namespace DiscogsConnect
{
    public abstract class SearchResult : Resource
    {
        public ResourceType Type { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
    }

    public class ArtistSearchResult : SearchResult
    {
    }
    public class ReleaseSearchResult : SearchResult
    {
        [JsonProperty("style")]
        public List<string> Styles { get; set; }

        [JsonProperty("format")]
        public List<string> Formats { get; set; }

        public string Country { get; set; }

        [JsonProperty("barcode")]
        public List<string> Barcodes { get; set; }

        [JsonProperty("label")]
        public List<string> Labels { get; set; }

        public string Catno { get; set; }
        public int Year { get; set; }

        [JsonProperty("genre")]
        public List<string> Genres { get; set; }
    }

    public class LabelSearchResult : SearchResult
    {
    }

    public class MasterSearchResult : SearchResult
    {
        [JsonProperty("style")]
        public List<string> Styles { get; set; }

        [JsonProperty("format")]
        public List<string> Formats { get; set; }

        public string Country { get; set; }

        [JsonProperty("label")]
        public List<string> Labels { get; set; }

        public string Catno { get; set; }
        public int Year { get; set; }

        [JsonProperty("genre")]
        public List<string> Genres { get; set; }
    }
}
