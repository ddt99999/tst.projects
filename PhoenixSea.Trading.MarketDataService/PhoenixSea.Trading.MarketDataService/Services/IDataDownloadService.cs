using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSea.Trading.MarketDataService.Services
{
    public interface IDataDownloadService
    {
        Task<string> DownloadAsync(string url);
    }
}
