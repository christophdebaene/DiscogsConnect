using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DiscogsConnect.Http
{
    public class HttpClientFactory : IHttpClientFactory
    {
        const string BASE_URL = "https://api.discogs.com";

        public HttpClient Create(HttpClientOptions options)
        {
            var client = System.Net.Http.HttpClientFactory.Create(CreateHandlers(options).ToArray());
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", options.UserAgent);

            if (options.HasCredentials)
            {                
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Discogs key={0}, secret={1}",
                    options.Authentication.Key, options.Authentication.Secret));
            }

            return client;
        }

        protected virtual IEnumerable<DelegatingHandler> CreateHandlers(HttpClientOptions options)
        {
            //return Enumerable.Empty<DelegatingHandler>();
            yield return new DebugDelegatingHandler();
        }
    }
}