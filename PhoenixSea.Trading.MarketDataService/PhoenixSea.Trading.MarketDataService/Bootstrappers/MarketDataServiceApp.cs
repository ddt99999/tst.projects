using System;
using PhoenixSea.Trading.MarketDataService.Services;
using PhoenixSea.Trading.Models;
using PhoenixSea.Trading.Models.Quandl;
using PhoenixSea.Trading.NoSql.Redis;
using PhoenixSea.Trading.Utils.Interfaces;
using PhoenixSea.Trading.Utils.Serialization;

namespace PhoenixSea.Trading.MarketDataService.Bootstrappers
{
    public class MarketDataServiceApp : IApplication
    {
        private readonly IUsTreasuryMarketDataService _usTreasuryMarketDataService;
        private readonly IHangSengStockMarketDataService _hangSengStockMarketDataService;

        public MarketDataServiceApp(
            IUsTreasuryMarketDataService usTreasuryMarketDataService,
            IHangSengStockMarketDataService hangSengStockMarketDataService)
        {
            _usTreasuryMarketDataService = usTreasuryMarketDataService;
            _hangSengStockMarketDataService = hangSengStockMarketDataService;
        }
        public void Start()
        {
            //_usTreasuryMarketDataService.Execute("https://www.quandl.com/api/v3/datasets/USTREASURY/REALLONGTERM.csv?order=asc&api_key=boBvGKEvQEdotMqzUv8X");
            _hangSengStockMarketDataService.Execute("https://query1.finance.yahoo.com/v7/finance/download/{0}?period1={1}&period2={2}&interval=1d&events=history&crumb={3}", "0001.HK", new DateTime(2000, 1, 1), DateTime.Today, "ovkmm9fm7tS");
        }

        public void Stop()
        {
        }
    }
}