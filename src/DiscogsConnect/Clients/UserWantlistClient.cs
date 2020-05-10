using DiscogsConnect.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    public class UserWantlistClient : IUserWantlistClient
    {
        private readonly IRestClient _restClient;
        public UserWantlistClient(IRestClient restClient)
            => _restClient = restClient;
        public async Task<PaginationResponse<Wants>> GetAsync(string username)
            => await _restClient.GetAsync<PaginationResponse<Wants>>($"users/{username}/wants");
        public async Task<List<Wants>> GetAllAsync(string username)
            => await _restClient.GetAllPagesAsync<Wants>($"users/{username}/wants");
        public async Task<Wants> AddAsync(string username, int releaseId, string notes, int rating = 0)
            => await _restClient.PutAsync<Wants>($"users/{username}/wants/{releaseId}", null, new { notes, rating });
        public async Task<Wants> UpdateAsync(string username, int releaseId, string notes, int rating = 0)
            => await _restClient.PostAsync<Wants>($"users/{username}/wants/{releaseId}", null, new { notes, rating });
        public async Task RemoveAsync(string username, int releaseId)
            => await _restClient.DeleteAsync($"users/{username}/wants/{releaseId}");
    }
}