using System;
using PhoenixSea.Common.Data;

namespace PhoenixSea.Trading.SqlServer.MarketData.Entities
{
    public class UsTreasuryData : EntityBase
    {
        public DateTime Timestamp { get; set; }
        public AssetProduct AssetProduct { get; set; }
        public double? LongTermRealAverageRate { get; set; }
    }
}