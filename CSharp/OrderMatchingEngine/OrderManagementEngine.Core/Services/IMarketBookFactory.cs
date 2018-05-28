using System.Collections;
using System.Collections.Generic;
using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public interface IMarketBookFactory
    {
        IDictionary<Asset, OrderBook> CreateMarketBook();
    }
}