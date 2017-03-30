using Disruptor.Dsl;

namespace OrderManagementEngine.Core.Disruptor
{
    public interface IDisruptorFactory<T> where T : class 
    {
        Disruptor<T> CreateDisruptor();
    }
}