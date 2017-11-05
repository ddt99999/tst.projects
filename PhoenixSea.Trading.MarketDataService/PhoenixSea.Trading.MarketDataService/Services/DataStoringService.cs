using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.Models.Quandl;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;
using PhoenixSea.Trading.SqlServer.MarketData.Repositories;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public class DataStoringService : IDataStoringService
    {
        private readonly IUsTreasuryDataService _usTreasuryDataService;

        public DataStoringService(
            IUsTreasuryDataService usTreasuryDataService)
        {
            _usTreasuryDataService = usTreasuryDataService;
        }

        public void InsertUsTreasuryRealLongTermRate(List<TreasuryRealLongTermRate> treasuryRealLongTermRates)
        {
            _usTreasuryDataService.InsertUsTreasuryRealLongTermRate(treasuryRealLongTermRates);
        }
    }
}