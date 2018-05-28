using System.Collections.Generic;
using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.Services
{
    public class OrderBookRepository : IOrderBookRepository
    {
        private readonly IOrderBookFactory orderBookFactory;
        private readonly IDictionary<Asset, OrderBook> marketBook;

        public OrderBookRepository(IOrderBookFactory orderBookFactory, IDictionary<Asset, OrderBook> marketBook)
        {
            this.orderBookFactory = orderBookFactory;
            this.marketBook = marketBook;// DIContainer.Resolve<IDictionary<Asset, OrderBook>>();
        }

        public void AddOrderBook(Order order)
        {
            OrderBook orderBook;
            // create a new market and its orderbook
            if (marketBook.TryGetValue(order.Asset, out orderBook))
                return;

            orderBook = orderBookFactory.CreateOrderBook();
            marketBook.Add(order.Asset, orderBook);
            //orderBook.AddOrder(order);
        }

        public bool RemoveOrder(Order order)
        {
            OrderBook orderBook;
            if (marketBook.TryGetValue(order.Asset, out orderBook))
            {
                orderBook.CancelOrder(order);
                return true;
            }

            // order not found
            return false;
        }

        public OrderBook GetOrderBook(Order order)
        {
            return GetOrderBook(order.Asset);
        }

        public OrderBook GetOrderBook(Asset asset)
        {
            OrderBook orderBook;
            return marketBook.TryGetValue(asset, out orderBook) ? orderBook : null;
        }
    }
}