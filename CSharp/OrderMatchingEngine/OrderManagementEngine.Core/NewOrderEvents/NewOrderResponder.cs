using Disruptor;
using OrderManagementEngine.Core.Messages;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderResponder : IEventHandler<NewOrderEvent>
    {
        private readonly IFix44MessageHandler fixMessageHandler;
        private readonly IFixConnectivityHandler fixConnectivityHandler;
        private readonly IOrderBookPriceDisplayService orderBookPriceDisplayService;

        public NewOrderResponder(
            IFix44MessageHandler fixMessageHandler,
            IFixConnectivityHandler fixConnectivityHandler,
            IOrderBookPriceDisplayService orderBookPriceDisplayService)
        {
            this.fixMessageHandler = fixMessageHandler;
            this.fixConnectivityHandler = fixConnectivityHandler;
            this.orderBookPriceDisplayService = orderBookPriceDisplayService;
        }

        public void OnEvent(NewOrderEvent data, long sequence, bool endOfBatch)
        {
            if (data.OrderMatchReport == null)
                return;

            // reply to FIX client
            //var message = fixMessageHandler.CreateFillReport(data.OrderMatchReport);
            //fixConnectivityHandler.SendMessageToTarget(message, data.SessionId);

            // display the price/time bids asks prices in Console
            orderBookPriceDisplayService.DisplayPrices(data.Order, 5);
        }
    }
}