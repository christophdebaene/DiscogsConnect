using Newtonsoft.Json;

namespace DiscogsConnect
{
    internal class DiscogsSerializerSettings : JsonSerializerSettings
    {
        public static JsonSerializerSettings Default = new DiscogsSerializerSettings();
        public DiscogsSerializerSettings()
        {
            Converters.Add(new SearchResourceConverter());
            ContractResolver = new DiscogsContractResolver();
        }
    }
}