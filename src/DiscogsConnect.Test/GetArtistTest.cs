namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using Xunit;
    
    public class GetArtistTest
    {
        [Fact]
        public void GetArtist_ValidIdentifier_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();
            
            // Act
            var response = client.GetArtist(45);
            
            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(45);
            response.ResourceUrl.Should().Be("http://api.discogs.com/artists/45");

            response.Profile.Should().NotBeNullOrEmpty();
            response.ReleasesUrl.Should().Be("http://api.discogs.com/artists/45/releases");
            response.Name.Should().Be("Aphex Twin");
            response.NameVariations.Should().NotBeEmpty();
            response.Uri.Should().Be("http://www.discogs.com/artist/Aphex+Twin");
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();
            response.Aliases.Should().NotBeEmpty();
            //response.DataQuality.Should().Be(DataQuality.Correct);
            response.Realname.Should().Be("Richard David James");
        }

        [Fact]
        public void GetArtistReleases_ValidIdentifier_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetArtistReleases(45);
            
            // Assert            
            response.Should().NotBeNull();

            response.Pagination.PerPage.Should().Be(50);
            response.Pagination.Items.Should().BeGreaterThan(500);
            response.Pagination.Page.Should().Be(1);
            response.Pagination.Pages.Should().BeGreaterThan(10);

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(50);
        }

        [Fact]
        public void GetArtistReleasesPaging()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetArtistReleasesAsync(45).Result;

            // Assert               
            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();
        }
    }
}
