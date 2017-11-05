using System;
using System.Collections.Concurrent;
using System.Linq;

namespace PhoenixSea.Common.Core.Collections
{
    public class BlockingBufferManager : IDisposable
    {
        /// <summary>
        ///     The full name of the <see cref="BlockingBufferManager" /> type.
        /// </summary>
        private static readonly string typeName = typeof(BlockingBufferManager).FullName;

        /// <summary>
        ///     Zero-based starting indices in <see cref="_data" />, of the available segments.
        /// </summary>
        private readonly BlockingCollection<int> _availableIndices;

        /// <summary>
        ///     Size of the buffers provided by the buffer manager.
        /// </summary>
        private readonly int _bufferSize;

        /// <summary>
        ///     Data block that provides the underlying storage for the buffers provided by the
        ///     buffer manager.
        /// </summary>
        private readonly byte[] _data;

        /// <summary>
        ///     Zero-based starting indices in <see cref="data" />, of the unavailable segments.
        /// </summary>
        private readonly ConcurrentDictionary<int, int> _usedIndices;

        /// <summary>
        ///     A value indicating whether the <see cref="BlockingBufferManager.Dispose" /> has
        ///     been called.
        /// </summary>
        private volatile bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlockingBufferManager" /> class.
        /// </summary>
        /// <param name="bufferSize">
        ///     Size of the buffers that will be provided by the buffer manager.
        /// </param>
        /// <param name="bufferCount">
        ///     Maximum amount of the buffers that will be concurrently used.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="bufferSize" /> or <paramref name="bufferCount" /> is less than one.
        /// </exception>
        public BlockingBufferManager(int bufferSize, int bufferCount)
        {
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize,
                    "Buffer size must not be less than one.");

            if (bufferCount < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferCount), bufferCount,
                    "Buffer count must not be less than one.");

            _bufferSize = bufferSize;
            _data = new byte[bufferSize * bufferCount];
            _availableIndices = new BlockingCollection<int>(bufferCount);

            for (var i = 0; i < bufferCount; i++)
                _availableIndices.Add(bufferSize * i);

            _usedIndices = new ConcurrentDictionary<int, int>(bufferCount, bufferCount);
        }

        /// <summary>
        ///     Gets the size of the buffers provided by the buffer manager.
        /// </summary>
        public int BufferSize => _bufferSize;

        /// <summary>
        ///     Gets the number of available buffers provided by the buffer manager.
        /// </summary>
        public int AvailableBuffers => !_isDisposed ? _availableIndices.Count : 0;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="BlockingBufferManager" /> is
        ///     disposed.
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        ///     Releases all resources used by the current instance of
        ///     <see cref="BlockingBufferManager" />. Underlying data block is an exception if it's
        ///     used in unmanaged operations that require pinning the buffer (e.g.
        ///     <see cref="System.Net.Sockets.Socket.ReceiveAsync" />).
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _availableIndices.CompleteAdding();
                int i;
                while (_availableIndices.TryTake(out i))
                    _usedIndices[i] = i;

                _isDisposed = true;
            }
        }

        /// <summary>
        ///     Gets an available buffer. This method blocks the calling thread until a buffer
        ///     becomes available.
        /// </summary>
        /// <returns>
        ///     An <see cref="ArraySegment&lt;T&gt;" /> with <see cref="BufferSize" /> as its
        ///     count.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="BlockingBufferManager" /> has been disposed.
        /// </exception>
        public ArraySegment<byte> GetBuffer()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(typeName);

            int index;
            try
            {
                index = _availableIndices.Take();
            }
            catch (InvalidOperationException)
            {
                throw new ObjectDisposedException(typeName);
            }

            _usedIndices[index] = index;
            return new ArraySegment<byte>(_data, index, BufferSize);
        }

        /// <summary>
        ///     Releases the specified buffer and makes it available for future use.
        /// </summary>
        /// <param name="buffer">
        ///     Buffer to release.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="buffer" />'s array is null, count is not <see cref="BufferSize" />,
        ///     or the offset is invalid; i.e. not taken from the current buffer manager.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="BlockingBufferManager" /> has been disposed.
        /// </exception>
        public void ReleaseBuffer(ArraySegment<byte> buffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(typeName);

            int offset;

            if (!buffer.Array.SequenceEqual(_data) ||
                buffer.Count != BufferSize ||
                !_usedIndices.TryRemove(buffer.Offset, out offset))
                throw new ArgumentException("Buffer is not taken from the current buffer manager.", nameof(buffer));

            try
            {
                _availableIndices.Add(offset);
            }
            catch (InvalidOperationException)
            {
                throw new ObjectDisposedException(typeName);
            }
        }
    }
}