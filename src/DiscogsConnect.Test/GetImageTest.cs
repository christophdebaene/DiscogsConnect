namespace DiscogsConnect.Test
{
    using FluentAssertions;
    using System.Linq;
    using Xunit;

    public class GetImageTest
    {
        [Fact]
        public void GetImage_ValidIdentifier_ExpectData()
        {
            // Arrange
            var client = DiscogsClientFactory.Create();
            
            // Act
            //var response = client.GetImage("R-1-1193812072.jpeg").Result;

            // Assert
            //response.Should().NotBeNull();
        }       
    }
}
