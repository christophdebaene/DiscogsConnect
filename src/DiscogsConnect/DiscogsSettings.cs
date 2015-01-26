namespace DiscogsConnect
{
    using System;

    public class DiscogsSettings
    {        
        public string UserAgent { get; private set; }
        public string Key { get; private set; }
        public string Secret { get; private set; }

        public DiscogsSettings(string userAgent, string key, string secret)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                throw new ArgumentNullException("userAgent");

            UserAgent = userAgent;
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            Key = key;

            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentNullException("secret");

            Secret = secret;
        }
    }
}
