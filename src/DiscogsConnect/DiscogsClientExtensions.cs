namespace DiscogsConnect
{
    public static class DiscogsClientExtensions
    {
        public static Artist GetArtist(this IDiscogsClient client, int artistId)
        {
            return client.GetArtistAsync(artistId).Result;
        }

        public static PaginationResponse<ArtistRelease> GetArtistReleases(this IDiscogsClient client, int artistId, int page = 1, int perPage = 50)
        {
            return client.GetArtistReleasesAsync(artistId, page, perPage).Result;
        }

        public static Release GetRelease(this IDiscogsClient client, int releaseId)
        {
            return client.GetReleaseAsync(releaseId).Result;
        }

        public static Master GetMasterRelease(this IDiscogsClient client, int masterReleaseId)
        {
            return client.GetMasterReleaseAsync(masterReleaseId).Result;
        }

        public static PaginationResponse<MasterVersion> GetMasterVersion(this IDiscogsClient client, int masterReleaseId, int page = 1, int perPage = 50)
        {
            return client.GetMasterVersionAsync(masterReleaseId, page, perPage).Result;
        }

        public static Label GetLabel(this IDiscogsClient client, int labelId)
        {
            return client.GetLabelAsync(labelId).Result;
        }

        public static PaginationResponse<LabelRelease> GetLabelRelease(this IDiscogsClient client, int labelId, int page = 1, int perPage = 50)
        {
            return client.GetLabelReleaseAsync(labelId, page, perPage).Result;
        }

        public static PaginationResponse<SearchResult> Search(this IDiscogsClient client, string searchString)
        {
            return client.SearchAsync(searchString).Result;
        }

        public static PaginationResponse<SearchResult> Search(this IDiscogsClient client, string searchString, ResourceType resourceType)
        {
            return client.SearchAsync(searchString, resourceType).Result;
        }
    }
}
