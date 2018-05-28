using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OrderManagementEngine.Core.Utils
{
    public class ConcurrentSortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        #region Variables

        private readonly ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
        private readonly SortedDictionary<TKey, TValue> sortedDict;

        #endregion

        public ConcurrentSortedDictionary()
        {
            this.sortedDict = new SortedDictionary<TKey, TValue>();
        }

        public ConcurrentSortedDictionary(IComparer<TKey> comparer)
        {
            this.sortedDict = new SortedDictionary<TKey, TValue>(comparer);
        }

        public ConcurrentSortedDictionary(IDictionary<TKey, TValue> dictionary)
        {
            this.sortedDict = new SortedDictionary<TKey, TValue>(dictionary);
        }

        public ConcurrentSortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            this.sortedDict = new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public TValue this[TKey key]
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return sortedDict[key];
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
            set
            {
                readerWriterLock.EnterWriteLock();
                try
                {
                    sortedDict[key] = value;
                }
                finally
                {
                    readerWriterLock.ExitWriteLock();
                }
            }
        }

        public IComparer<TKey> Comparer
        {
            get
            {
                readerWriterLock.EnterReadLock();

                try
                {
                    return sortedDict.Comparer;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        public int Count
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return sortedDict.Count;
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return new SortedDictionary<TKey, TValue>.KeyCollection(sortedDict);
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                readerWriterLock.EnterReadLock();
                try
                {
                    return new SortedDictionary<TKey, TValue>.ValueCollection(sortedDict);
                }
                finally
                {
                    readerWriterLock.ExitReadLock();
                }
            }
        }

        #region Methods
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                sortedDict.Add(key, value);
            }
            catch (ArgumentException ex)
            {

            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                sortedDict.Clear();
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return sortedDict.Contains(item);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public bool ContainsKey(TKey key)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return sortedDict.ContainsKey(key);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                sortedDict.CopyTo(array, arrayIndex);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                return sortedDict.Remove(item.Key);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public bool Remove(TKey key)
        {
            readerWriterLock.EnterWriteLock();
            try
            {
                return sortedDict.Remove(key);
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            readerWriterLock.EnterReadLock();
            try
            {
                return sortedDict.TryGetValue(key, out value);
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new ConcurrentEnumerator<KeyValuePair<TKey, TValue>>(sortedDict.GetEnumerator(), readerWriterLock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ConcurrentEnumerator<KeyValuePair<TKey, TValue>>(sortedDict.GetEnumerator(), readerWriterLock);
        }

        #endregion
    }
}
