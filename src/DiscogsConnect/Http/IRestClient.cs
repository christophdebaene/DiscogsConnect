using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscogsConnect.Http
{
    public interface IRestClient
    {
        Task<byte[]> GetByteArrayAsync(string path);
        Task<TResult> SendAsync<TResult>(HttpMethod method, string path, object parameters, object content) where TResult : class;
    }

    public class RestClient : IRestClient
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

                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseBody, DiscogsSerializerSettings.Default);
            }
        }

        static Dictionary<string, string> ToDictionary(object values)
        {
            if (values == null)
                return null;

            var jsonSerializer = JsonSerializer.CreateDefault(DiscogsSerializerSettings.Default);
            return JObject.FromObject(values, jsonSerializer)
                .Properties()
                .Where(x => !(x.Value.Type == JTokenType.Null || string.IsNullOrWhiteSpace(x.Value.ToString()) || x.Value.ToString().Equals("NONE", StringComparison.OrdinalIgnoreCase)))
                .ToDictionary(x => x.Name, x => x.Value.ToString());
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
           => content == null ? null :
                new StringContent(
                    JsonConvert.SerializeObject(content, DiscogsSerializerSettings.Default),
                    Encoding.UTF8,
                    "application/json");
    }
    public static class RestClientExtensions
    {
        public static async Task<TResult> GetAsync<TResult>(this IRestClient client, string path, object parameters = null) where TResult : class
            => await client.SendAsync<TResult>(HttpMethod.Get, path, parameters, null);
        public static async Task PostAsync(this IRestClient client, string path, object parameters = null, object content = null)
            => await client.SendAsync<Unit>(HttpMethod.Post, path, parameters, content);
        public static async Task<TResult> PostAsync<TResult>(this IRestClient client, string path, object parameters = null, object content = null) where TResult : class
            => await client.SendAsync<TResult>(HttpMethod.Post, path, parameters, content);
        public static async Task PutAsync(this IRestClient client, string path, object parameters = null, object content = null)
          => await client.SendAsync<Unit>(HttpMethod.Put, path, parameters, content);
        public static async Task<TResult> PutAsync<TResult>(this IRestClient client, string path, object parameters = null, object content = null) where TResult : class
            => await client.SendAsync<TResult>(HttpMethod.Put, path, parameters, content);
        public static async Task DeleteAsync(this IRestClient client, string path, object parameters = null, object content = null)
            => await client.SendAsync<Unit>(HttpMethod.Delete, path, parameters, content);
        public static async Task<List<T>> GetAllPagesAsync<T>(this IRestClient client, string path) where T : class
        {
            var result = new List<T>();

            var page = 1;
            Pagination pagination;

            do
            {
                var response = await client.GetAsync<PaginationResponse<T>>(path, new { page = page++, per_page = 100 });
                result.AddRange(response.Items);
                pagination = response.Pagination;
            }
            while (pagination.Page != pagination.Pages);

            return result;
        }
    }
}