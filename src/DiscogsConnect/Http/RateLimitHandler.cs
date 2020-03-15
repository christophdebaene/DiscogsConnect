using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsConnect.Http
{
    public class RateLimitHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {            
            var response = await base.SendAsync(request, cancellationToken);
            var rateLimit = RateLimit.Parse(response);

            Console.WriteLine($"R: Used:{rateLimit.Total} - Remaining:{rateLimit.Remaining} - Total:{rateLimit.Total}");

            if (rateLimit.Remaining < 2)
                Thread.Sleep(TimeSpan.FromSeconds(90));
            
            return response;
        }
    }
}