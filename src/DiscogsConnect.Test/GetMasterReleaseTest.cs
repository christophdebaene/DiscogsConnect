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

            response.Videos.Should().HaveCount(x => x >= 9);
            response.Videos.Should().NotBeEmpty();
            response.Title.Should().NotBeNullOrEmpty();
            response.MainRelease.Should().Be(32662);
            response.Artists.Should().NotBeEmpty();
          
            response.Year.Should().Be(1992);
            response.Images.Should().NotBeEmpty();

            var sampleImage = response.Images.First();

            sampleImage.Uri.Should().NotBeNullOrEmpty();
            sampleImage.Height.Should().BePositive();
            sampleImage.Width.Should().BePositive();
            sampleImage.ResourceUrl.Should().NotBeNullOrEmpty();
            sampleImage.Type.Should().NotBeNull();
            sampleImage.Uri150.Should().NotBeNullOrEmpty();

            response.Tracks.Should().NotBeEmpty();

            var sampleTrack = response.Tracks.First();

            sampleTrack.Duration.Should().NotBeNullOrEmpty();
            sampleTrack.Position.Should().NotBeNullOrEmpty();
            sampleTrack.Title.Should().NotBeNullOrEmpty();
            
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
        }
    }
}
