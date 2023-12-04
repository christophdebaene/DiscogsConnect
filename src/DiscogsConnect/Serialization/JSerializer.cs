using System.Text.Json;

namespace DiscogsConnect.Serialization;
internal static class JSerializer
{
    static readonly JsonSerializerOptions s_options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters =
        {
            new EnumConverterFactory(),
        },
        TypeInfoResolver = new SearchTypeResolver()
    };
    public static T Deserialize<T>(string data) => JsonSerializer.Deserialize<T>(data, s_options);
    public static string Serialize<T>(T data) => JsonSerializer.Serialize<T>(data, s_options);
    public static JsonDocument SerializeToDocument(object data) => JsonSerializer.SerializeToDocument(data, s_options);
}
