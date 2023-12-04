using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscogsConnect.Http;

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
