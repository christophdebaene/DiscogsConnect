using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Test
{
    [Collection("DiscogsClient")]
    public class GetReleaseTest
    {
        protected IDiscogsClient Client { get; private set; }

        public GetReleaseTest(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }

        [Fact]
        public void SearchValidRelease_ExpectData()
        {
            // Act
            var response = Client.GetRelease(1017350).Result;

            // Assert

            response.Id.Should().Be(1017350);
            response.ResourceUrl.Should().Be("https://api.discogs.com/releases/1017350");
            response.Uri.Should().Be("https://www.discogs.com/Various-Serious-Beats-55/release/1017350");
            response.Status.Should().Be("Accepted");
            response.DataQuality.Should().Be(DataQuality.NeedsVote);
            response.Styles.Should().ContainSingle("House");
            response.ReleasedFormatted.Should().Be("25 Jun 2007");
            response.Labels.Should().NotBeEmpty();
            response.Released.Should().Be("2007-06-25");
            response.Formats.Should().NotBeEmpty();
            response.FormatQuantity.Should().Be(3);
            response.Year.Should().Be(2007);
            response.Images.Should().NotBeEmpty();
            response.Genres.Should().ContainSingle("Electronic");
            response.Thumb.Should().NotBeNullOrWhiteSpace();
            response.ExtraArtists.Should().NotBeEmpty();
            response.Title.Should().Be("Serious Beats 55");
            response.Country.Should().Be("Belgium");
            response.Notes.Should().NotBeNullOrWhiteSpace();
            response.Identifiers.Should().NotBeEmpty();
            response.Companies.Should().NotBeEmpty();
            response.Artists.Should().NotBeEmpty();
            response.Tracks.Should().NotBeEmpty();
        }
    }
}