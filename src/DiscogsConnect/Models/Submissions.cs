using System.Collections.Generic;

namespace DiscogsConnect;
public class Submissions
{
    public List<Release> Releases { get; set; }
    public List<Entity> Labels { get; set; }    
    public List<Release.Artist> Artists { get; set; }            
}
