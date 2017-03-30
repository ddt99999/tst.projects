using Disruptor;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    /// <summary>
    /// To persist the information into log
    /// </summary>
    public class CancelOrderPersister : IEventHandler<CancelOrderEvent>
    {
        public void OnEvent(CancelOrderEvent data, long sequence, bool endOfBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}