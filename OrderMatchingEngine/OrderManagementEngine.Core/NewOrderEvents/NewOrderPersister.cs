using System.IO;
using Disruptor;

namespace OrderManagementEngine.Core.NewOrderEvents
{
    public class NewOrderPersister : IEventHandler<NewOrderEvent>
    {
        private readonly StreamWriter streamWriter;

        public NewOrderPersister(StreamWriter streamWriter)
        {
            this.streamWriter = streamWriter;
        }

        public void OnEvent(NewOrderEvent data, long sequence, bool endOfBatch)
        {
            //streamWriter.Write(data.GetRawData());
        }
    }
}