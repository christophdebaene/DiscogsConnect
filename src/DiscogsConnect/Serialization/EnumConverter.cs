using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscogsConnect.Serialization;
internal class EnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    private readonly Dictionary<TEnum, string> _enumToString = [];
    private readonly Dictionary<string, TEnum> _stringToEnum = [];
    private readonly Dictionary<int, TEnum> _numberToEnum = [];
    public EnumConverter()
    {
        var type = typeof(TEnum);
        foreach (var value in Enum.GetValues<TEnum>())
        {
            var enumMember = type.GetMember(value.ToString())[0];
            var attr = enumMember.GetCustomAttribute<EnumMemberAttribute>(false);
            var num = Convert.ToInt32(type.GetField("value__")?.GetValue(value));

            if (attr?.Value != null)
            {
                _enumToString.Add(value, attr.Value);
                _stringToEnum.Add(attr.Value, value);
                _numberToEnum.Add(num, value);
            }
            else
            {
                _enumToString.Add(value, value.ToString());
                _stringToEnum.Add(value.ToString(), value);
                _numberToEnum.Add(num, value);
            }
        }
    }
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.TokenType;
        if (type == JsonTokenType.String)
        {
            var stringValue = reader.GetString();

            if (stringValue != null && _stringToEnum.TryGetValue(stringValue, out var enumValue))
            {
                return enumValue;
            }
        }
        else if (type == JsonTokenType.Number)
        {
            var numValue = reader.GetInt32();
            _numberToEnum.TryGetValue(numValue, out var enumValue);
            return enumValue;
        }

        return default;
    }
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(_enumToString[value]);
    }
}
