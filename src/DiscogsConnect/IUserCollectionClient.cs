using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    public interface IUserCollectionClient
    {
        Task<FolderCollection> GetFoldersAsync(string username);
        Task<Folder> GetFolderAsync(string username, int folderId);
        Task<Folder> AddFolderAsync(string username, string name);
        Task DeleteFolderAsync(string username, int folderId);
        Task<PaginationResponse<CollectionItem>> GetItemsByReleaseAsync(string username, int releaseId, int page = 1, int perPage = 100);
        Task<List<CollectionItem>> GetItemsByReleaseAllAsync(string username, int releaseId);
        Task<PaginationResponse<CollectionItem>> GetItemsByFolderAsync(string username, int folderId, int page = 1, int perPage = 100);
        Task<List<CollectionItem>> GetItemsByFolderAllAsync(string username, int folderId);
        Task<AddToCollectionResponse> AddToFolderAsync(string username, int folderId, int releaseId);
        Task DeleteInstanceAsync(string username, int folderId, int releaseId, int instanceId);
        Task<FieldCollection> GetFieldsAsync(string username);
        Task EditFieldAsync(string username, int folderId, int releaseId, int instanceId, int fieldId, string value);
        Task<CollectionValue> GetValueAsync(string username);
    }
}