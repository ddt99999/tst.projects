using Disruptor;
using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderTranslator : IEventTranslatorTwoArg<NewOrderEvent, Order, SessionID>
    {
        public void TranslateTo(NewOrderEvent @event, long sequence, Order order, SessionID sessionId)
        {
            @event.Order = order;
            @event.SessionId = sessionId;
        }
    }
}