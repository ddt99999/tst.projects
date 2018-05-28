using Disruptor;
using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    public class CancelOrderEventTranslator : IEventTranslatorTwoArg<CancelOrderEvent, Order, SessionID>
    {
        public void TranslateTo(CancelOrderEvent @event, long sequence, Order order, SessionID sessionId)
        {
            @event.Order = order;
            @event.SessionId = sessionId;
        }
    }
}