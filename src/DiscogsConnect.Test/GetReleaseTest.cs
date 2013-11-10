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
            var response = client.GetRelease(1);
            
            // Assert
            response.Styles.Should().NotBeEmpty();
            response.Videos.Should().NotBeEmpty();
            response.Labels.Should().NotBeEmpty();
            response.Year.Should().BeGreaterThan(0);            
            response.Artists.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();               
            response.Id.Should().Be(1);
            response.Genres.Should().NotBeEmpty();
            response.Thumb.Should().NotBeNullOrEmpty();
            response.ExtraArtists.Should().NotBeEmpty();                                                                  
            response.Title.Should().NotBeNullOrEmpty();
            response.MasterId.Should().Be(5427);
            response.Tracks.Should().NotBeEmpty();            
            response.Status.Should().NotBeNullOrEmpty();
            response.ReleasedFormatted.Should().NotBeNullOrEmpty();
            response.MasterUrl.Should().NotBeNullOrEmpty();
            response.Released.Should().NotBeNullOrEmpty();
            response.Country.Should().NotBeNullOrEmpty();
            response.Notes.Should().NotBeNullOrEmpty();
            response.Identifiers.Should().NotBeEmpty();
            response.Companies.Should().NotBeEmpty();
            response.Uri.Should().NotBeEmpty();
            response.Formats.Should().NotBeEmpty();
            response.DataQuality.Should().NotBeNull();
        }
    }
}
