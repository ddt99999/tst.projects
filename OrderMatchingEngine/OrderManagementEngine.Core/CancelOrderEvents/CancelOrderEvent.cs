using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    public class CancelOrderEvent
    {
        public SessionID SessionId { get; set; }
        public Order Order { get; set; }
        public OrderCancelReport OrderCancelReport { get; set; }
    }
}