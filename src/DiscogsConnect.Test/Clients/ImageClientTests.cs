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
        public async Task GetImageAsync()
        {
            var imageUrl = "https://api-img.discogs.com/LqbmoS-I4oG4VfG8i_yHryqKVUM=/fit-in/599x526/filters:strip_icc():format(jpeg):mode_rgb():quality(96)/discogs-images/R-1017350-1269167729.jpeg.jpg";

            var response = await Client.Image.GetImageAsync(imageUrl);
            response.Should().NotBeNullOrEmpty();
        }
    }
}