using Disruptor;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderMatcher : IEventHandler<NewOrderEvent>
    {
        private readonly IOrderMatchingService marketOrderMatchingService;

        public NewOrderMatcher(IOrderMatchingService marketOrderMatchingService)
        {
            this.marketOrderMatchingService = marketOrderMatchingService;
        }

        public void OnEvent(NewOrderEvent data, long sequence, bool endOfBatch)
        {
            var currentService = marketOrderMatchingService;
            var order = data.Order;

            if (data.Order.IsLimit)
            {
                currentService = marketOrderMatchingService as LimitOrderMatchingService;
            }

            if (currentService != null)
                data.OrderMatchReport = currentService.ProcessOrder(order, data.SessionId);
        }
    }
}