using Newtonsoft.Json;

namespace DiscogsConnect.Serialization
{
    public class DiscogsSerializerSettings : JsonSerializerSettings
    {
        public static JsonSerializerSettings Default = new DiscogsSerializerSettings();

        public DiscogsSerializerSettings()
        {
            this.Converters.Add(new SearchResourceConverter());
            this.ContractResolver = new DiscogsContractResolver();
        }
    }
}