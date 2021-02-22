using System.Collections.Generic;
using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect
{
    internal class UserCollectionClient : IUserCollectionClient
    {
        private readonly IRestClient _restClient;
        public UserCollectionClient(IRestClient restClient)
            => _restClient = restClient;
        public async Task<FolderCollection> GetFoldersAsync(string username)
            => await _restClient.GetAsync<FolderCollection>($"users/{username}/collection/folders");
        public async Task<Folder> GetFolderAsync(string username, int folderId)
            => await _restClient.GetAsync<Folder>($"users/{username}/collection/folders/{folderId}");
        public async Task<Folder> AddFolderAsync(string username, string name)
            => await _restClient.PostAsync<Folder>($"users/{username}/collection/folders", null, new { name });
        public async Task DeleteFolderAsync(string username, int folderId)
            => await _restClient.DeleteAsync($"users/{username}/collection/folders/{folderId}");
        public async Task<PaginationResponse<CollectionItem>> GetItemsByReleaseAsync(string username, int releaseId, int page = 1, int per_page = 100)
            => await _restClient.GetAsync<PaginationResponse<CollectionItem>>($"users/{username}/collection/releases/{releaseId}", new { page, per_page });

        public async Task<List<CollectionItem>> GetItemsByReleaseAllAsync(string username, int releaseId)
            => await _restClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/releases/{releaseId}");
        public async Task<PaginationResponse<CollectionItem>> GetItemsByFolderAsync(string username, int folderId, int page = 1, int per_page = 100)
            => await _restClient.GetAsync<PaginationResponse<CollectionItem>>($"users/{username}/collection/folders/{folderId}/releases", new { page, per_page });
        public async Task<List<CollectionItem>> GetItemsByFolderAllAsync(string username, int folderId)
            => await _restClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/folders/{folderId}/releases");
        public async Task<AddToCollectionResponse> AddToFolderAsync(string username, int folderId, int releaseId)
            => await _restClient.PostAsync<AddToCollectionResponse>($"users/{username}/collection/folders/{folderId}/releases/{releaseId}");
        public async Task DeleteInstanceAsync(string username, int folderId, int releaseId, int instanceId)
            => await _restClient.DeleteAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}");
        public async Task<FieldCollection> GetFieldsAsync(string username)
            => await _restClient.GetAsync<FieldCollection>($"users/{username}/collection/fields");
        public async Task EditFieldAsync(string username, int folderId, int releaseId, int instanceId, int fieldId, string value)
            => await _restClient.PostAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}/fields/{fieldId}", null, new { value });
        public async Task<CollectionValue> GetValueAsync(string username)
            => await _restClient.GetAsync<CollectionValue>($"users/{username}/collection/value");
    }
}