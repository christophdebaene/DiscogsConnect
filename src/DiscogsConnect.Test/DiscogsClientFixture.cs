using Xunit;

namespace DiscogsConnect.Test
{
    public class DiscogsClientFixture
    {
        const string UserAgent = "DiscogsConnect/2.0";
        const string Key = "<fill in>";
        const string Secret = "<fill in>";
        
        public IDiscogsClient DiscogsClient { get; private set; }

        public DiscogsClientFixture()
        {
            DiscogsClient = new DiscogsClient(UserAgent, Key, Secret);            
        }
    }

    [CollectionDefinition("DiscogsClient")]
    public class DiscogsClientCollection : ICollectionFixture<DiscogsClientFixture>
    {
    }
}