using System;

namespace PhoenixSea.Trading.Core.Services
{
    public abstract class YahooMarketDataService<T> : MarketDataService<T> where T : class 
    {
        protected YahooMarketDataService(
            IDataDownloadService dataDownloadService, 
            IDataProcessingService dataProcessingService) 
            : base(dataDownloadService, dataProcessingService)
        {
        }

        public override void Execute(string urlTemplate, string ticker, DateTime startTime, DateTime endTime, string apiKey)
        {
            try
            {
                var startTimeInSec = TimeSpan.FromTicks(startTime.Ticks).TotalSeconds - 62135596800;
                var endTimeInSec = TimeSpan.FromTicks(endTime.Ticks).TotalSeconds - 62135596800;
                var url = string.Format(urlTemplate, ticker, startTimeInSec, endTimeInSec, apiKey);
                Execute(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}