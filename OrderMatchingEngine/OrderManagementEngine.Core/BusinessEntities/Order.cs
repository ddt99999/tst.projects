using System;
using OrderManagementEngine.Core.Messages;

namespace OrderManagementEngine.Core.BusinessEntities
{
    public class Order
    {
        public long OrderId { get; set; }
        public bool IsLimit { get; set; }
        public MessageType CommandType { get; set; }
        public OrderType OrderType { get; set; }
        public Asset Asset { get; set; }
        public OrderSide OrderSide { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal OriginalQuantity { get; set; }
        public decimal FilledQuantity { get; set; }
        public long LastUpdateTime { get; set; }
        public string ClOrdId { get; set; }
        public TradingAccount Account { get; set; }
        public Order NextOrder { get; set; }
        public Order PrevOrder { get; set; }
        public OrderList OrderList { get; set; }

        public void UpdateQuantity(decimal newQuantity, long timestamp)
        {
            if ((newQuantity > this.Quantity) && (this.OrderList.TailOrder != this))
            {
                // Move order to the end of the list. i.e. loses time priority
                this.OrderList.MoveTail(this);
                this.LastUpdateTime = timestamp;
            }
            OrderList.Volume -= (Quantity - newQuantity);
            Quantity = newQuantity;
        }
    }
}