using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscogsConnect.Serialization;
internal class SearchResultConverter : JsonConverter<SearchResult>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(SearchResult).GetTypeInfo().IsAssignableFrom(typeToConvert.GetTypeInfo());
    }
    public override SearchResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readerClone = reader;

        if (readerClone.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        ResourceType? resourceType = null;

        while (readerClone.Read())
        {
            if (readerClone.TokenType == JsonTokenType.PropertyName && readerClone.GetString() == "type")
            {
                readerClone.Read();
                if (readerClone.TokenType == JsonTokenType.String)
                {
                    resourceType = Enum.Parse<ResourceType>(readerClone.GetString(), true);
                }
            }
        }

        return resourceType is not null
            ? (SearchResult)JsonSerializer.Deserialize(ref reader, GetType(resourceType.Value), options)
            : throw new NotImplementedException();
    }
    static Type GetType(ResourceType type) => type switch
    {
        ResourceType.Artist => typeof(ArtistSearchResult),
        ResourceType.Master => typeof(MasterSearchResult),
        ResourceType.Label => typeof(LabelSearchResult),
        ResourceType.Release => typeof(ReleaseSearchResult),
        _ => throw new NotImplementedException()
    };

    public override void Write(Utf8JsonWriter writer, SearchResult value, JsonSerializerOptions options) => throw new NotImplementedException();
}
