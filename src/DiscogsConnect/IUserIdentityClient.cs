using System.Threading.Tasks;

namespace DiscogsConnect;
public interface IUserIdentityClient
{
    Task<UserIdentity> GetUserAsync(string username);
    Task<SubmissionsPaginationResponse> GetSubmissionsAsync(string username, int page = 1, int per_page = 100);
}
