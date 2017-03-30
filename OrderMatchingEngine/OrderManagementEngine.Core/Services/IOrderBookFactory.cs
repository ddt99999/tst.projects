using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public interface IOrderBookFactory
    {
        OrderBook CreateOrderBook();
    }
}