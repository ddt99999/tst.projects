using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public interface IOrderBookRepository
    {
        void AddOrderBook(Order order);
        bool RemoveOrder(Order order);
        OrderBook GetOrderBook(Order order);
        OrderBook GetOrderBook(Asset asset);
    }
}