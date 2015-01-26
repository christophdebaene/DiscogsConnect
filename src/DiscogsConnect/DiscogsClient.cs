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
        private const string BASE_URL = "https://api.discogs.com";
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
        
        public DiscogsClient(DiscogsSettings settings)
        {

#if DEBUG
            client = HttpClientFactory.Create(new DebugDelegatingHandler());            
#else
            client = new HttpClient();
#endif

            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", settings.UserAgent);
            client.DefaultRequestHeaders.Add("Authorization", 
                string.Format("Discogs key={0}, secret={1}", settings.Key, settings.Secret));
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
             
        public Task<Artist> GetArtist(int artistId)
        {
            return GetTypeAsync<Artist>(ApiUrls.Artist(artistId));
        }
           
        public Task<PaginationResponse<ArtistRelease>> GetArtistReleases(int artistId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<ArtistRelease>>(ApiUrls.ArtistReleases(artistId, page, perPage));
        }
        
        public Task<Release> GetRelease(int releaseId)
        {            
            return GetTypeAsync<Release>(ApiUrls.Release(releaseId));                
        }

        public Task<Master> GetMasterRelease(int masterReleaseId)
        {            
            return GetTypeAsync<Master>(ApiUrls.MasterRelease(masterReleaseId));                
        }

        public Task<PaginationResponse<MasterVersion>> GetMasterVersion(int masterReleaseId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<MasterVersion>>(ApiUrls.MasterReleaseVersions(masterReleaseId, page, perPage));
        }
      
        public Task<Label> GetLabel(int labelId)
        {
            return GetTypeAsync<Label>(ApiUrls.Label(labelId));               
        }

        public Task<PaginationResponse<LabelRelease>> GetLabelRelease(int labelId, int page = 1, int perPage = 50)
        {
            return GetTypeAsync<PaginationResponse<LabelRelease>>(ApiUrls.LabelReleases(labelId, page, perPage));
        }

        public Task<byte[]> GetImage(string filename)
        {
            return GetTypeAsync<byte[]>(ApiUrls.Image(filename));
        }

        public Task<PaginationResponse<SearchResult>> Search(string searchString)
        {
            return GetTypeAsync<PaginationResponse<SearchResult>>(ApiUrls.Search(searchString));
        }

        public Task<PaginationResponse<SearchResult>> Search(string searchString, ResourceType searchType)
        {
            return GetTypeAsync<PaginationResponse<SearchResult>>(ApiUrls.Search(searchString, searchType));
        }             
    }   
}