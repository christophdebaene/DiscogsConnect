using System.Net.Http;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal class DatabaseClient : IDatabaseClient
    {
        private readonly HttpClient _httpClient;
        public DatabaseClient(HttpClient httpClient)
            => _httpClient = httpClient;
        public async Task<Release> GetReleaseAsync(int id, Currency currency = Currency.NONE)
            => await _httpClient.GetAsync<Release>($"releases/{id}", new[] { ("curr_abbr", currency.ToString()) });
        public async Task<ReleaseRating> GetReleaseRatingAsync(int id)
            => await _httpClient.GetAsync<ReleaseRating>($"releases/{id}/rating");
        public async Task<Artist> GetArtistAsync(int id)
            => await _httpClient.GetAsync<Artist>($"artists/{id}");
        public async Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int id, int page = 1, int perPage = 100)
            => await _httpClient.GetPagedAsync<ArtistRelease>($"artists/{id}/releases", page, perPage);
        public async Task<Master> GetMasterReleaseAsync(int id)
            => await _httpClient.GetAsync<Master>($"masters/{id}");
        public async Task<PaginationResponse<SearchResult>> SearchAsync(SearchCriteria criteria, int page = 1, int perPage = 100)
            => await _httpClient.GetPagedAsync<SearchResult>("database/search", page, perPage, criteria.Parameters());
    }
}