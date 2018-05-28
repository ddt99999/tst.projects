using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.SqlServer.MarketData.Entities;

namespace PhoenixSea.Trading.SqlServer.MarketData.Mappings
{
    public class AssetClassMapping : BaseEntityMapping<AssetClass>
    {
        public AssetClassMapping()
        {
            Property(x => x.Name).HasColumnName("Name").HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
        }
    }
}