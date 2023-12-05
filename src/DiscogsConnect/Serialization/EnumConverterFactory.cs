using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscogsConnect.Serialization;
internal class EnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var type = typeof(EnumConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(type)!;
    }
}
