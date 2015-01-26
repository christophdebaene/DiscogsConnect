namespace DiscogsConnect
{
    using System;
    using System.Globalization;

    internal static class ApiUrls
    {
        public static Uri Release(int releaseId)
        {
            return "releases/{0}".FormatUri(releaseId);
        }

        public static Uri MasterRelease(int masterId)
        {
            return "masters/{0}".FormatUri(masterId);
        }

        public static Uri MasterReleaseVersions(int masterId, int page = 1, int perPage = 50)
        {
            return "masters/{0}/versions?page={1}&per_page={2}".FormatUri(masterId, page, perPage);
        }

        public static Uri Artist(int artistId)
        {
            return "artists/{0}".FormatUri(artistId);
        }

        public static Uri ArtistReleases(int artistId, int page = 1, int perPage = 50)
        {         
            return "artists/{0}/releases?page={1}&per_page={2}".FormatUri(artistId, page, perPage);
        }
                  
        public static Uri Label(int labelId)
        {
            return "labels/{0}".FormatUri(labelId);
        }

        public static Uri LabelReleases(int labelId, int page = 1, int perPage = 50)
        {
            return "labels/{0}/releases?page={1}&per_page={2}".FormatUri(labelId, page, perPage);
        }

        public static Uri Search(string searchString)
        {
            return "database/search?q={0}".FormatUri(searchString);
        }

        public static Uri Search(string searchString, ResourceType searchType)
        {
            return "database/search?q={0}&type={1}".FormatUri(searchString, searchType.ToString().ToLower());
        }

        public static Uri Image(string filename)
        {
            return "images/{0}".FormatUri(filename);        
        }             

        static Uri FormatUri(this string pattern, params object[] args)
        {           
            return new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);
        }
    }
}