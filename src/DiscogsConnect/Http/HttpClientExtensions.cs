using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string uri, IEnumerable<(string Name, string Value)> parameters = null)
        {
            if (parameters != null)
            {
                var filteredParameters = parameters
                    .Where(x => x.Value != null && x.Value != "NONE");

                if (filteredParameters.Any())
                    uri = uri + "?" + string.Join("&", filteredParameters.Select(x =>
                    $"{WebUtility.UrlEncode(x.Name)}={WebUtility.UrlEncode(x.Value)}"));
            }

            var result = await client
                .GetStringAsync(uri)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(result, DiscogsSerializerSettings.Default);
        }

        public static async Task<PaginationResponse<T>> GetPagedAsync<T>(this HttpClient client, string requestUri, int page = 1, int perPage = 100, IEnumerable<(string Name, string Value)> parameters = null)
        {
            return await client.GetAsync<PaginationResponse<T>>(requestUri, new[] { ("page", page.ToString()), ("per_page", perPage.ToString()) }.Union(parameters ?? Enumerable.Empty<(string, string)>()));
        }
        public static async Task<List<T>> GetAllPagesAsync<T>(this HttpClient client, string requestUri)
        {
            var result = new List<T>();

            var page = 1;
            Pagination pagination;

            do
            {
                var response = await client.GetPagedAsync<T>(requestUri, page++);
                result.AddRange(response.Items);
                pagination = response.Pagination;
            }
            while (pagination.Page != pagination.Pages);

            return result;
        }

        public static async Task PostAsync<T>(this HttpClient client, string requestUri, T content = null) where T : class
        {
            var httpContent = content == null ? null :
                new StringContent(
                    JsonConvert.SerializeObject(content, DiscogsSerializerSettings.Default),
                    Encoding.UTF8,
                    "application/json");

            using (var response = await client.PostAsync(requestUri, httpContent))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task<TResult> PostAsync<T, TResult>(this HttpClient client, string requestUri, T content = null) where T : class
        {
            var httpContent = content == null ? null :
                new StringContent(
                    JsonConvert.SerializeObject(content, DiscogsSerializerSettings.Default),
                    Encoding.UTF8,
                    "application/json");

            using (var response = await client.PostAsync(requestUri, httpContent))
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(responseBody, DiscogsSerializerSettings.Default);
            }
        }
    }
}