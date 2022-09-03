using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiscogsConnect;

internal class DiscogsSerializerSettings : JsonSerializerSettings
{
    public static JsonSerializerSettings Default = new DiscogsSerializerSettings();

    public DiscogsSerializerSettings()
    {
        Converters.Add(new StringEnumConverter());
        Converters.Add(new SearchResourceConverter());
        ContractResolver = new DiscogsContractResolver();
    }
}
