using System;

namespace PhoenixSea.Trading.Core.Services
{
    public interface IMarketDataService<T> where T : class 
    {
        void Execute(string url);
        void Execute(string urlFormat, string ticker, DateTime startTime, DateTime endTime, string apiKey);
    }
}