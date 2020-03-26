using DiscogsConnect.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DiscogsConnect
{
    public class DiscogsOptions
    {
        public string UserAgent { get; set; }
        public string Token { get; set; }
        public Action<RateLimit> RateLimitAction { get; set; }
    }

    public class DiscogsClient : IDiscogsClient
    {
        public static readonly Uri DiscogsApiUrl = new Uri("https://api.discogs.com");

        private readonly HttpClient _client;
        public IDatabaseClient Database { get; }
        public IImageClient Image { get; }
        public IUserCollectionClient UserCollection { get; }
        public IUserWantlistClient UserWantlist { get; }
        public DiscogsClient(DiscogsOptions options) : this(options, new HttpClient())
        {
        }
        public DiscogsClient(DiscogsOptions options, HttpClient client)
        {
            _client = client;
            _client.BaseAddress = DiscogsApiUrl;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
            _client.DefaultRequestHeaders.Add("Authorization", $"Discogs token={options.Token}");

            Database = new DatabaseClient(_client);
            Image = new ImageClient(_client);
            UserCollection = new UserCollectionClient(_client);
            UserWantlist = new UserWantlistClient(_client);
        }
    }
}