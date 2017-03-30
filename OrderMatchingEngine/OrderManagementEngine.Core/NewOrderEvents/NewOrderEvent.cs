using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderEvent
    {
        public SessionID SessionId { get; set; }
        public Order Order { get; set; }
        public OrderMatchReport OrderMatchReport { get; set; }
    }
}