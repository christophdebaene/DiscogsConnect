namespace DiscogsConnect
{
    using System.Runtime.Serialization;

    public enum DataQuality
    {
        [EnumMember(Value="Needs Vote")]
        NeedsVote,
        [EnumMember(Value = "Correct")]
        Correct
    }
}
