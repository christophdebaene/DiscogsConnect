using System.Collections.Generic;
using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect;

internal class UserWantlistClient(IRestClient restClient) : IUserWantlistClient
{
    public async Task<PaginationResponse<Wants>> GetAsync(string username)
        => await restClient.GetAsync<PaginationResponse<Wants>>($"users/{username}/wants");
    public async Task<List<Wants>> GetAllAsync(string username)
        => await restClient.GetAllPagesAsync<Wants>($"users/{username}/wants");
    public async Task<Wants> AddAsync(string username, int releaseId, string notes, int rating = 0)
        => await restClient.PutAsync<Wants>($"users/{username}/wants/{releaseId}", null, new { notes, rating });
    public async Task<Wants> UpdateAsync(string username, int releaseId, string notes, int rating = 0)
        => await restClient.PostAsync<Wants>($"users/{username}/wants/{releaseId}", null, new { notes, rating });
    public async Task RemoveAsync(string username, int releaseId)
        => await restClient.DeleteAsync($"users/{username}/wants/{releaseId}");
}
