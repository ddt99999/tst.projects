using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.Utils;
using QuickFix;

namespace OrderManagementEngine.Core.Services
{
    /// <summary>
    /// To perform the market order matching
    /// </summary>
    public class MarketOrderMatchingService : IOrderMatchingService
    {
        private readonly IOrderBookRepository orderBookRepository;
        private readonly object locker = new object();
        public MarketOrderMatchingService(
            IOrderBookRepository orderBookRepository)
        {
            this.orderBookRepository = orderBookRepository;
        }

        public OrderMatchReport ProcessOrder(Order order, SessionID sessionId)
        {
            if (order.Quantity <= 0)
                return null; // TODO ErrorReport is needed

            lock (locker)
            {
                var orderBook = orderBookRepository.GetOrderBook(order);
                var report = ExecuteOrderMatching(orderBook, order);
                return report;
            }
        }

        protected virtual OrderMatchReport ExecuteOrderMatching(OrderBook orderBook, Order order)
        {
            var side = order.OrderSide;
            var quantityRemaining = order.Quantity;
            var isMatchingTried = false;

            if (side == OrderSide.Bid)
            {
                orderBook.LastOrderSign = 1;

                while ((quantityRemaining > 0) && (orderBook.Asks.OrderSize > 0))
                {
                    isMatchingTried = true;
                    if (order.Price >= orderBook.Asks.GetMinPrice())
                    {
                        var ordersWithBestAskPrice = orderBook.Asks.GetOrderListByMinPrice();
                        quantityRemaining = ProcessOrders(orderBook, ordersWithBestAskPrice, quantityRemaining, order);
                    }
                    else
                    {
                        orderBook.Bids.InsertOrder(order);
                        break;
                    }
                    
                }

                if (!isMatchingTried)
                {
                    orderBook.Bids.InsertOrder(order);
                }
            }
            else if (side == OrderSide.Ask)
            {
                orderBook.LastOrderSign = -1;

                while ((quantityRemaining > 0) && (orderBook.Bids.OrderSize > 0))
                {
                    isMatchingTried = true;
                    if (order.Price <= orderBook.Bids.GetMaxPrice())
                    {
                        var ordersWithBestBidPrice = orderBook.Bids.GetOrderListByMaxPrice();
                        quantityRemaining = ProcessOrders(orderBook, ordersWithBestBidPrice, quantityRemaining, order);
                    }
                    else
                    {
                        orderBook.Asks.InsertOrder(order);
                        break;
                    }
                }

                if (!isMatchingTried)
                {
                    orderBook.Asks.InsertOrder(order);
                }
            }
            else
            {
                return null; // TODO return ErrorReport

            }

            return new OrderMatchReport
            {
                Account = order.Account,
                Asset = order.Asset,
                ClOrdId = order.ClOrdId,
                MatchedQuantity = order.Quantity - quantityRemaining,
                MatchType = isMatchingTried ? (quantityRemaining > 0 ?  OrderMatchType.PartialFilled : OrderMatchType.Filled) : OrderMatchType.None,
                OrderId = order.OrderId,
                OrderSide = order.OrderSide,
                OriginalOrderQuantity = order.Quantity,
                Price = order.Price,
                RemainingQuantity = quantityRemaining
            };
        }

        protected decimal ProcessOrders(OrderBook orderBook, OrderList orders, decimal quantityRemaining, Order order)
        {
            var side = order.OrderSide;
            string buyer, seller;
            var clientId = order.ClOrdId;
            var timestamp = order.LastUpdateTime;

            while ((orders.Length > 0) && (quantityRemaining > 0))
            {
                decimal quantityTraded = 0;
                var headOrder = orders.HeadOrder;

                if (quantityRemaining < headOrder.Quantity)
                {
                    quantityTraded = quantityRemaining;

                    if (side == OrderSide.Ask)
                    {
                        orderBook.Bids.UpdateOrderQuantity(headOrder.Quantity - quantityRemaining, headOrder.OrderId);
                    }
                    else
                    {
                        orderBook.Asks.UpdateOrderQuantity(headOrder.Quantity - quantityRemaining, headOrder.OrderId);
                    }

                    quantityRemaining = 0;
                }
                else
                {
                    quantityTraded = headOrder.Quantity;

                    if (side == OrderSide.Ask)
                    {
                        orderBook.Bids.RemoveOrderById(headOrder.OrderId);
                    }
                    else
                    {
                        orderBook.Asks.RemoveOrderById(headOrder.OrderId);
                    }

                    quantityRemaining -= quantityTraded;
                }

                if (side == OrderSide.Ask)
                {
                    buyer = headOrder.ClOrdId;
                    seller = clientId;
                }
                else
                {
                    buyer = clientId;
                    seller = headOrder.ClOrdId;
                }

                // TODO To record the trades?
            }
            return quantityRemaining;
        }
    }
}