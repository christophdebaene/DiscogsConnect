namespace DiscogsConnect
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;

    public class Release : Resource
    {                
        public string Title { get; set; }
        public string Uri { get; set; }
        public string Status { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DataQuality DataQuality { get; set; }
        
        public int MasterId { get; set; }
        public string MasterUrl { get; set; }        
        public string Country { get; set; }
        public int Year { get; set; }
        public string Released { get; set; }
        public string ReleasedFormatted { get; set; }
        public string Notes { get; set; }

        public List<string> Styles { get; set; }
        public List<string> Genres { get; set; }
        public List<Label> Labels { get; set; }
        public List<Company> Companies { get; set; }

        [JsonProperty("extraartists")]
        public List<Artist> ExtraArtists { get; set; }

        public List<Video> Videos { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Format> Formats { get; set; }
        public List<Image> Images { get; set; }

        [JsonProperty("tracklist")]
        public List<Track> Tracks { get; set; }

        public List<Identifier> Identifiers { get; set; }
        
        public string Thumb { get; set; }

        public class Label : Resource
        {
            public string Name { get; set; }
            public int EntityType { get; set; }
            public string Catno { get; set; }
            public string EntityTypeName { get; set; }            
        }

        public class Artist : Resource
        {            
            public string Tracks { get; set; }         
            public string Role { get; set; }            
            public string Anv { get; set; }            
            public string Join { get; set; }            
            public string Name { get; set; }            
        }

        public class Company : Resource
        {            
            public string EntityType { get; set; }         
            public string Catno { get; set; }            
            public string EntityTypeName { get; set; }
            public string Name { get; set; }            
        }

        public class Identifier
        {
            public string Type { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
        }

        public class Format
        {            
            public List<string> Descriptions { get; set; }
            public string Name { get; set; }
            public string Qty { get; set; }            
        }
    }    
}
