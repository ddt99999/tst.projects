namespace PhoenixSea.Common.Core.MarketData
{
    public interface ITimeSeriesData<out T> : IMarketData<T>, ITimeSeries
    {
    }
}