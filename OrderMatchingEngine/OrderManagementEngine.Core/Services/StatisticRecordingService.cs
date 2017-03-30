using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public class StatisticRecordingService : IStatisticRecordingService
    {
        private readonly IOrderBookRepository orderBookRepository;

        public StatisticRecordingService(IOrderBookRepository orderBookRepository)
        {
            this.orderBookRepository = orderBookRepository;
        }

        public void IncrementTotalFilledOrder(Asset asset)
        {
            var orderBook = orderBookRepository.GetOrderBook(asset);

            if (orderBook != null)
                orderBook.IncrementTotalFilledOrder();
        }
        public void IncrementTotalCanceledOrder(Asset asset)
        {
            var orderBook = orderBookRepository.GetOrderBook(asset);

            if (orderBook != null)
                orderBook.IncrementTotalCanceledOrder();
        }
        public void ResetStatistics(Asset asset)
        {
            var orderBook = orderBookRepository.GetOrderBook(asset);

            if (orderBook != null)
                orderBook.ResetStatistics();
        }
    }
}