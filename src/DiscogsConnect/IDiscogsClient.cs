namespace DiscogsConnect;
public interface IDiscogsClient
{
    IDatabaseClient Database { get; }
    IImageClient Image { get; }
    IUserIdentityClient UserIdentity { get; }
    IUserCollectionClient UserCollection { get; }
    IUserWantlistClient UserWantlist { get; }
}
