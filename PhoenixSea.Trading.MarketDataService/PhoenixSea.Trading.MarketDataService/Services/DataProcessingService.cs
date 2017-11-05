using System.Collections.Generic;
using FileHelpers;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        public List<T> ProcessData<T>(string input) where T : class 
        {
            var engine = new FileHelperEngine<T>();
            var items = engine.ReadStringAsList(input);
            return items;
        }
    }
}