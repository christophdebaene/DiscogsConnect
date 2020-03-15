using System.Threading.Tasks;

namespace DiscogsConnect
{
    public interface IDatabaseClient
    {          
        Task<Artist> GetArtistAsync(int id);
        Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int id, int page = 1, int perPage = 100);    
        Task<Release> GetReleaseAsync(int id);
        Task<Master> GetMasterReleaseAsync(int id);
    }
}