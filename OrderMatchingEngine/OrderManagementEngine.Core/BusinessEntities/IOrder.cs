using System;
using OrderManagementEngine.Core.Messages;

namespace OrderManagementEngine.Core.BusinessEntities
{
    /// <summary>
    /// An order from the client
    /// </summary>
    public interface IOrder : IEquatable<IOrder>
    {
        /// <summary>
        /// The internal OD of the order in this engine
        /// </summary>
        long OrderId { get; }
        MessageType CommandType { get; }

        OrderType OrderType { get; }

        /// <summary>
        /// The asset of the order
        /// </summary>
        Asset Asset { get; }

        /// <summary>
        /// Bid or ask
        /// </summary>
        OrderSide OrderSide { get; }
        /// <summary>
        /// The price of the order
        /// </summary>
        decimal Price { get; }
        /// <summary>
        /// The volume/quantity of the order
        /// </summary>
        decimal Quantity { get; }
        /// <summary>
        /// The quantity or volume this order was created with
        /// </summary>
        decimal OriginalQuantity { get; }

        /// <summary>
        /// The quantity or volume that has been filled so far for this order
        /// </summary>
        decimal FilledQuantity { get; }

        /// <summary>
        /// When the order was last updated, either creation or a property changed
        /// </summary>
        long LastUpdateTime { get; }

        /// <summary>
        /// The Client Order ID, assigned to the order by the client application
        /// </summary>
        string ClOrdId { get; }

        /// <summary>
        /// The trading account associated with the order
        /// </summary>
        TradingAccount Account { get; }

        IOrder NextOrder { get; }
        IOrder PrevOrder { get; }

        /// <summary>
        /// The order was partially filled/matched and should adjust itself accordingly
        /// </summary>
        /// <param name="filledQuantity"></param>
        void OrderPartiallyFilled(decimal filledQuantity);

        /// <summary>
        /// Update the price of this order, this will set LastUpdateTime to now
        /// </summary>
        /// <param name="newPrice"></param>
        void UpdatePrice(decimal newPrice);

        /// <summary>
        /// Update the quantity of this order, this will set LastUpdateTime to now
        /// </summary>
        /// <param name="newQuantity"></param>
        void UpdateQuantity(decimal newQuantity);

    }
}