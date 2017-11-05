using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;
using PhoenixSea.Trading.SqlServer.MarketData.Mappings;

namespace PhoenixSea.Trading.SqlServer.MarketData.Context
{
    public class MarketDataDbContext : DbContext
    {
        public MarketDataDbContext() : base($"name={nameof(MarketDataDbContext)}")
        {
            bool instanceExists = System.Data.Entity.SqlServer.SqlProviderServices.Instance != null;
        }

        public DbSet<AssetClass> Instruments { get; set; }
        public DbSet<AssetProduct> StrategyTypes { get; set; }
        public DbSet<UsTreasuryData> InstrumentHedges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());

            modelBuilder.Configurations.Add(new AssetClassMapping());
            modelBuilder.Configurations.Add(new AssetProductMapping());
            modelBuilder.Configurations.Add(new UsTreasuryDataMapping());
        }
    }
}
