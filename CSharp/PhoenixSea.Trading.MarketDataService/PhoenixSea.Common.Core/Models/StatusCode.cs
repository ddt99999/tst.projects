using ProtoBuf;

namespace PhoenixSea.Common.Core.Models
{
    [ProtoContract(Name = "StatusCode")]
    public enum StatusCode
    {
        [ProtoEnum(Name = "HEARTBEAT", Value = 0)] HeartBeat,
        [ProtoEnum(Name = "UNKNOWN", Value = 1)] Unknown,
        [ProtoEnum(Name = "INITIALISING", Value = 2)] Initialising,
        [ProtoEnum(Name = "TRADING", Value = 3)] Trading,
        [ProtoEnum(Name = "NOT_TRADING", Value = 4)] NonTrading,
        [ProtoEnum(Name = "FATAL", Value = 5)] Fatal,
        [ProtoEnum(Name = "DELAYED", Value = 6)] Delayed
    }
}