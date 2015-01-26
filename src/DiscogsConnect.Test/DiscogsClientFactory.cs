namespace DiscogsConnect.Test
{
    public static class DiscogsClientFactory
    {
        public static IDiscogsClient Create()
        {
            return new DiscogsClient(new DiscogsSettings(
                "DiscogsConnect/2.0", 
                "<enter key>",
                "<enter secret>"));
        }
    }
}