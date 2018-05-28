using Disruptor;
using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderStatisticsRecorder : IEventHandler<NewOrderEvent>
    {
        private readonly IStatisticRecordingService statisticRecordingService;

        public NewOrderStatisticsRecorder(IStatisticRecordingService statisticRecordingService)
        {
            this.statisticRecordingService = statisticRecordingService;
        }

        public void OnEvent(NewOrderEvent data, long sequence, bool endOfBatch)
        {
            if (data.OrderMatchReport == null)
                return;

            if (data.OrderMatchReport.MatchType == OrderMatchType.Filled)
                statisticRecordingService.IncrementTotalFilledOrder(data.Order.Asset);
        }
    }
}