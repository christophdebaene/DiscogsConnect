using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DiscogsConnect
{
    public class Label : Resource
    {
        public string Name { get; set; }
        public string Profile { get; set; }
        public string ReleasesUrl { get; set; }
        public string Uri { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DataQuality DataQuality { get; set; }

        public string ContactInfo { get; set; }
        public List<Sublabel> Sublabels { get; set; }
        public List<string> Urls { get; set; }
        public List<Image> Images { get; set; }

        public class Sublabel : Resource
        {
            public string Name { get; set; }
        }
    }

    public class LabelRelease : Resource
    {
        public string Status { get; set; }
        public string Thumb { get; set; }
        public string Format { get; set; }
        public string Title { get; set; }
        public string Catno { get; set; }
        public string Artist { get; set; }
    }
}