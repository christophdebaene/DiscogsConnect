using System.Net.Http;
using System.Threading.Tasks;

namespace DiscogsConnect.Http;

public interface IRestClient
{
    Task<byte[]> GetByteArrayAsync(string path);
    Task<TResult> SendAsync<TResult>(HttpMethod method, string path, object parameters, object content) where TResult : class;
}
