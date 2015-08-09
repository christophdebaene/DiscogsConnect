using DiscogsConnect.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    public class DiscogsClient : IDiscogsClient
    {
        readonly HttpClient _client;
        public static readonly Uri DiscogsApiUrl = new Uri("https://api.discogs.com");

        private static IEnumerable<MediaTypeFormatter> Formatters
        {
            get
            {
                yield return new JsonMediaTypeFormatter
                {
                    SerializerSettings = Serialization.DiscogsSerializerSettings.Default
                };
            }
        }

        public DiscogsClient(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                throw new ArgumentNullException("userAgent");

            var httpClientFactory = new Http.HttpClientFactory();
            _client = httpClientFactory.Create(new HttpClientOptions
            {
                UserAgent = userAgent
            });
        }

        public DiscogsClient(string userAgent, string key, string secret)
        {            
            if (string.IsNullOrEmpty(userAgent))
                throw new ArgumentNullException("userAgent");

            if (string.IsNullOrEmpty(userAgent))
                throw new ArgumentNullException("key");

            if (string.IsNullOrEmpty(userAgent))
                throw new ArgumentNullException("secret");

            var httpClientFactory = new Http.HttpClientFactory();
            _client = httpClientFactory.Create(new HttpClientOptions
            {
                UserAgent = userAgent,
                Authentication = new HttpClientOptions.Credentials(key, secret)
            });
        }
        
        async Task<T> GetTypeAsync<T>(Uri uri)
        {
            return await _client
               .GetAsync(uri)
               .ContinueWith(x =>
               {
                   if (!x.Result.IsSuccessStatusCode)
                       throw new DiscogsException(string.Format("{0} ({1})", x.Result.StatusCode, x.Result.ReasonPhrase));
                   else return x.Result.Content.ReadAsAsync<T>(Formatters);
               })
               .Unwrap();
        }

        public async Task<Artist> GetArtist(int artistId)
        {
            return await GetTypeAsync<Artist>(ApiUrls.Artist(artistId));
        }

        public async Task<PaginationResponse<ArtistRelease>> GetArtistReleases(int artistId, int page = 1, int perPage = 50)
        {
            return await GetTypeAsync<PaginationResponse<ArtistRelease>>(ApiUrls.ArtistReleases(artistId, page, perPage));
        }

        public async Task<Release> GetRelease(int releaseId)
        {
            return await GetTypeAsync<Release>(ApiUrls.Release(releaseId));
        }

        public async Task<Master> GetMasterRelease(int masterReleaseId)
        {
            return await GetTypeAsync<Master>(ApiUrls.MasterRelease(masterReleaseId));
        }

        public async Task<PaginationResponse<MasterVersion>> GetMasterVersion(int masterReleaseId, int page = 1, int perPage = 50)
        {
            return await GetTypeAsync<PaginationResponse<MasterVersion>>(ApiUrls.MasterReleaseVersions(masterReleaseId, page, perPage));
        }

        public async Task<Label> GetLabel(int labelId)
        {
            return await GetTypeAsync<Label>(ApiUrls.Label(labelId));
        }

        public async Task<PaginationResponse<LabelRelease>> GetLabelRelease(int labelId, int page = 1, int perPage = 50)
        {
            return await GetTypeAsync<PaginationResponse<LabelRelease>>(ApiUrls.LabelReleases(labelId, page, perPage));
        }

        public async Task<byte[]> GetImage(string uri)
        {
            return await _client.GetByteArrayAsync(uri);
        }

        public async Task<PaginationResponse<SearchResult>> Search(SearchCriteria criteria)
        {
            return await GetTypeAsync<PaginationResponse<SearchResult>>(ApiUrls.Search(criteria));
        }
    }
}