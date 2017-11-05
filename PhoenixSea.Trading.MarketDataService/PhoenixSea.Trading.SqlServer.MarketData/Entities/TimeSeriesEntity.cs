using System;
using PhoenixSea.Common.Data;

namespace PhoenixSea.Trading.SqlServer.MarketData.Entities
{
    public class TimeSeriesEntity : EntityBase
    {
        public DateTime Timestamp { get; set; }
    }
}