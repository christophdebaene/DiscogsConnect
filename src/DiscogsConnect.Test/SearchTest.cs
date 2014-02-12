namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;
        
    public class SearchTest
    {
        [Fact]
        public void SearchByArtistType_ShouldOnlyContainArtistType()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.Search("Deadmau5");

            // Assert            
            response.Items.Should().NotBeEmpty();            
        }

        [Fact]
        public void SearchByReleaseType_ShouldOnlyContainRelease()
        {
            // Arrange
            var client = new DiscogsClient();

            // Act
            var response = client.Search("Deadmau5", ResourceType.Release);

            // Assert
            response.Items.Should().NotBeEmpty();
            response.Items.Where(
                x => x.Type != ResourceType.Release).Should().BeEmpty();
        }       
    }
}