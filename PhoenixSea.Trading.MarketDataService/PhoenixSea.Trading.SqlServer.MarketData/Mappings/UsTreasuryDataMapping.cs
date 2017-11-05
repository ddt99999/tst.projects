using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.SqlServer.MarketData.Mappings
{
    public class UsTreasuryDataMapping : BaseEntityMapping<UsTreasuryData>
    {
        public UsTreasuryDataMapping()
        {
            Property(x => x.Timestamp).HasColumnName("Timestamp").HasColumnType("DATETIME2").IsRequired();
            Property(x => x.LongTermRealAverageRate).HasColumnName("LongTermRealAverageRate");
            HasRequired(x => x.AssetProduct).WithMany();
        }
    }
}