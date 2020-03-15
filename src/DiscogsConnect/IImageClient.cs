using System.Threading.Tasks;

namespace DiscogsConnect
{
    public interface IImageClient
    {        
        Task<byte[]> GetImageAsync(string uri);
    }
}