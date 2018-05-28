using System;

namespace PhoenixSea.Common.Core.MarketData
{
    public interface ITimeSeries
    {
        DateTime TimeStamp { get; }
    }
}