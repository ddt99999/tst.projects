using PhoenixSea.Trading.Core.Services;
using PhoenixSea.Trading.Models.Quandl;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public interface IUsTreasuryMarketDataService : IMarketDataService<TreasuryRealLongTermRate>
    {
        
    }
}