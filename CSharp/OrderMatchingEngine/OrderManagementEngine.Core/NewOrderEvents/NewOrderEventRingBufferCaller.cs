using Disruptor;
using OrderManagementEngine.Core.Interfaces;
using OrderManagementEngine.Core.Utils;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderEventRingBufferCaller : IRingBufferCaller<NewOrderEvent>
    {
        public RingBuffer<NewOrderEvent> CallRingBuffer()
        {
            return DIContainer.Resolve<RingBuffer<NewOrderEvent>>();
        }
    }
}