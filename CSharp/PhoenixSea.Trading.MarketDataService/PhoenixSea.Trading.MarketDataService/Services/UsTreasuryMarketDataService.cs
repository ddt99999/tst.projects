using System.Collections.Generic;
using PhoenixSea.Trading.Core.Services;
using PhoenixSea.Trading.Models.Quandl;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public class UsTreasuryMarketDataService : MarketDataService<TreasuryRealLongTermRate>, IUsTreasuryMarketDataService
    {
        private readonly IUsTreasuryDataService _usTreasuryDataService;

        public UsTreasuryMarketDataService(
            IDataDownloadService dataDownloadService, 
            IDataProcessingService dataProcessingService,
            IUsTreasuryDataService usTreasuryDataService) 
            : base(dataDownloadService, dataProcessingService)
        {
            _usTreasuryDataService = usTreasuryDataService;
        }

        public override void HandleData(List<TreasuryRealLongTermRate> data)
        {
            _usTreasuryDataService.InsertUsTreasuryRealLongTermRate(data);
        }
    }
}