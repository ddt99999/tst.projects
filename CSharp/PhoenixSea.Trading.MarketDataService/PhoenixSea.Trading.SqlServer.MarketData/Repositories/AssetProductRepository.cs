using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Context;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.SqlServer.MarketData.Repositories
{
    public class AssetProductRepository : GenericRepository<MarketDataDbContext, AssetProduct>, IAssetProductRepository
    {
        public AssetProductRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }


    }
}