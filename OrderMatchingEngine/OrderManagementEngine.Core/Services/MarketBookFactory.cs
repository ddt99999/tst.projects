using System.Collections.Concurrent;
using System.Collections.Generic;
using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public class MarketBookFactory : IMarketBookFactory
    {
        private IDictionary<Asset, OrderBook> marketBook; 
        public MarketBookFactory()
        {
            this.marketBook = new ConcurrentDictionary<Asset, OrderBook>();
        }
        public IDictionary<Asset, OrderBook> CreateMarketBook()
        {
            LoadMarketBook();
            return marketBook;
        }

        private void LoadMarketBook()
        {
            marketBook.Add(new Asset("MSFT"), new OrderBook());
            marketBook.Add(new Asset("GOOG"), new OrderBook());
            marketBook.Add(new Asset("FB"), new OrderBook());
        }
    }
}