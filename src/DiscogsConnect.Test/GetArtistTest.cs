namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;
    
    public class GetArtistTest
    {
        [Fact]
        public void GetArtist_ValidIdentifier_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();
            
            // Act
            var response = client.GetArtist(45).Result;
            
            // Assert
            response.Should().NotBeNull();
            response.Profile.Should().NotBeNullOrWhiteSpace();
            response.ReleasesUrl.Should().Be("http://api.discogs.com/artists/45/releases");
            response.Name.Should().Be("Aphex Twin");
            response.NameVariations.Should().NotBeEmpty();
            response.Uri.Should().Be("http://www.discogs.com/artist/45-Aphex-Twin");
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();
            response.ResourceUrl.Should().Be("http://api.discogs.com/artists/45");
            response.Aliases.Should().NotBeEmpty();
            response.Id.Should().Be(45);
            response.DataQuality.Should().Be(DataQuality.Correct);
            response.Realname.Should().Be("Richard David James");                                                                                                                        
        }

        [Fact]
        public void GetArtistReleases_ValidIdentifier_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetArtistReleases(45).Result;
            
            // Assert            
            response.Should().NotBeNull();

            response.Pagination.PerPage.Should().Be(50);
            response.Pagination.Items.Should().BeGreaterThan(500);
            response.Pagination.Page.Should().Be(1);
            response.Pagination.Pages.Should().BeGreaterThan(10);

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(50);

            var artistRelease = response.Items.SingleOrDefault(x => x.Id == 258478);
            artistRelease.Should().NotBeNull();
            artistRelease.Thumb.Should().NotBeNullOrWhiteSpace();
            artistRelease.Artist.Should().Be("Aphex Twin");
            artistRelease.MainRelease.Should().Be(63114);
            artistRelease.Title.Should().Be("Analog Bubblebath Vol 2");
            artistRelease.Role.Should().Be("Main");
            artistRelease.Year.Should().Be(1991);
            artistRelease.ResourceUrl.Should().Be("http://api.discogs.com/masters/258478");
            artistRelease.Type.Should().Be(ResourceType.Master);                        
        }
    }
}
