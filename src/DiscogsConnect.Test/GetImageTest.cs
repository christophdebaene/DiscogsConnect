using Xunit;
using FluentAssertions;

namespace DiscogsConnect.Test
{
    [Collection("DiscogsClient")]
    public class GetImageTest
    {
        protected IDiscogsClient Client { get; private set; }

        public GetImageTest(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }
        
        [Fact]
        public void GetImage_ValidIdentifier_ExpectData()
        {
            // Arrange

            var imageUrl = "https://api-img.discogs.com/LqbmoS-I4oG4VfG8i_yHryqKVUM=/fit-in/599x526/filters:strip_icc():format(jpeg):mode_rgb():quality(96)/discogs-images/R-1017350-1269167729.jpeg.jpg";

            // Act

            var imageData = Client.GetImage(imageUrl).Result;

            // Assert

            imageData.Should().NotBeNullOrEmpty();
        }
    }
}