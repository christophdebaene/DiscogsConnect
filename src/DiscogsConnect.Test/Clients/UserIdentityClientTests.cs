using System;
using System.Threading.Tasks;
using DiscogsConnect.Test;
using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Clients;

[Collection("DiscogsClient")]
public class UserIdentityClientTests(DiscogsClientFixture fixture)
{
    protected IDiscogsClient Client { get; } = fixture.DiscogsClient;
    [Fact]
    public async Task GetUser()
    {        
        var response = await Client.UserIdentity.GetUserAsync("shooezgirl");

        response.Should().NotBeNull();
        response.Username.Should().Be("shooezgirl");
        response.CurrAbbr.Should().Be("USD");
        response.HomePage.Should().BeEmpty();
        response.Profile.Should().BeEmpty();
        response.Name.Should().Be("Liz");
        response.Location.Should().Be("Portland Oregon");
    }

    [Fact]
    public async Task GetSubmissions()
    {        
        var response = await Client.UserIdentity.GetSubmissionsAsync("shooezgirl", 1, 5);
        response.Should().NotBeNull();
    }
}
