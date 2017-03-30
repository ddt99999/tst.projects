using Disruptor;
using OrderManagementEngine.Core.Messages;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    /// <summary>
    /// To create the OrderCancel report and send to the client via FIX
    /// </summary>
    public class CancelOrderResponder : IEventHandler<CancelOrderEvent>
    {
        private readonly IFix44MessageHandler fixMessageHandler;
        private readonly IFixConnectivityHandler fixConnectivityHandler;

        public CancelOrderResponder(
            IFix44MessageHandler fixMessageHandler,
            IFixConnectivityHandler fixConnectivityHandler)
        {
            this.fixMessageHandler = fixMessageHandler;
            this.fixConnectivityHandler = fixConnectivityHandler;
        }

        public void OnEvent(CancelOrderEvent data, long sequence, bool endOfBatch)
        {
            var message = fixMessageHandler.CreateCancelReport(data.OrderCancelReport);
            fixConnectivityHandler.SendMessageToTarget(message, data.SessionId);
        }
    }
}