using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DiscogsConnect.Serialization;
internal class SearchTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        if (type.Name == "PaginationResponse`1")
        {
            var propertyInfo = typeInfo.Properties.FirstOrDefault(x => x.Name == "items");
            if (propertyInfo != null)
            {
                propertyInfo.Name = GetPropertyName(type.GetGenericArguments()[0]);
            }
        }

        return typeInfo;
    }
    static string GetPropertyName(Type type) => type switch
    {
        Type _ when type == typeof(ArtistSearchResult) => "releases",
        Type _ when type == typeof(LabelRelease) => "releases",
        Type _ when type == typeof(ArtistRelease) => "releases",
        Type _ when type == typeof(CollectionItem) => "releases",
        Type _ when type == typeof(MasterVersion) => "versions",
        Type _ when type == typeof(SearchResult) => "results",
        Type _ when type == typeof(Wants) => "wants",
        _ => throw new NotImplementedException()
    };
}


