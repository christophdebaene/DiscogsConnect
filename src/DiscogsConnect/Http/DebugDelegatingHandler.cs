using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsConnect.Http
{
    class DebugDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = base.SendAsync(request, cancellationToken);
            var json = response.Result.Content.ReadAsStringAsync().Result;
            json = PrettyPrint(json);
            return response;
        }

        static string PrettyPrint(string json)
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