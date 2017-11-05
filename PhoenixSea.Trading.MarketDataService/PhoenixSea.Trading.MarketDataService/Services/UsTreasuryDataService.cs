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
    public class UsTreasuryDataService : IUsTreasuryDataService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUsTreasuryDataRepository _usTreasuryDataRepository;
        private readonly IAssetProductRepository _assetProductRepository;

        public UsTreasuryDataService(
            IDbContextScopeFactory dbContextScopeFactory,
            IUsTreasuryDataRepository usTreasuryDataRepository,
            IAssetProductRepository assetProductRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _usTreasuryDataRepository = usTreasuryDataRepository;
            _assetProductRepository = assetProductRepository;
        }

        public void InsertUsTreasuryRealLongTermRate(List<TreasuryRealLongTermRate> treasuryRealLongTermRates)
        {
            using (var scope = _dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadUncommitted))
            {
                var assetProduct = _assetProductRepository.FindAll().Single(x => x.Name.Equals("US Treasury"));

                var usTreasuryDatas = _usTreasuryDataRepository.FindAll().Where(x => x.AssetProduct.Id == assetProduct.Id).ToList();

                var usTreasuryCache = new ConcurrentDictionary<DateTime, UsTreasuryData>();

                foreach (var treasuryData in usTreasuryDatas)
                {
                    usTreasuryCache[treasuryData.Timestamp] = treasuryData;
                }

                foreach (var treasuryRealLongTermRate in treasuryRealLongTermRates)
                {
                    if (usTreasuryCache.ContainsKey(treasuryRealLongTermRate.Timestamp))
                        continue;

                    var usTreasuryData = new UsTreasuryData
                    {
                        Timestamp = treasuryRealLongTermRate.Timestamp,
                        AssetProduct = assetProduct,
                        LongTermRealAverageRate = treasuryRealLongTermRate.LongTermRealAverageRate
                    };
                    _usTreasuryDataRepository.Insert(usTreasuryData);
                }

                scope.SaveChanges();
            }
        }
    }
}