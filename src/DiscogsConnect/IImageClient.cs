using System.Threading.Tasks;

namespace DiscogsConnect;
public interface IImageClient
{
    Task<byte[]> GetAsync(string uri);
}
