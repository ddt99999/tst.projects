using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace OrderManagementEngine.Core.Utils
{
    public class ConcurrentEnumerator<T> : IEnumerator<T>
    {
        #region variables

        private readonly IEnumerator<T> enumerator;
        private ReaderWriterLockSlim readerWriterLock;

        #endregion

        #region constructor

        public ConcurrentEnumerator(
            IEnumerator<T> enumerator, ReaderWriterLockSlim readerWriterLock)
        {
            this.enumerator = enumerator;
            this.readerWriterLock = readerWriterLock;

            this.readerWriterLock.EnterReadLock();
        }

        #endregion

        #region Implementation of IEnumerator

        public T Current
        {
            get
            {
                return enumerator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            // this will be called when the foreach loop finishes
            readerWriterLock.ExitReadLock();
        }

        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        public void Reset()
        {
            enumerator.Reset();
        }

        #endregion
    }
}
