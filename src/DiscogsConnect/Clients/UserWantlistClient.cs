using System.Net.Http;

namespace DiscogsConnect
{
    public class UserWantlistClient : IUserWantlistClient
    {
        private readonly HttpClient _httpClient;
        public UserWantlistClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}