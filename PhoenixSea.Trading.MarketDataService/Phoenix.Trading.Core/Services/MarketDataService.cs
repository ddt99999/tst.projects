using System;
using System.Collections.Generic;

namespace PhoenixSea.Trading.Core.Services
{
    public abstract class MarketDataService<T> : IMarketDataService<T> where T : class
    {
        private readonly IDataDownloadService _dataDownloadService;
        private readonly IDataProcessingService _dataProcessingService;

        public MarketDataService(
            IDataDownloadService dataDownloadService,
            IDataProcessingService dataProcessingService)
        {
            _dataDownloadService = dataDownloadService;
            _dataProcessingService = dataProcessingService;
        }

        public void Execute(string url)
        {
            try
            {
                var result = _dataDownloadService.DownloadAsync(url).Result;
                var data = _dataProcessingService.ProcessData<T>(result);
                HandleData(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public virtual void Execute(string urlFormat, string ticker, DateTime startTime, DateTime endTime, string apiKey)
        { 
        }

        public abstract void HandleData(List<T> data);
    }
}