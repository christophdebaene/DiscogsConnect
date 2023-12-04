using System;
using System.Threading.Tasks;
using DiscogsConnect.Test;
using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Clients;

[Collection("DiscogsClient")]
public class UserWantlistClientTests
{
    protected IDiscogsClient Client { get; }
    public UserWantlistClientTests(DiscogsClientFixture fixture)
    {
        Client = fixture.DiscogsClient;
    }

    [Fact]
    public async Task GetAsync()
    {
        var username = Environment.GetEnvironmentVariable("DISCOGS_USERNAME");
        var response = await Client.UserWantlist.GetAsync(username);

        response.Should().NotBeNull();
        response.Pagination.Should().NotBeNull();
        response.Pagination.Page.Should().Be(1);
        response.Pagination.Pages.Should().BeGreaterThanOrEqualTo(1);
        response.Pagination.Items.Should().BeGreaterThanOrEqualTo(1);
    }
}
