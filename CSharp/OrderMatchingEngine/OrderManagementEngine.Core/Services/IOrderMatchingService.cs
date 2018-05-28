using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;

namespace OrderManagementEngine.Core.Services
{
    public interface IOrderMatchingService
    {
        OrderMatchReport ProcessOrder(Order order, SessionID sessionId);
    }
}