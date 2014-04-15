namespace DiscogsConnect
{
    using System.Threading.Tasks;

    public interface IDiscogsClient
    {
        Task<Artist> GetArtist(int artistId);
        Task<PaginationResponse<ArtistRelease>> GetArtistReleases(int artistId, int page = 1, int perPage = 50);

        Task<Release> GetRelease(int releaseId);

        Task<Master> GetMasterRelease(int masterReleaseId);
        Task<PaginationResponse<MasterVersion>> GetMasterVersion(int masterReleaseId, int page = 1, int perPage = 50);

        Task<Label> GetLabel(int labelId);
        Task<PaginationResponse<LabelRelease>> GetLabelRelease(int labelId, int page = 1, int perPage = 50);

        Task<byte[]> GetImage(string filename);

        Task<PaginationResponse<SearchResult>> Search(string searchString);
        Task<PaginationResponse<SearchResult>> Search(string searchString, ResourceType searchType);
    }
}