namespace PhoenixSea.Common.Core.MarketData
{
    public interface IMarketData<out T>
    {
        T Data { get; }
    }
}