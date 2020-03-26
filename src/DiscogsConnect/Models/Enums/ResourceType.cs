using System.Runtime.Serialization;

namespace DiscogsConnect
{
    public enum ResourceType
    {
        [EnumMember(Value = "release")]
        Release,

        [EnumMember(Value = "master")]
        Master,

        [EnumMember(Value = "artist")]
        Artist,

        [EnumMember(Value = "label")]
        Label
    }
}