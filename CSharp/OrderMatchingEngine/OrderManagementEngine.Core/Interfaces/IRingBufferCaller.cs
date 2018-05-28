using Disruptor;

namespace OrderManagementEngine.Core.Interfaces
{
    public interface IRingBufferCaller<TEvent> where TEvent : class
    {
        RingBuffer<TEvent> CallRingBuffer();
    }
}