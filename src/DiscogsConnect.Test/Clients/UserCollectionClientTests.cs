using System;
using System.Threading.Tasks;
using DiscogsConnect.Test;
using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Clients;

[Collection("DiscogsClient")]
public class UserCollectionClientTests(DiscogsClientFixture fixture)
{
    protected IDiscogsClient Client { get; } = fixture.DiscogsClient;
    [Fact]
    public async Task GetAsync()
    {
        var username = Environment.GetEnvironmentVariable("DISCOGS_USERNAME");
        var response = await Client.UserCollection.GetItemsByFolderAllAsync(username, 0);

        response.Should().NotBeNull();        
    }
}
