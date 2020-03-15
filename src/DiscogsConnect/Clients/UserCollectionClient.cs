using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    internal class UserCollectionClient : IUserCollectionClient
    {
        private HttpClient _httpClient;
        
        public UserCollectionClient(HttpClient httpClient)
            => _httpClient = httpClient;        
        public async Task<FolderResponse> GetFoldersAsync(string username)        
            => await _httpClient.GetAsync<FolderResponse>($"users/{username}/collection/folders");        
        public async Task<Folder> GetFolderAsync(string username, int folderId)        
            => await _httpClient.GetAsync<Folder>($"users/{username}/collection/folders/{folderId}");        
        public async Task AddFolderAsync(string username, string folder)        
            => await _httpClient.PostAsync<object>($"users/{username}/collection/folders", new { folder });        
        public async Task DeleteFolderAsync(string username, int folderId)        
            => await _httpClient.DeleteAsync($"users/{username}/collection/folders/{folderId}");        
        public async Task<PaginationResponse<CollectionItem>> GetItemsByReleaseAsync(string username, int releaseId, int page = 1, int perPage = 100)
            => await _httpClient.GetPagedAsync<CollectionItem>($"users/{username}/collection/releases/{releaseId}", page, perPage);        
        public async Task<List<CollectionItem>> GetItemsByReleaseAllAsync(string username, int releaseId)
            => await _httpClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/releases/{releaseId}");        
        public async Task<PaginationResponse<CollectionItem>> GetItemsByFolderAsync(string username, int folderId, int page = 1, int perPage = 100)        
            => await _httpClient.GetPagedAsync<CollectionItem>($"users/{username}/collection/folders/{folderId}/releases", page, perPage);        
        public async Task<List<CollectionItem>> GetItemsByFolderAllAsync(string username, int folderId)
            => await _httpClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/folders/{folderId}/releases");        
        public async Task<AddToCollectionResponse> AddToFolderAsync(string username, int folderId, int releaseId)        
            => await _httpClient.PostAsync<object, AddToCollectionResponse>($"users/{username}/collection/folders/{folderId}/releases/{releaseId}");
        public async Task DeleteInstanceAsync(string username, int folderId, int releaseId, int instanceId)
            => await _httpClient.DeleteAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}");        
        public async Task<FieldsResponse> GetFieldsAsync(string username)
            => await _httpClient.GetAsync<FieldsResponse>($"users/{username}/collection/fields");        
        public async Task EditFieldAsync(string username, int folderId, int releaseId, int instanceId, int fieldId, string value)
            => await _httpClient.PostAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}/fields/{fieldId}", new { value });
        public async Task<CollectionValue> GetValueAsync(string username)
            => await _httpClient.GetAsync<CollectionValue>($"users/{username}/collection/value");
    }
}
