using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.BusinessEntities
{
    /// <summary>
    /// The data structure to give the best performance for Bids/Asks operations
    /// It can help to keep track the orders of each single price, order deletion, order update in constant time
    /// It also order insertion with sorting in runtime in log time 
    /// </summary>
    public class OrderTree
    {
        private readonly SortedDictionary<decimal, OrderList> priceTree = new SortedDictionary<decimal, OrderList>();
        private readonly Dictionary<decimal, OrderList> priceMap = new Dictionary<decimal, OrderList>();
        private readonly Dictionary<long, Order> orderMap = new Dictionary<long, Order>();

        public decimal Volume { get; private set; }
        public int OrderSize { get; private set; }
        public int Depth { get; private set; }

        public OrderTree()
        {
            Reset();
        }

        public void Reset()
        {
            this.priceTree.Clear();
            this.priceMap.Clear();
            this.orderMap.Clear();

            Volume = 0;
            OrderSize = 0;
            Depth = 0;       
        }

        public int Length()
        {
            return orderMap.Count;
        }

        /// <summary>
        /// Returns the OrderList associated with 'price'
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public OrderList GetOrderListByPrice(decimal price)
        {
            OrderList orderList;
            return priceMap.TryGetValue(price, out orderList) ? orderList : new OrderList();
        }

        /// <summary>
        /// Returns the order given the order ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrder(long orderId)
        {
            Order order;
            return orderMap.TryGetValue(orderId, out order) ? order : null;
        }

        public SortedDictionary<decimal, OrderList> GetPriceTree()
        {
            return priceTree;
        }

        private void CreatePrice(decimal price)
        {
            Depth++;
            var orderList = new OrderList();
            priceTree.Add(price, orderList);
            priceMap.Add(price, orderList);
        }

        private void RemovePrice(decimal price)
        {
            Depth--;
            priceTree.Remove(price);
            priceMap.Remove(price);
        }

        public bool IsPriceExists(decimal price)
        {
            return priceMap.ContainsKey(price);
        }

        public bool IsOrderExists(long orderId)
        {
            return orderMap.ContainsKey(orderId);
        }

        public void InsertOrder(Order order)
        {
            var orderId = order.OrderId;
            var orderPrice = order.Price;

            if (IsOrderExists(orderId))
                RemoveOrderById(orderId);

            OrderSize++;

            if (!IsPriceExists(orderPrice))
                CreatePrice(orderPrice);

            order.OrderList = priceMap[orderPrice];
            priceMap[orderPrice].AppendOrder(order);
            orderMap.Add(orderId, order);
            Volume += order.Quantity;        
        }

        public void UpdateOrder(Order orderUpdate)
        {
            var orderId = orderUpdate.OrderId;
            var orderPrice = orderUpdate.Price;

            Order order;
            if (orderMap.TryGetValue(orderId, out order))
            {
                var originalVolume = order.Quantity;

                if (orderPrice != order.Price)
                {
                    // Price has been updated
                    var tempOrderList = priceMap[order.Price];
                    tempOrderList.RemoveOrder(order);

                    if (tempOrderList.Length == 0)
                        RemovePrice(order.Price);

                    InsertOrder(orderUpdate);
                }
                else
                {
                    // The quantity has changed
                    order.UpdateQuantity(orderUpdate.Quantity, orderUpdate.LastUpdateTime);
                }

                Volume += (order.Quantity - originalVolume);
            }   
        }
       
        public void UpdateOrderQuantity(decimal quantity, long orderId)
        {
            var order = orderMap[orderId];
            var originalQuantity = order.Quantity;
            order.UpdateQuantity(quantity, order.LastUpdateTime);
            Volume += (order.Quantity - originalQuantity);
            orderMap[orderId] = order;
        }

        public void RemoveOrderById(long orderId)
        {
            OrderSize -= 1;
            Order order;

            if (!orderMap.TryGetValue(orderId, out order))
                return;

            Volume -= order.Quantity;
            order.OrderList.RemoveOrder(order);

            if (order.OrderList.Length == 0)
                RemovePrice(order.Price);

            orderMap.Remove(orderId);    
        }

        public decimal GetMaxPrice()
        {
            if (Depth > 0)
                return priceTree.Last().Key;

            return -1;
        }

        public decimal GetMinPrice()
        {
            if (Depth > 0)
            {
                return this.priceTree.First().Key;
            }

            return -1;
        }

        public OrderList GetOrderListByMaxPrice()
        {
            var maxPrice = GetMaxPrice();

            return Depth > 0 ? 
                GetOrderListByPrice(maxPrice) : 
                null;
        }

        public OrderList GetOrderListByMinPrice()
        {
            var minPrice = GetMinPrice();

            return Depth > 0 ?
                GetOrderListByPrice(minPrice) :
                null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("| The Book:");
            sb.AppendLine("| Max Price = " + GetMaxPrice());
            sb.AppendLine("| Min Price = " + GetMinPrice());
            sb.AppendLine("| Volume in book = " + Volume);
            sb.AppendLine("| Depth of book = " + Depth);
            sb.AppendLine("| Orders in book = " + OrderSize);
            sb.AppendLine("| Length of tree = " + Length());
            sb.AppendLine();

            foreach (var entry in priceTree)
            {
                sb.Append(entry.Value);
                sb.AppendLine("|");
            }

            return sb.ToString();
        }
    }
}