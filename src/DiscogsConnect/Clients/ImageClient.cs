using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect;
internal class ImageClient(IRestClient restClient) : IImageClient
{
    public async Task<byte[]> GetAsync(string uri)
        => await restClient.GetByteArrayAsync(uri);
}
