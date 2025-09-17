using System.Collections.Generic;
using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect;
internal class UserCollectionClient(IRestClient restClient) : IUserCollectionClient
{
    public async Task<FolderCollection> GetFoldersAsync(string username)
        => await restClient.GetAsync<FolderCollection>($"users/{username}/collection/folders");
    public async Task<Folder> GetFolderAsync(string username, int folderId)
        => await restClient.GetAsync<Folder>($"users/{username}/collection/folders/{folderId}");
    public async Task<Folder> AddFolderAsync(string username, string name)
        => await restClient.PostAsync<Folder>($"users/{username}/collection/folders", null, new { name });
    public async Task DeleteFolderAsync(string username, int folderId)
        => await restClient.DeleteAsync($"users/{username}/collection/folders/{folderId}");
    public async Task<PaginationResponse<CollectionItem>> GetItemsByReleaseAsync(string username, int releaseId, int page = 1, int per_page = 100)
        => await restClient.GetAsync<PaginationResponse<CollectionItem>>($"users/{username}/collection/releases/{releaseId}", new { page, per_page });
    public async Task<List<CollectionItem>> GetItemsByReleaseAllAsync(string username, int releaseId)
        => await restClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/releases/{releaseId}");
    public async Task<PaginationResponse<CollectionItem>> GetItemsByFolderAsync(string username, int folderId, int page = 1, int per_page = 100)
        => await restClient.GetAsync<PaginationResponse<CollectionItem>>($"users/{username}/collection/folders/{folderId}/releases", new { page, per_page });
    public async Task<List<CollectionItem>> GetItemsByFolderAllAsync(string username, int folderId)
        => await restClient.GetAllPagesAsync<CollectionItem>($"users/{username}/collection/folders/{folderId}/releases");
    public async Task<AddToCollectionResponse> AddToFolderAsync(string username, int folderId, int releaseId)
        => await restClient.PostAsync<AddToCollectionResponse>($"users/{username}/collection/folders/{folderId}/releases/{releaseId}");
    public async Task DeleteInstanceAsync(string username, int folderId, int releaseId, int instanceId)
        => await restClient.DeleteAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}");
    public async Task<FieldCollection> GetFieldsAsync(string username)
        => await restClient.GetAsync<FieldCollection>($"users/{username}/collection/fields");
    public async Task EditFieldAsync(string username, int folderId, int releaseId, int instanceId, int fieldId, string value)
        => await restClient.PostAsync($"users/{username}/collection/folders/{folderId}/releases/{releaseId}/instances/{instanceId}/fields/{fieldId}", null, new { value });
    public async Task<CollectionValue> GetValueAsync(string username)
        => await restClient.GetAsync<CollectionValue>($"users/{username}/collection/value");
}
