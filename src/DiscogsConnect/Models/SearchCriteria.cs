using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace DiscogsConnect
{
    public class SearchCriteria
    {
        public string Query { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ResourceType Type { get; set; }

        public string Title { get; set; }
        public string ReleaseTitle { get; set; }
        public string Credit { get; set; }
        public string Artist { get; set; }
        public string Anv { get; set; }
        public string Label { get; set; }
        public string Genre { get; set; }
        public string Style { get; set; }
        public string Country { get; set; }
        public string Year { get; set; }
        public string Format { get; set; }
        public string Catno { get; set; }
        public string Barcode { get; set; }
        public string Track { get; set; }
        public string Submitter { get; set; }
        public string Contributor { get; set; }

        //internal string BuildQueryString()
        //{
        //    var jsonSerializer = JsonSerializer.CreateDefault(Serialization.DiscogsSerializerSettings.Default);
        //    var jObject = JObject.FromObject(this, jsonSerializer);

        //    var query = string.Join("&", jObject
        //        .Children()
        //        .Cast<JProperty>()
        //        .Where(x => !string.IsNullOrEmpty(x.Value.ToString()))
        //        .Select(x => string.Format("{0}={1}", x.Name, x.Value.ToString())));

        //    return query;
        //}
    }
}