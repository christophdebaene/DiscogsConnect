namespace DiscogsConnect
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Globalization;
    using System.IO;

    internal class JsonUtils
    {
        public static string PrettyPrint(string json)
        {
            var jsonObject = JObject.Parse(json);
            return jsonObject.ToString(Formatting.Indented);
            
            /*
            var jsonObject = JObject.Parse(json);
            using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
            {
                var textWriter = new JsonTextWriter(sw)
                {
                    Formatting = Formatting.Indented
                };

                jsonObject.WriteTo(textWriter);
                return sw.ToString();
            }
            */
        }
    }
}
