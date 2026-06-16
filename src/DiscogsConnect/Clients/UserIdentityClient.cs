using System.Threading.Tasks;
using DiscogsConnect.Http;

namespace DiscogsConnect;
internal class UserIdentityClient(IRestClient restClient) : IUserIdentityClient
{
    public async Task<UserIdentity> GetUserAsync(string username)
        => await restClient.GetAsync<UserIdentity>($"users/{username}");

    public async Task<SubmissionsPaginationResponse> GetSubmissionsAsync(string username, int page = 1, int per_page = 100)
        => await restClient.GetAsync<SubmissionsPaginationResponse>($"users/{username}/submissions", new { page, per_page });
}
