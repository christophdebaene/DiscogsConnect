using System.Collections.Generic;

namespace DiscogsConnect;
public class Community
{
    public int Have { get; set; }
    public int Want { get; set; }
    public DataQuality DataQuality { get; set; }
    public string Status { get; set; }
    public Rating Rating { get; set; }
    public Contributor Submitter { get; set; }
    public List<Contributor> Contributors { get; set; }

    public class Contributor
    {
        public string Username { get; set; }
        public string ResourceUrl { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}
public class Rating
{
    public int Count { get; set; }
    public float Average { get; set; }
}
