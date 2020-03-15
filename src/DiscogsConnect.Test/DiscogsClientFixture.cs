using Xunit;

namespace DiscogsConnect.Test
{
    public class DiscogsClientFixture
    {
        const string UserAgent = "DiscogsConnect/2.0";
        const string Token = "<fill in>";
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