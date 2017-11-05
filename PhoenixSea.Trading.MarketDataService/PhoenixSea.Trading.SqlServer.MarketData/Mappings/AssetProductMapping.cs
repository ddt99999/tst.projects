using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.SqlServer.MarketData.Mappings
{
    public class AssetProductMapping : BaseEntityMapping<AssetProduct>
    {
        public AssetProductMapping()
        {
            Property(x => x.Name).HasColumnName("Name").HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
            Property(x => x.Description).HasColumnName("Description").HasColumnType("VARCHAR").HasMaxLength(1000);
            HasRequired(x => x.AssetClass).WithMany();
        }
    }
}