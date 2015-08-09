using FluentAssertions;
using System.Linq;
using Xunit;

namespace DiscogsConnect.Test
{
    [Collection("DiscogsClient")]
    public class SearchTest
    {
        protected IDiscogsClient Client { get; private set; }
    
        public SearchTest(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }

        [Fact]
        public void SearchByReleaseType_ShouldOnlyContainReleaseType()
        {
            // Act

            var criteria = new SearchCriteria
            {
                Type = ResourceType.Release,
                Title = "Serious Beats 55"
            };

            var response = Client.Search(criteria).Result;

            // Assert
            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(1);

            var release = response.Items.Cast<ReleaseSearchResult>().First();

            release.Should().NotBeNull();
            release.Id.Should().Be(1017350);
            release.ResourceUrl.Should().Be("https://api.discogs.com/releases/1017350");
            release.Type.Should().Be(ResourceType.Release);
            release.Title.Should().Be("Various - Serious Beats 55");
            release.Year.Should().Be(2007);
            release.Catno.Should().Be("541416 501825");
            release.Genres.Should().ContainSingle("Electronic");
            release.Styles.Should().ContainSingle("House");
        }        
    }
}