using System.Threading;
using Disruptor;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    /// <summary>
    /// To add the new orders into the market order book, this happens before the order matching
    /// </summary>
    public class NewOrderRegistrator : IEventHandler<NewOrderEvent>
    {
        private readonly IOrderBookRepository orderBookRepository;
        private readonly object locker = new object();
        private long orderId;

        public NewOrderRegistrator(
            IOrderBookRepository orderBookRepository)
        {
            this.orderBookRepository = orderBookRepository;
            this.orderId = 0;
        }

        public void OnEvent(NewOrderEvent data, long sequence, bool endOfBatch)
        {
            var order = data.Order;
            lock (locker)
            {
                orderBookRepository.AddOrderBook(order);
            }           
        }
    }
}