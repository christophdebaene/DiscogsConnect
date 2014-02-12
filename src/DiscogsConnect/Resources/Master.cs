namespace DiscogsConnect
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;

    public class Master : Resource
    {
        public string Title { get; set; }
        public string VersionsUrl { get; set; }        
        public string Uri { get; set; }        
        public int MainRelease { get; set; }        
        public string MainReleaseUrl { get; set; }        
        public int Year { get; set; }        
        public List<string> Styles { get; set; }        
        public List<string> Genres { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DataQuality DataQuality { get; set; }
        
        public List<Video> Videos { get; set; }
        public List<Artist> Artists { get; set; }        
        public List<Image> Images { get; set; }

        [JsonProperty("tracklist")]
        public List<Track> Tracks { get; set; }

        public class Artist : Resource
        {                       
            public string Join { get; set; }
            public string Tracks { get; set; }
            public string Role { get; set; }
            public string Anv { get; set; }
            public string Name { get; set; }            
        }
    }
   
    public class MasterVersion : Resource
    {            
        public string Title { get; set; }
        public string Status { get; set; }
        public string Thumb { get; set; }
        public string Country { get; set; }
        public string Format { get; set; }
        public string Label { get; set; }
        public string Released { get; set; }
        public string Catno { get; set; }
    }
}
