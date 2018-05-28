using PhoenixSea.Common.Data;
using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Context;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.SqlServer.MarketData.Repositories
{
    public class UsTreasuryDataRepository : GenericRepository<MarketDataDbContext, UsTreasuryData>, IUsTreasuryDataRepository
    {
        public UsTreasuryDataRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }
    }
}