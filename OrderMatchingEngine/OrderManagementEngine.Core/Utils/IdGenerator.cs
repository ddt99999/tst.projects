using System.Threading;

namespace OrderManagementEngine.Core.Utils
{
    public sealed class IdGenerator
    {
        private long id;

        public IdGenerator()
        {
            Reset();
        }

        public long GenerateId()
        {
            return Interlocked.Increment(ref id);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref id, 0);
        }
    }
}