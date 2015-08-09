namespace DiscogsConnect.Http
{
    public class HttpClientOptions
    {
        public string UserAgent { get; set; }        
        public Credentials Authentication { get; set; }

        public class Credentials
        {
            public string Key { get; private set; }
            public string Secret { get; private set; }
            public Credentials(string key, string secret)
            {
                Key = key;
                Secret = secret;
            }
        }
        
        public bool HasCredentials
        {
            get
            {
                return this.Authentication != null &&
                    !string.IsNullOrWhiteSpace(this.Authentication.Key) &&
                    !string.IsNullOrWhiteSpace(this.Authentication.Secret);
            }
        }
    }
}