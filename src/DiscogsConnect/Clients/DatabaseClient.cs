using System.Collections.Generic;
using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect;
internal class DatabaseClient(IRestClient restClient) : IDatabaseClient
{
    public async Task<Release> GetReleaseAsync(int id, Currency currency = Currency.NONE)
        => await restClient.GetAsync<Release>($"releases/{id}", new { curr_abbr = currency.ToString() });
    public async Task<ReleaseRating> GetReleaseRatingAsync(int id)
        => await restClient.GetAsync<ReleaseRating>($"releases/{id}/rating");
    public async Task<Artist> GetArtistAsync(int id)
        => await restClient.GetAsync<Artist>($"artists/{id}");
    public async Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int id, int page = 1, int per_page = 100)
        => await restClient.GetAsync<PaginationResponse<ArtistRelease>>($"artists/{id}/releases", new { page, per_page });
    public async Task<List<ArtistRelease>> GetArtistReleasesAllAsync(int id)
        => await restClient.GetAllPagesAsync<ArtistRelease>($"artists/{id}/releases");
    public async Task<Master> GetMasterReleaseAsync(int id)
        => await restClient.GetAsync<Master>($"masters/{id}");
    public async Task<PaginationResponse<SearchResult>> SearchAsync(SearchCriteria criteria)
        => await restClient.GetAsync<PaginationResponse<SearchResult>>("database/search", criteria);
}
