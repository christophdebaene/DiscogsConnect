using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscogsConnect.Test;
using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Clients;

[Collection("DiscogsClient")]
public partial class DatabaseClientTests
{
    protected IDiscogsClient Client { get; }

    public DatabaseClientTests(DiscogsClientFixture fixture)
    {
        Client = fixture.DiscogsClient;
    }

    [Fact]
    public async Task GetArtistAsync()
    {
        var artistId = 45; // Aphex Twin

        var response = await Client.Database.GetArtistAsync(artistId);

        response.Should().NotBeNull();
        response.Name.Should().Be("Aphex Twin");
        response.Id.Should().Be(45);
        response.ResourceUrl.Should().Be("https://api.discogs.com/artists/45");
        response.Uri.Should().Be("https://www.discogs.com/artist/45-Aphex-Twin");
        response.ReleasesUrl.Should().Be("https://api.discogs.com/artists/45/releases");

        var primaryImage = response.Images.FirstOrDefault(x => x.Type == ImageType.Primary);
        primaryImage.Should().NotBeNull();
        primaryImage.Width.Should().BeGreaterThan(0);
        primaryImage.Height.Should().BeGreaterThan(0);

        response.Realname.Should().Be("Richard David James");
        response.Profile.Should().Contain("18 August 1971");
        response.Urls.Should().Contain("https://aphextwin.warp.net/", "https://en.wikipedia.org/wiki/Aphex_Twin");
        response.NameVariations.Should().Contain("Apex Twin", "Aphex Twins", "AphexTwin", "The Aphex Twin");
        response.Aliases.Should().NotBeEmpty();

        var alias = response.Aliases.FirstOrDefault(x => x.Id == 46);
        alias.Should().NotBeNull();
        alias.Name.Should().Be("GAK");
        alias.ResourceUrl.Should().Be("https://api.discogs.com/artists/46");
    }

    [Fact]
    public async Task GetArtistReleasesAsync()
    {
        var artistId = 1360; // Adam Beyer
        var page = 2;
        var pageSize = 50;

        var response = await Client.Database.GetArtistReleasesAsync(artistId, page, pageSize);

        response.Should().NotBeNull();

        response.Pagination.PerPage.Should().Be(pageSize);
        response.Pagination.Items.Should().BeGreaterThan(500);
        response.Pagination.Page.Should().Be(page);
        response.Pagination.Pages.Should().BeGreaterThan(10);

        response.Items.Should().NotBeEmpty();
        response.Items.Should().HaveCount(pageSize);
    }

    [Fact]
    public async Task GetMasterReleaseAsync()
    {
        // https://www.discogs.com/Emmanuel-Top-This-Is-A-Acid-Phase/master/36961
        var masterId = 36961;

        var response = await Client.Database.GetMasterReleaseAsync(masterId);

        response.Id.Should().Be(masterId);
        response.ResourceUrl.Should().Be("https://api.discogs.com/masters/36961");
        response.DataQuality.Should().Be(DataQuality.Correct);
        response.Title.Should().Be("This Is A...? / Acid Phase");
        response.Year.Should().Be(1994);
        response.MainRelease.Should().Be(8310);
        response.MainReleaseUrl.Should().Be("https://api.discogs.com/releases/8310");
        response.Genres.Should().Contain("Electronic");
        response.Styles.Should().Contain("Techno", "Acid");

        response.Artists.Should().HaveCount(1);
        response.Artists.First().Id.Should().Be(1240);
        response.Artists.First().Name.Should().Be("Emmanuel Top");
        response.Artists.First().ResourceUrl.Should().Be("https://api.discogs.com/artists/1240");

        response.Videos.Should().NotBeEmpty();
        response.Images.Should().NotBeEmpty();
        response.VersionsUrl.Should().Be("https://api.discogs.com/masters/36961/versions");

        // Tracks

        response.Tracks.Should().BeEquivalentTo(
            new List<Track>
            {
                new Track { Position = "A", Duration = "5:20", Type = "track", Title = "This Is A...?" },
                new Track { Position = "B", Duration = "5:15", Type = "track", Title = "Acid Phase" },
            });
    }

    [Fact]
    public async Task GetReleaseAsync()
    {
        // https://www.discogs.com/Emmanuel-Top-This-Is-A-Acid-Phase/release/8310
        var releaseId = 8310;

        var response = await Client.Database.GetReleaseAsync(releaseId);

        response.Id.Should().Be(releaseId);
        response.Uri.Should().Be("https://www.discogs.com/release/8310-Emmanuel-Top-This-Is-A-Acid-Phase");
        response.ResourceUrl.Should().Be("https://api.discogs.com/releases/8310");
        response.Title.Should().Be("This Is A...? / Acid Phase");
        response.Year.Should().Be(1994);
        response.MasterId.Should().Be(36961);
        response.Country.Should().Be("France");
        response.FormatQuantity.Should().Be(1);
        response.Genres.Should().Contain("Electronic");
        response.Styles.Should().Contain("Techno", "Acid");
        response.Released.Should().Be("1994-11-10");
        response.ReleasedFormatted.Should().Be("10 Nov 1994");
        response.Status.Should().Be("Accepted");
        response.Videos.Should().NotBeEmpty();
        response.Images.Should().NotBeEmpty();
        response.Companies.Should().NotBeEmpty();

        // Artists

        response.Artists.Should().HaveCount(1);

        var artist = response.Artists.First();
        artist.Id.Should().Be(1240);
        artist.ResourceUrl.Should().Be("https://api.discogs.com/artists/1240");
        artist.Name.Should().Be("Emmanuel Top");

        // Tracks

        response.Tracks.Should().BeEquivalentTo(
            new List<Track>
            {
                new Track { Position = "A", Duration = "5:20", Type = "track", Title = "This Is A...?" },
                new Track { Position = "B", Duration = "5:15", Type = "track", Title = "Acid Phase" },
            });

        // Labels

        response.Labels.Should().HaveCount(1);

        var label = response.Labels.First();
        label.Id.Should().Be(285);
        label.ResourceUrl.Should().Be("https://api.discogs.com/labels/285");
        label.Name.Should().Be("Attack Records");
        label.Catno.Should().Be("ATT-V-94 003");
        label.EntityType.Should().Be("1");
        label.EntityTypeName.Should().Be("Label");
    }

    [Fact]
    public async Task FormattedArtist()
    {
        var releaseId = 6163259;

        var response = await Client.Database.GetReleaseAsync(releaseId);

        // Track

        var track = response.Tracks[35];
        track.Position.Should().Be("2-18");
        track.GetFormattedArtists().Should().Be("Craig Mack Feat. Busta Rhymes, LL Cool J, Notorious B.I.G. & Rampage (2)");
    }
}
