using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DiscogsConnect
{
    public class SearchCriteria
    {
        public string Query { get; set; }
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

        internal IEnumerable<(string Name, string Value)> Parameters()
        {
            var jsonSerializer = JsonSerializer.CreateDefault(DiscogsSerializerSettings.Default);
            return JObject.FromObject(this, jsonSerializer)
                .Properties()
                .Where(x => !string.IsNullOrEmpty(x.Value.ToString()))
                .Select(x => (x.Name, x.Value.ToString()));
        }
    }
}