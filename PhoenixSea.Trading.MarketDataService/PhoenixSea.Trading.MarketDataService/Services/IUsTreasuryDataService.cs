using System.Collections.Generic;
using PhoenixSea.Trading.Models.Quandl;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public interface IUsTreasuryDataService
    {
        void InsertUsTreasuryRealLongTermRate(List<TreasuryRealLongTermRate> treasuryRealLongTermRates);
    }
}