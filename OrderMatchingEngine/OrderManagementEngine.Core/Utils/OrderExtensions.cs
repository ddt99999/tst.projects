using System.Text;
using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Utils
{
    public static class OrderExtensions
    {
        public static bool HasBetterPriceThan(this Order x, Order y)
        {
            if (x.OrderSide != y.OrderSide)
                throw new EntityException(
                    "Trying to compare prices for orders on different market sides");

            return x.OrderSide == OrderSide.Ask
                       ? x.Price < y.Price
                       : x.Price > y.Price;
        }

        public static string GetPropertiesString(this Order order)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var properties = order.GetType().GetProperties();
            foreach (var p in properties)
            {
                var getter = p.GetGetMethod();
                var o = getter.Invoke(order, new object[] { });
                sb.AppendFormat("{0}={1};", p.Name, o);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}