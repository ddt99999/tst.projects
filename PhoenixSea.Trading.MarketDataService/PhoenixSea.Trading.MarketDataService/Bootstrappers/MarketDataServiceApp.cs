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
        private readonly IDataDownloadService _dataDownloadService;
        private readonly IDataProcessingService _dataProcessingService;
        private readonly IDataStoringService _dataStoringService;

        public MarketDataServiceApp(
            IDataDownloadService dataDownloadService,
            IDataProcessingService dataProcessingService,
            IDataStoringService dataStoringService)
        {
            _dataDownloadService = dataDownloadService;
            _dataProcessingService = dataProcessingService;
            _dataStoringService = dataStoringService;
        }
        public void Start()
        {
            try
            {
                //var result = _dataDownloadService.DownloadAsync("https://www.quandl.com/api/v3/datasets/USTREASURY/LONGTERMRATES.json?order=asc&api_key=boBvGKEvQEdotMqzUv8X").Result;
                ////var result = "{\"dataset\":{\"id\":12428526,\"dataset_code\":\"LONGTERMRATES\",\"database_code\":\"USTREASURY\",\"name\":\"Treasury Long Term Rates\",\"refreshed_at\":\"2017-10-30T22:00:30.218Z\",\"newest_available_date\":\"2017-10-27\",\"oldest_available_date\":\"2000-01-03\",\"column_names\":[\"Date\",\"LT Composite \\u003e 10 Yrs\",\"Treasury 20-Yr CMT\",\"Extrapolation Factor\"]}}";
                //var treasuryLongTermRateData = _jsonSerializer.Deserialize<TreasuryLongTermRateDataSetBase>(result);
                //_usTreasuryRepository.Insert(result);

                var result = _dataDownloadService.DownloadAsync("https://www.quandl.com/api/v3/datasets/USTREASURY/REALLONGTERM.csv?order=asc&api_key=boBvGKEvQEdotMqzUv8X").Result;
                var treasuryRealLongTermRates = _dataProcessingService.ProcessData<TreasuryRealLongTermRate>(result);
                _dataStoringService.InsertUsTreasuryRealLongTermRate(treasuryRealLongTermRates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void Stop()
        {
        }
    }
}