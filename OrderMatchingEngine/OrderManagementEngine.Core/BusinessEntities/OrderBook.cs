using System;
using System.Collections.Generic;
using System.Threading;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.BusinessEntities
{
    /// <summary>
    /// The central place to manage and store the Bids/Asks orders for each asset/symbol like MSFT, USDCHF and etc 
    /// </summary>
    public class OrderBook
    {
        public OrderTree Bids { get; private set; }
        public OrderTree Asks { get; private set; }

        private long time;
        /// <summary>
        /// Timestamp
        /// </summary>
        public long Time
        {
            get
            {
                return Interlocked.CompareExchange(ref time, 0, 0);
            }

            set
            {
                Interlocked.Exchange(ref time, value);
            }
        }
        private int nextQuoteId;

        public int NextQuoteId
        {
            get
            {
                return Interlocked.CompareExchange(ref nextQuoteId, 0, 0);
            }

            set
            {
                Interlocked.Exchange(ref nextQuoteId, value);
            }
        }

        private int lastOrderSign;
        /// <summary>
        /// Check if this is market or limit order
        /// </summary>
        public int LastOrderSign
        {
            get
            {
                return Interlocked.CompareExchange(ref lastOrderSign, 0, 0);
            }

            set
            {
                Interlocked.Exchange(ref lastOrderSign, value);
            }
        }

        /// <summary>
        /// Calculate the total filled orders of the day. For REPORTING purpose
        /// This could be adjusted to get the count per minute or per hour, but for simplicity,
        /// we assume this will be reset everyday.
        /// </summary>
        public int totalFilledOrder;

        public int TotalFilledOrder
        {
            get { return totalFilledOrder; }
        }

        /// <summary>
        /// Calculate the total cancelled orders of the day. For REPORTING purpose.
        /// This could be adjusted to get the count per minute or per hour, but for simplicity,
        /// we assume this will be reset everyday.
        /// </summary>
        public int totalCanceledOrder;

        public int TotalCanceledOrder
        {
            get { return totalCanceledOrder; }
        }

        // Since the write lock may already be held by AddOrder when a Match is kicked off
        // the sided locks are made recursive.

        private readonly ReaderWriterLockSlim bidsLock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private readonly ReaderWriterLockSlim asksLock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public OrderBook()
        {
            Bids = new OrderTree();
            Asks = new OrderTree();
            Time = 0;
            NextQuoteId = 0;
            LastOrderSign = 1;
        }

//        public OrderBook(IComparer<Order> comparer)
//        {
//            this.bids = new SortedSet<Order>(comparer);
//            this.asks = new SortedSet<Order>(comparer);
//        }

        /// <summary>
        /// Add an order to the bid or ask stack.
        /// </summary>
        public void AddOrder(Order order)
        {
            var orderTree = GetBidAskOrdersBySide(order);
            orderTree.InsertOrder(order);
        }

        public void CancelOrder(Order order)
        {
            Time = DateTime.UtcNow.Ticks;
            var side = order.OrderSide;
            var orderId = order.OrderId;

            var orderTree = GetBidAskOrdersBySide(side);

            if (orderTree.IsOrderExists(order.OrderId))
                orderTree.RemoveOrderById(orderId);
        }

        public decimal GetVolumeByPrice(OrderSide side, decimal price)
        {
            decimal volume;
            var orderTree = GetBidAskOrdersBySide(side);

            if (orderTree.IsPriceExists(price))
                volume = orderTree.GetOrderListByPrice(price).Volume;
            else
            {
                Console.WriteLine("No price found");
                return -1;
            }

            return volume;
        }

        public decimal GetVolumeBySide(OrderSide side)
        {
            return GetBidAskOrdersBySide(side).Volume;
        }

        public decimal GetBestBid()
        {
            return Bids.GetMaxPrice();
        }

        public decimal GetWorstBid()
        {
            return Bids.GetMinPrice();
        }

        public decimal GetBestAsk()
        {
            return Asks.GetMinPrice();
        }

        public decimal GetWorstAsk()
        {
            return Asks.GetMaxPrice();
        }

        public decimal GetSpread()
        {
            return GetBestAsk() - GetBestBid();
        }

        public decimal GetMid()
        {
            return GetBestBid() + (GetSpread() / 2.0M);
        }

        public bool ContainBidsAndAsks()
        {
            return ((Bids.OrderSize > 0) && (Asks.OrderSize > 0));
        }

        public void IncrementTotalFilledOrder()
        {
            Interlocked.Increment(ref totalFilledOrder);
        }
        public void IncrementTotalCanceledOrder()
        {
            Interlocked.Increment(ref totalCanceledOrder);
        }

        public void IncrementNextOrderId()
        {
            Interlocked.Increment(ref nextQuoteId);
        }

        public void ResetStatistics()
        {
            totalFilledOrder = 0;
            totalCanceledOrder = 0;
        }

        public OrderTree GetBidAskOrdersBySide(Order order)
        {
            return GetBidAskOrdersBySide(order.OrderSide);
        }

        private ReaderWriterLockSlim GetLockBySide(Order order)
        {
            return GetLockBySide(order.OrderSide);
        }

        public OrderTree GetBidAskOrdersBySide(OrderSide side)
        {
            return (side == OrderSide.Bid) ? Bids : Asks;
        }

        private ReaderWriterLockSlim GetLockBySide(OrderSide side)
        {
            return (side == OrderSide.Bid) ? bidsLock : asksLock;
        }
    }
}