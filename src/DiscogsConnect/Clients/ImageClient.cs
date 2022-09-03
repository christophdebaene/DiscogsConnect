using System.Threading.Tasks;

using DiscogsConnect.Http;

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
