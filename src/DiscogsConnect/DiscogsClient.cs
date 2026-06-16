using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DiscogsConnect.Http;

namespace DiscogsConnect;
public class DiscogsOptions
{
    public string UserAgent { get; set; }
    public string Token { get; set; }    
}
public class DiscogsClient : IDiscogsClient
{
    public static readonly Uri DiscogsApiUrl = new("https://api.discogs.com");

    private readonly IRestClient _restClient;

    public IDatabaseClient Database { get; }
    public IImageClient Image { get; }
    public IUserIdentityClient UserIdentity { get;  }
    public IUserCollectionClient UserCollection { get; }
    public IUserWantlistClient UserWantlist { get; }
    public DiscogsClient(DiscogsOptions options) : this(options, new HttpClient())
    {
    }
    public DiscogsClient(DiscogsOptions options, HttpClient client)
    {
        client.BaseAddress = DiscogsApiUrl;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);
        client.DefaultRequestHeaders.Add("Authorization", $"Discogs token={options.Token}");

        _restClient = new RestClient(client);

        Database = new DatabaseClient(_restClient);
        Image = new ImageClient(_restClient);
        UserIdentity = new UserIdentityClient(_restClient);
        UserCollection = new UserCollectionClient(_restClient);
        UserWantlist = new UserWantlistClient(_restClient);
    }
}
