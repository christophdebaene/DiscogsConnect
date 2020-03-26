using System.Threading.Tasks;

namespace DiscogsConnect
{
    public interface IDatabaseClient
    {
        Task<Release> GetReleaseAsync(int id, Currency currency = Currency.NONE);
        Task<Artist> GetArtistAsync(int id);
        Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int id, int page = 1, int perPage = 100);
        Task<Master> GetMasterReleaseAsync(int id);
        Task<PaginationResponse<SearchResult>> SearchAsync(SearchCriteria criteria, int page = 1, int perPage = 100);
    }
}