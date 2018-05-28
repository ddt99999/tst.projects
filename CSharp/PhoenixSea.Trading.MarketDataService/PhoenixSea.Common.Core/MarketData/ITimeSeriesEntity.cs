namespace PhoenixSea.Common.Core.MarketData
{
    public interface ITimeSeriesEntity : ITimeSeries
    {
        string ToCsv();
        string[] GetHeaders();
    }
}