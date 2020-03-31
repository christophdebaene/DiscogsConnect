using DiscogsConnect.Test;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DiscogsConnect.Clients
{
    [Collection("DiscogsClient")]
    public class DatabaseClientTests
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
            response.Id.Should().Be(45);
            response.Uri.Should().Be("https://www.discogs.com/artist/45-Aphex-Twin");
            response.ResourceUrl.Should().Be("https://api.discogs.com/artists/45");
            response.ReleasesUrl.Should().Be("https://api.discogs.com/artists/45/releases");
            response.Name.Should().Be("Aphex Twin");
            response.Realname.Should().Be("Richard David James");
            response.NameVariations.Should().Contain("Apex Twin", "Aphex Twins", "AphexTwin", "The Aphex Twin");
            response.Profile.Should().Contain("18 August 1971");
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();
            response.Aliases.Should().NotBeEmpty();
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
            response.Uri.Should().Be("https://www.discogs.com/Emmanuel-Top-This-Is-A-Acid-Phase/release/8310");
            response.ResourceUrl.Should().Be("https://api.discogs.com/releases/8310");
            response.DataQuality.Should().Be(DataQuality.Correct);
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
            label.EntityType.Should().Be(1);
            label.EntityTypeName.Should().Be("Label");
        }

        [Fact]
        public async Task SearchAsync()
        {
            var criteria = new SearchCriteria
            {
                Type = ResourceType.Release,
                Title = "Serious Beats 55"
            };

            var response = await Client.Database.SearchAsync(criteria);

            response.Should().NotBeNull();

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(1);

            var release = response.Items.Cast<ReleaseSearchResult>().First();

            release.Id.Should().Be(1017350);
            release.Uri.Should().Be("/Various-Serious-Beats-55/release/1017350");
            release.ResourceUrl.Should().Be("https://api.discogs.com/releases/1017350");
            release.Type.Should().Be(ResourceType.Release);
            release.Country.Should().Be("Belgium");
            release.Title.Should().Be("Various - Serious Beats 55");
            release.Year.Should().Be(2007);
            release.Catno.Should().Be("541416 501825");
            release.Genres.Should().ContainSingle("Electronic");
            release.Styles.Should().ContainSingle("House");
            release.Formats.Should().Contain("CD", "Compilation");
            release.Barcodes.Should().Contain("SABAM", "NEWS541416501825");
        }
    }
}