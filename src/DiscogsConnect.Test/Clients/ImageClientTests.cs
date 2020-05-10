using DiscogsConnect.Test;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace DiscogsConnect.Clients
{
    [Collection("DiscogsClient")]
    public class ImageClientTests
    {
        protected IDiscogsClient Client { get; }
        public ImageClientTests(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }

        [Fact]
        public async Task GetAsync()
        {
            var imageUrl = "https://img.discogs.com/P8O9BmDeLn0cGSmGUyvd3ThO4l0=/600x848/smart/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/A-45-1414874002-9310.jpeg.jpg";

            var response = await Client.Image.GetAsync(imageUrl);
            response.Should().NotBeNullOrEmpty();
        }
    }
}