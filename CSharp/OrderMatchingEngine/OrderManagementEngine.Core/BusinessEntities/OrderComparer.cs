using System.Collections.Generic;

namespace OrderManagementEngine.Core.BusinessEntities
{
    public class OrderComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            return OrderSorter.SortOrderBySomeProperties(x, y);
        }
    }
}