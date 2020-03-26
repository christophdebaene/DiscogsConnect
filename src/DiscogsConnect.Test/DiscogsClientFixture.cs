using Xunit;

namespace DiscogsConnect.Test
{
    public class DiscogsClientFixture
    {
        private const string UserAgent = "DiscogsConnect/2.0";
        private const string Token = "<fill in>";
        public IDiscogsClient DiscogsClient { get; }
        public DiscogsClientFixture()
        {
            DiscogsClient = new DiscogsClient(new DiscogsOptions
            {
                UserAgent = UserAgent,
                Token = Token
            });
        }
    }

    [CollectionDefinition("DiscogsClient")]
    public class DiscogsClientCollection : ICollectionFixture<DiscogsClientFixture>
    {
    }
}