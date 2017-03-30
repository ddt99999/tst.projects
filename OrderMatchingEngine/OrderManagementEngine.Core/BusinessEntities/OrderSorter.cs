using System;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.BusinessEntities
{
    public static class OrderSorter
    {
        /// <summary>
        /// The default way of comparing orders, where a better order is "less than" the other.
        /// Compares price, last update time, quantity, then ID as a tie-breaker
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// -1 if x is less than y (e.g. better)
        /// 0 if x == y
        /// 1 if x is greater than y (e.g. worse)
        /// </returns>
        public static int SortOrderBySomeProperties(Order x, Order y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            if (x.OrderSide != y.OrderSide)
                throw new ArgumentException("IsOrderBetterThan for different order side");

            // Sort by price, last update, quantity then ID as a tie-breaker
            if (x.Price != y.Price)
                return SortByPrice(x, y);
            if (x.LastUpdateTime != y.LastUpdateTime)
                return BoolCompareToInt(x.LastUpdateTime < y.LastUpdateTime);
            if (x.Quantity != y.Quantity)
                return BoolCompareToInt(x.Quantity > y.Quantity);
            if (x.OrderId != y.OrderId)
                return BoolCompareToInt(x.OrderId < y.OrderId);

            return XEqualToY;
        }

        public static int SortByPrice(Order x, Order y)
        {
            return x.Price == y.Price ? XEqualToY : BoolCompareToInt(x.HasBetterPriceThan(y));
        }

        private static int BoolCompareToInt(bool b)
        {
            return b ? XBetterThanY : XWorseThanY;
        }

        private const int XBetterThanY = -1;
        private const int XEqualToY = 0;
        private const int XWorseThanY = 1;
    }
}