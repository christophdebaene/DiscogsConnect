namespace DiscogsConnect
{    
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class DiscogsClient : IDiscogsClient
    {
        private readonly HttpClient client;
        
        static IEnumerable<MediaTypeFormatter> Formatters
        {
            get
            {
                var formatter = new JsonMediaTypeFormatter();
                formatter.SerializerSettings.Converters.Add(new SearchResourceConverter());
                formatter.SerializerSettings.ContractResolver = new DiscogsContractResolver();                
                yield return formatter;
            }
        }

        public DiscogsClient() : this("http://api.discogs.com")
        {
        }

        public DiscogsClient(string baseAddress)
        {

#if DEBUG
            client = HttpClientFactory.Create(new DebugDelegatingHandler());            
#else
            client = new HttpClient();
#endif

            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
            client.DefaultRequestHeaders.Add("User-Agent", "DiscogsConnect/2.0");
        }

        Task<T> GetTypeAsync<T>(Uri uri)
        {
            return client
               .GetAsync(uri)
               .ContinueWith(x =>
               {
                   if (!x.Result.IsSuccessStatusCode)
                       throw new DiscogsException(string.Format("{0} ({1})", x.Result.StatusCode, x.Result.ReasonPhrase));
                   else return x.Result.Content.ReadAsAsync<T>(Formatters);
               })
               .Unwrap();
        }
             
        public Task<Artist> GetArtistAsync(int artistId)
        {
            return GetTypeAsync<Artist>(ApiUrls.Artist(artistId));
        }
           
        public Task<PaginationResponse<ArtistRelease>> GetArtistReleasesAsync(int artistId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<ArtistRelease>>(ApiUrls.ArtistRelease(artistId, page, perPage));
        }
        
        public Task<Release> GetReleaseAsync(int releaseId)
        {            
            return GetTypeAsync<Release>(ApiUrls.Release(releaseId));                
        }

        public Task<Master> GetMasterReleaseAsync(int masterReleaseId)
        {            
            return GetTypeAsync<Master>(ApiUrls.MasterRelease(masterReleaseId));                
        }

        public Task<PaginationResponse<MasterVersion>> GetMasterVersionAsync(int masterReleaseId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<MasterVersion>>(ApiUrls.MasterVersion(masterReleaseId, page, perPage));
        }
      
        public Task<Label> GetLabelAsync(int labelId)
        {
            return GetTypeAsync<Label>(ApiUrls.Label(labelId));               
        }

        public Task<PaginationResponse<LabelRelease>> GetLabelReleaseAsync(int labelId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<LabelRelease>>(ApiUrls.LabelRelease(labelId, page, perPage));
        }

        public Task<byte[]> GetImageAsync(string filename)
        {
            return GetTypeAsync<byte[]>(ApiUrls.Image(filename));
        }

        public Task<PaginationResponse<SearchResult>> SearchAsync(string searchString)
        {
            return GetTypeAsync<PaginationResponse<SearchResult>>(ApiUrls.Search(searchString));
        }

        public Task<PaginationResponse<SearchResult>> SearchAsync(string searchString, ResourceType searchType)
        {
            return GetTypeAsync<PaginationResponse<SearchResult>>(ApiUrls.Search(searchString, searchType));
        }             
    }   
}