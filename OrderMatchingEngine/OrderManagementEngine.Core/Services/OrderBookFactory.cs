using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public class OrderBookFactory : IOrderBookFactory
    {
        public OrderBookFactory()
        {
        }

        public OrderBook CreateOrderBook()
        {
            return new OrderBook();
        }
    }
}