using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OrderManagementEngine.Core.BusinessEntities
{
    /// <summary>
    /// This class creates a sorted, iterable list or orders for each price level
    /// in the order tree
    /// </summary>
    public class OrderList : IEnumerable<Order>, IEnumerator<Order>
    {
        public Order HeadOrder { get; private set; }

        public Order TailOrder { get; private set; }

        public int Length { get; private set; }

        public decimal Volume { get; set; }

        private Order last;

        public void AppendOrder(Order incomingOrder)
        {
            if (Length == 0)
            {
                incomingOrder.NextOrder = null;
                incomingOrder.PrevOrder = null;
                HeadOrder = incomingOrder;
                TailOrder = incomingOrder;
            }
            else
            {
                incomingOrder.PrevOrder = TailOrder;
                incomingOrder.NextOrder = null;

                TailOrder.NextOrder = incomingOrder;
                TailOrder = incomingOrder;
            }
            Length++;
            Volume += incomingOrder.Quantity;
        }

        public void RemoveOrder(Order order)
        {
            Volume -= order.Quantity;
            Length--;

            if (Length == 0)
                return;

            var tempNextOrder = order.NextOrder;
            var tempPrevOrder = order.PrevOrder;

            if (tempNextOrder != null && tempPrevOrder != null)
            {
                tempNextOrder.PrevOrder = tempPrevOrder;
                tempPrevOrder.NextOrder = tempNextOrder;
            }
            else if (tempNextOrder != null)
            {
                tempNextOrder.PrevOrder = null;
                HeadOrder = tempNextOrder;
            }
            else if (tempPrevOrder != null)
            {
                tempPrevOrder.NextOrder = null;
                TailOrder = tempPrevOrder;
            }
        }

        /*
		 * Move 'order' to the tail of the list (after modification for e.g.)
		 */
        public void MoveTail(Order order)
        {
            if (order.PrevOrder != null)
                order.PrevOrder.NextOrder = order.NextOrder;
            else
                HeadOrder = order.NextOrder;

            order.NextOrder.PrevOrder = order.PrevOrder;

            // set the previous tail's next order to this order
            TailOrder.NextOrder = order;
            order.PrevOrder = TailOrder;

            TailOrder = order;
            order.NextOrder = null; 
        }

        public IEnumerator<Order> GetEnumerator()
        {
            this.last = HeadOrder;
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Order Next()
        {
            if (this.last == null)
                throw new NullReferenceException();

            var order = this.last;
            this.last = this.last.NextOrder;

            return order;
        }

        public bool MoveNext()
        {
            return this.last != null;
        }

        public void Reset()
        {
            HeadOrder = null;
            TailOrder = null;
            Length = 0;
            Volume = 0;
            last = null;      
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var order in this)
            {
                sb.AppendFormat("| " + order + "\n");
            }
            return sb.ToString();
        }

        public Order Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            Reset();
        }
    }
}