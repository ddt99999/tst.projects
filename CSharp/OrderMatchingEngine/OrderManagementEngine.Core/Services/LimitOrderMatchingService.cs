using Disruptor;
using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.CancelOrderEvents;
using QuickFix;

namespace OrderManagementEngine.Core.Services
{
    public class LimitOrderMatchingService : MarketOrderMatchingService
    {
        public LimitOrderMatchingService(
            IOrderBookRepository orderBookRepository,
            IEventTranslatorTwoArg<CancelOrderEvent, OrderMatchReport, SessionID> translator) 
            : base(orderBookRepository)
        {
        }

        protected override OrderMatchReport ExecuteOrderMatching(OrderBook orderBook, Order order)
        {
            var isOrderInBook = false;
            var side = order.OrderSide;
            var quantityRemaining = order.Quantity;
            var price = order.Price;

            if (side == OrderSide.Bid)
            {
                orderBook.LastOrderSign = 1;

                while ((orderBook.Asks.OrderSize > 0) &&
                       (quantityRemaining > 0) &&
                       (price >= orderBook.Asks.GetMinPrice()))
                {
                    var ordersAtBestAsk = orderBook.Asks.GetOrderListByMinPrice();
                    quantityRemaining = ProcessOrders(orderBook, ordersAtBestAsk, quantityRemaining, order);
                }

                // if volume remains, add order to book
                if (quantityRemaining > 0)
                {
                    order.OrderId = orderBook.NextQuoteId;
                    order.Quantity = quantityRemaining;
                    orderBook.Bids.InsertOrder(order);
                    isOrderInBook = true;
                    orderBook.IncrementNextOrderId();
                }
                else
                    isOrderInBook = false;
            }
            else if (side == OrderSide.Ask)
            {
                orderBook.LastOrderSign = -1;

                while ((orderBook.Bids.OrderSize > 0) &&
                       (quantityRemaining > 0) &&
                       (price <= orderBook.Bids.GetMaxPrice()))
                {
                    var ordersAtBestBid = orderBook.Bids.GetOrderListByMaxPrice();
                    quantityRemaining = ProcessOrders(orderBook, ordersAtBestBid, quantityRemaining, order);
                }

                // If volume remains, add to book
                if (quantityRemaining > 0)
                {
                    order.OrderId = orderBook.NextQuoteId;
                    order.Quantity = quantityRemaining;
                    orderBook.Asks.InsertOrder(order);
                    isOrderInBook = true;
                    orderBook.IncrementNextOrderId();
                }
                else
                {
                    isOrderInBook = false;
                }
            }

            return new OrderMatchReport
            {
                Account = order.Account,
                Asset = order.Asset,
                ClOrdId = order.ClOrdId,
                MatchedQuantity = order.Quantity - quantityRemaining,
                MatchType = quantityRemaining > 0 ? OrderMatchType.PartialFilled : OrderMatchType.Filled,
                OrderId = order.OrderId,
                OrderSide = order.OrderSide,
                OriginalOrderQuantity = order.Quantity,
                Price = order.Price,
                RemainingQuantity = quantityRemaining
            };
        }
    }
}