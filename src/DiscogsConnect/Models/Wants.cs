using System;
using System.Collections.Generic;

namespace DiscogsConnect;
public class Wants : Resource
{
    public int Rating { get; set; }
    public Information BasicInformation { get; set; }
    public string Notes { get; set; }
    public DateTime DateAdded { get; set; }
    public class Information : Resource
    {
        public List<string> Styles { get; set; }
        public List<Entity> Labels { get; set; }
        public int Year { get; set; }
        public string MasterUrl { get; set; }
        public List<Release.Artist> Artists { get; set; }
        public List<string> Genres { get; set; }
        public string Thumb { get; set; }
        public string Title { get; set; }
        public List<Format> Formats { get; set; }
        public string CoverImage { get; set; }
        public int? MasterId { get; set; }
    }
}
