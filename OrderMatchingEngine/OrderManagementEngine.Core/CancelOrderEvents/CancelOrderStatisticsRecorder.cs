using Disruptor;
using OrderManagementEngine.Core.Services;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    /// <summary>
    /// The event handler to record the total number of cancelled orders being made (per day)
    /// This could be extended to record other statistics in the future
    /// </summary>
    public class CancelOrderStatisticsRecorder : IEventHandler<CancelOrderEvent>
    {
        private readonly IStatisticRecordingService statisticRecordingService;

        public CancelOrderStatisticsRecorder(IStatisticRecordingService statisticRecordingService)
        {
            this.statisticRecordingService = statisticRecordingService;
        }

        public void OnEvent(CancelOrderEvent data, long sequence, bool endOfBatch)
        {
            // TODO implement this
        }
    }
}