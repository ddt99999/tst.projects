using System.Threading.Tasks;

namespace PhoenixSea.Trading.Core.Services
{
    public interface IDataDownloadService
    {
        Task<string> DownloadAsync(string url);
    }
}
