namespace DiscogsConnect
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DebugDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = base.SendAsync(request, cancellationToken);
            var json = response.Result.Content.ReadAsStringAsync().Result;
            json = JsonUtils.PrettyPrint(json);
            return response;
        }
    }
}
