using System.Net.Http;

namespace DiscogsConnect.Http
{
    public interface IHttpClientFactory
    {
        HttpClient Create(HttpClientOptions options);
    }
}