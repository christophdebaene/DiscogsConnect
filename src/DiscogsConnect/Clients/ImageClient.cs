using DiscogsConnect.Http;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal class ImageClient : IImageClient
    {
        private readonly IRestClient _restClient;
        public ImageClient(IRestClient restClient)
            => _restClient = restClient;

        public async Task<byte[]> GetAsync(string uri)
            => await _restClient.GetByteArrayAsync(uri);
    }
}