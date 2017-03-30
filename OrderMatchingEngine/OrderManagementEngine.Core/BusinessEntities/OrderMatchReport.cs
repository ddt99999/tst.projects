namespace OrderManagementEngine.Core.BusinessEntities
{
    public sealed class OrderMatchReport
    {
        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalOrderQuantity { get; set; }
        public decimal MatchedQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public Asset Asset { get; set; }
        public OrderMatchType MatchType { get; set; }
        public string ClOrdId { get; set; }
        public OrderSide OrderSide { get; set; }
        public TradingAccount Account { get; set; }
    }
}