using FluentAssertions;
using System.Linq;
using Xunit;

namespace DiscogsConnect.Test
{

    [Collection("DiscogsClient")]
    public class GetArtistTest
    {
        protected IDiscogsClient Client { get; private set; }

        public GetArtistTest(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }
        
        [Fact]
        public void GetArtist_ValidIdentifier_ExpectData()
        {
            // Act
            var response = Client.GetArtist(45).Result;

            // Assert
            response.Should().NotBeNull();
            response.Profile.Should().NotBeNullOrWhiteSpace();
            response.ReleasesUrl.Should().Be("https://api.discogs.com/artists/45/releases");
            response.Name.Should().Be("Aphex Twin");
            response.NameVariations.Should().NotBeEmpty();
            response.Uri.Should().Be("https://www.discogs.com/artist/45-Aphex-Twin");
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();
            response.ResourceUrl.Should().Be("https://api.discogs.com/artists/45");
            response.Aliases.Should().NotBeEmpty();
            response.Id.Should().Be(45);
            //response.DataQuality.Should().Be(DataQuality.Correct);
            response.Realname.Should().Be("Richard David James");
        }
        
        [Fact]
        public void GetArtistReleases_ValidIdentifier_ExpectData()
        {
            // Act
            var response = Client.GetArtistReleases(1360).Result;

            // Assert
            response.Should().NotBeNull();

            response.Pagination.PerPage.Should().Be(50);
            response.Pagination.Items.Should().BeGreaterThan(500);
            response.Pagination.Page.Should().Be(1);
            response.Pagination.Pages.Should().BeGreaterThan(10);

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(50);

            var artistRelease = response.Items.FirstOrDefault();
            artistRelease.Should().NotBeNull();
            artistRelease.Thumb.Should().NotBeNullOrWhiteSpace();            
        }
    }
}