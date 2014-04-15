namespace DiscogsConnect.Test
{    
    using FluentAssertions;
    using Xunit;

    public class GetReleaseTest
    {       
        [Fact]
        public void SearchValidRelease_ExpectData()
        {             
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.GetRelease(1).Result;
            
            // Assert
            response.Status.Should().Be("Accepted");
            response.Styles.Should().NotBeEmpty();
            response.ReleasedFormatted.Should().Be("Mar 1999");
            response.Labels.Should().NotBeEmpty();
            response.Released.Should().Be("1999-03-00");
            response.MasterUrl.Should().Be("http://api.discogs.com/masters/5427");
            response.Year.Should().Be(1999);
            response.Images.Should().NotBeEmpty();
            response.Id.Should().Be(1);
            response.Genres.Should().NotBeEmpty();
            response.Thumb.Should().NotBeNullOrWhiteSpace();
            response.ExtraArtists.Should().NotBeEmpty();
            response.Title.Should().Be("Stockholm");
            response.Country.Should().Be("Sweden");
            response.Notes.Should().NotBeNullOrWhiteSpace();
            response.Identifiers.Should().NotBeEmpty();
            response.Companies.Should().NotBeEmpty();
            response.Uri.Should().Be("http://www.discogs.com/Persuader-Stockholm/release/1");
            response.Artists.Should().NotBeEmpty();
            response.Formats.Should().NotBeEmpty();
            response.ResourceUrl.Should().Be("http://api.discogs.com/releases/1");
            response.MasterId.Should().Be(5427);
            response.Tracks.Should().NotBeEmpty();
            response.DataQuality.Should().Be(DataQuality.CompleteAndCorrect);
        }
    }
}
