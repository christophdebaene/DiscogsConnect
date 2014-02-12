namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;
    
    public class GetMasterReleaseTest
    {
        [Fact]
        public void SearchValidMasterRelease_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetMasterRelease(565);
            
            // Assert
            response.Should().NotBeNull();

            response.Styles.Should().NotBeEmpty();
            response.Genres.Should().NotBeEmpty();
            response.Videos.Should().NotBeEmpty();
            response.Title.Should().Be("Selected Ambient Works 85-92");
            response.MainRelease.Should().Be(32662);
            response.MainReleaseUrl.Should().Be("http://api.discogs.com/releases/32662");
            response.Year.Should().Be(1992);
            response.Uri.Should().Be("http://www.discogs.com/Aphex-Twin-Selected-Ambient-Works-85-92/master/565");
            response.VersionsUrl.Should().Be("http://api.discogs.com/masters/565/versions");
            response.Artists.Should().NotBeEmpty();                      
            response.Images.Should().NotBeEmpty();
            response.ResourceUrl.Should().Be("http://api.discogs.com/masters/565");
            response.Tracks.Should().NotBeEmpty();                        
            response.Id.Should().Be(565);
            response.DataQuality.Should().Be(DataQuality.Correct);
        }

        [Fact]
        public void SearchValidMasterReleaseVersion_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetMasterVersion(8471);

            // Assert
            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();

            response.Pagination.PerPage.Should().Be(50);
            response.Pagination.Items.Should().BeGreaterThan(100);
            response.Pagination.Page.Should().Be(1);
            response.Pagination.Pages.Should().BeGreaterThan(2);

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(50);

            var masterVersion = response.Items.SingleOrDefault(x => x.Id == 400591);
            masterVersion.Should().NotBeNull();
            masterVersion.Status.Should().Be("Accepted");
            masterVersion.Thumb.Should().NotBeNullOrWhiteSpace();
            masterVersion.Title.Should().Be("Back In Black");
            masterVersion.Country.Should().Be("Australia");
            masterVersion.Format.Should().Be("LP, Album");
            masterVersion.Label.Should().Be("Albert Productions");
            masterVersion.Released.Should().Be("1980-07-25");
            masterVersion.Catno.Should().Be("APLP 046");
            masterVersion.ResourceUrl.Should().Be("http://api.discogs.com/releases/400591");
        }
    }
}
