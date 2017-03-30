using Disruptor;
using OrderManagementEngine.Core.Interfaces;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.CancelOrderEvents
{
    public class CancelOrderEventRingBufferCaller : IRingBufferCaller<CancelOrderEvent>
    {
        public RingBuffer<CancelOrderEvent> CallRingBuffer()
        {
            return DIContainer.Resolve<RingBuffer<CancelOrderEvent>>();
        }
    }
}