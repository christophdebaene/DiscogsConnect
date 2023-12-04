using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DiscogsConnect.Serialization;

namespace DiscogsConnect.Http;
internal class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;
    public RestClient(HttpClient httpClient)
        => _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<byte[]> GetByteArrayAsync(string path)
        => await _httpClient.GetByteArrayAsync(path);
    public async Task<TResult> SendAsync<TResult>(HttpMethod method, string path, object values, object content) where TResult : class
    {
        using (var requestMessage = new HttpRequestMessage
        {
            Method = method,
            RequestUri = CreateUri(path, values),
            Content = CreateContent(content)
        })

        using (var response = await _httpClient.SendAsync(requestMessage))
        {
            response.EnsureSuccessStatusCode();

            if (typeof(TResult) == typeof(Unit))
                return Unit.Value as TResult;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JSerializer.Deserialize<TResult>(responseBody);
        }
    }
    static Dictionary<string, string> ToDictionary(object values)
    {
        if (values == null)
            return null;

        using (var doc = JSerializer.SerializeToDocument(values))
        {
            return doc.RootElement.EnumerateObject()
                .Where(x => !(x.Value.ValueKind == JsonValueKind.Null || string.IsNullOrWhiteSpace(x.Value.ToString()) || x.Value.ToString().Equals("NONE", StringComparison.OrdinalIgnoreCase)))
                .ToDictionary(x => x.Name, x => x.Value.ToString());
        }
    }

    static Uri CreateUri(string path, object values)
    {
        var dictionary = ToDictionary(values);

        if (dictionary != null && dictionary.Any())
            path = path + "?" + string.Join("&", dictionary.Select(x =>
                $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

        return new Uri(path, UriKind.Relative);
    }
    static HttpContent CreateContent<T>(T content = null) where T : class
       => content == null ? null : new StringContent(JSerializer.Serialize(content), Encoding.UTF8, MediaTypeNames.Application.Json);
}
