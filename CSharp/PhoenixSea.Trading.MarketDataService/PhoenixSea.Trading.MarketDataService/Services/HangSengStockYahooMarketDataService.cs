using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixSea.Trading.Models.Yahoo;
using PhoenixSea.Trading.Core.Services;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public class HangSengStockYahooMarketDataService : YahooMarketDataService<Stock>, IHangSengStockMarketDataService
    {
        public HangSengStockYahooMarketDataService(
            IDataDownloadService dataDownloadService, 
            IDataProcessingService dataProcessingService) 
            : base(dataDownloadService, dataProcessingService)
        {
        }

        public override void HandleData(List<Stock> data)
        {
            
        }
    }
}
