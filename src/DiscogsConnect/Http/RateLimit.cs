using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DiscogsConnect.Http;

public class RateLimit
{
    public int? Total { get; set; }
    public int? Used { get; set; }
    public int? Remaining { get; set; }
    public static RateLimit Parse(HttpResponseMessage response)
    {
        return new RateLimit
        {
            Total = GetHeaderValueAsIntSafe(response, "X-Discogs-Ratelimit"),
            Used = GetHeaderValueAsIntSafe(response, "X-Discogs-Ratelimit-Used"),
            Remaining = GetHeaderValueAsIntSafe(response, "X-Discogs-Ratelimit-Remaining"),
        };
    }
    private static int? GetHeaderValueAsIntSafe(HttpResponseMessage response, string key)
    {
        if (response.Headers.TryGetValues(key, out IEnumerable<string> values))
        {
            if (int.TryParse(values.FirstOrDefault(), out int value))
                return value;
        }

        return null;
    }
}
