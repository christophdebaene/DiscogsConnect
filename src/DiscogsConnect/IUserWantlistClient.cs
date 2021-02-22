using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscogsConnect
{
    public interface IUserWantlistClient
    {
        Task<PaginationResponse<Wants>> GetAsync(string username);
        Task<List<Wants>> GetAllAsync(string username);
        Task<Wants> AddAsync(string username, int releaseId, string notes, int rating = 0);
        Task<Wants> UpdateAsync(string username, int releaseId, string notes, int rating = 0);
        Task RemoveAsync(string username, int releaseId);
    }
}