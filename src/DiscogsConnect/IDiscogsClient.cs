namespace DiscogsConnect
{
    using System.Threading.Tasks;

    public interface IDiscogsClient
    {
        Task<Artist> GetArtistAsync(int artistId);
        Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int artistId, int page = 1, int perPage = 50);

        Task<Release> GetReleaseAsync(int releaseId);

        Task<Master> GetMasterReleaseAsync(int masterReleaseId);
        Task<PaginationResponse<MasterVersion>> GetMasterVersionAsync(int masterReleaseId, int page = 1, int perPage = 50);

        Task<Label> GetLabelAsync(int labelId);
        Task<PaginationResponse<LabelRelease>> GetLabelReleaseAsync(int labelId, int page = 1, int perPage = 50);

        Task<byte[]> GetImageAsync(string filename);

        Task<PaginationResponse<SearchResult>> SearchAsync(string searchString);
        Task<PaginationResponse<SearchResult>> SearchAsync(string searchString, ResourceType searchType);
    }
}
