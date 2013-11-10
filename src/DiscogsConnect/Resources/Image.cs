namespace DiscogsConnect
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Image
    {        
        public int Width { get; set; }
        public int Height { get; set; }
        public string Uri { get; set; }
        public string Uri150 { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageType Type { get; set; }

        public string ResourceUrl { get; set; }
    }

    public enum ImageType
    {
        Primary,
        Secondary
    }
}
