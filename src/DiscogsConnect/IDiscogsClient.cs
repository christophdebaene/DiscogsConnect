namespace DiscogsConnect
{
    public interface IDiscogsClient
    {
        IDatabaseClient Database { get; }
        IImageClient Image { get; }
        IUserCollectionClient UserCollection { get; }
    }   
}