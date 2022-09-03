namespace DiscogsConnect;

public class Entity : Resource
{
    public string Name { get; set; }
    public int EntityType { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Catno { get; set; }
    public string EntityTypeName { get; set; }
}
