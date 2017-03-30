using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;
using QuickFix.FIX44;

namespace OrderManagementEngine.Core.Messages
{
    public interface IFixMessageHandler
    {
        void ProcessMessage(NewOrderSingle newOrder, SessionID sessionId);
    }
}