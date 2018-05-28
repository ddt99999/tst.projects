using System.Threading.Tasks;
using Disruptor.Dsl;

namespace OrderManagementEngine.Core.Disruptor
{
    public class DisruptorFactory<T> : IDisruptorFactory<T> where T : class, new()
    {
        public Disruptor<T> CreateDisruptor()
        {
            return new Disruptor<T>(() => new T(), 2048, TaskScheduler.Default);
        }
    }
}