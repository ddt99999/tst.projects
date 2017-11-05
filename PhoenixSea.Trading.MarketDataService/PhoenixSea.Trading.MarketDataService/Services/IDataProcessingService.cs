using System.Collections.Generic;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public interface IDataProcessingService
    {
        List<T> ProcessData<T>(string input) where T : class;
    }
}