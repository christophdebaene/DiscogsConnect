namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;

    public class GetLabelTest
    {
        [Fact]
        public void GetValidLabel_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetLabel(1);
            
            // Assert
            response.Should().NotBeNull();

            response.Profile.Should().NotBeNullOrEmpty();
            response.Name.Should().NotBeNullOrEmpty();
            response.ContactInfo.Should().NotBeNullOrEmpty();
            response.Sublabels.Should().NotBeEmpty();
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();

            var sampleImage = response.Images.First();

            sampleImage.Type.Should().NotBeNull();
            sampleImage.Width.Should().BeGreaterThan(0);
            sampleImage.Height.Should().BeGreaterThan(0);
            sampleImage.Uri.Should().NotBeNullOrEmpty();
            sampleImage.Uri150.Should().NotBeNullOrEmpty();
            
            response.Id.Should().Be(1);
            response.DataQuality.Should().Be(DataQuality.Correct);
        }

        [Fact]
        public void GetValidLabelRelease_ExpectData()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetLabelRelease(1);

            // Assert
            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();
        }
    }
}
