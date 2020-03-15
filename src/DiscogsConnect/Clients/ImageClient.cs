using System.Net.Http;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal class ImageClient : IImageClient
    {
        private HttpClient _httpClient;
        public ImageClient(HttpClient httpClient)
            => _httpClient = httpClient;        
        public async Task<byte[]> GetImageAsync(string uri) 
            => await _httpClient.GetByteArrayAsync(uri);        
    }
}