using PhoenixSea.Common.Data;

namespace PhoenixSea.Trading.SqlServer.MarketData.Entities
{
    public class AssetProduct : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetClass AssetClass { get; set; }
    }
}