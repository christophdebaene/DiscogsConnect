# DiscogsConnect

DiscogsConnect is a .NET client library for accessing the [REST API](http://www.discogs.com/developers) v2.0 of [Discogs](http://www.discogs.com).

## Installation

Install with [NuGet](https://www.nuget.org/packages/DiscogsConnect)

    Install-Package DiscogsConnect

## Quickstart

```csharp
static async Task Main(string[] args)
{
    var options = new DiscogsOptions
    {
        Token = "...",
        UserAgent = "MyApp/2.0"
    };

    var client = new DiscogsClient(options);
                
    var artist        = await client.Database.GetArtistAsync(1360);
    var artistRelease = await client.Database.GetArtistReleasesAsync(1360, 1, 100);
    var release       = await client.Database.GetReleaseAsync(8310);            
    var masterRelease = await client.Database.GetMasterReleaseAsync(36961);

    var username = "...";
    var items  = await client.UserCollection.GetItemsByFolderAsync(username, 0);
    var wanted = await client.UserWantlist.GetAllAsync(username);
    // ...
}
```

## Rate Limiting

Note that requests are [throttled](https://www.discogs.com/developers#page:home,header:home-rate-limiting) by Discogs by source IP to 60 per minute.
The number of requests remaining are passed through headers.

Below you find a simple but working example to wait when you reach the limit of number of requests.

```csharp
static async Task Main(string[] args)
{
    var options = new DiscogsOptions
    {
        Token = "...",
        UserAgent = "MyApp/2.0"
    };
    
    var httpClient = new HttpClient(new RateLimitHandler(), true);
    var client = new DiscogsClient(options, httpClient);
                                    
    var username = "...";
    var items = await client.UserCollection.GetItemsByFolderAllAsync(username, 0);
}

public class RateLimitHandler : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        var rateLimit = RateLimit.Parse(response);

        Console.WriteLine($"Used:{rateLimit.Total} - Remaining:{rateLimit.Remaining} - Total:{rateLimit.Total}");

        if (rateLimit.Remaining < 2)
            Thread.Sleep(TimeSpan.FromSeconds(90));

        return response;
    }
}
```

## Development

After cloning the project, go to the `Properties` directory in the test project, rename `_launchSettings.json` to `launchSettings.json`
(remove the preceding underscore) and edit the file to insert your personal Discord token.
This file is gitignore'd after renaming so you won't have to worry about accidentally leaking your token.
