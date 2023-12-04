using System.Collections.Generic;
using System.Text.Json.Serialization;
using DiscogsConnect.Serialization;

namespace DiscogsConnect;

[JsonConverter(typeof(SearchResultConverter))]
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
    public List<string> Style { get; set; }
    public List<string> Format { get; set; }
    public List<Format> Formats { get; set; }
    public int FormatQuantity { get; set; }
    public string Country { get; set; }
    public List<string> Barcode { get; set; }
    public List<string> Label { get; set; }
    public string Catno { get; set; }
    public string Year { get; set; }
    public List<string> Genre { get; set; }
}

public class LabelSearchResult : SearchResult
{
}
public class MasterSearchResult : SearchResult
{
    public List<string> Style { get; set; }
    public List<string> Format { get; set; }
    public string Country { get; set; }
    public List<string> Label { get; set; }
    public string Catno { get; set; }
    public string Year { get; set; }
    public List<string> Genre { get; set; }
    public List<string> Barcode { get; set; }
}
