using System.Collections.Generic;
using System.IO;
using ZeroFormatter;

namespace PhoenixSea.Common.Core.Serialization
{
    public class FastBinarySerializer : IFastBinarySerializer
    {
        public void Serialize<T>(Stream destination, T instance)
        {
            ZeroFormatterSerializer.Serialize(destination, instance);
        }

        public void Serialize<T>(Stream destination, IEnumerable<T> instances)
        {
            foreach (var instance in instances)
                Serialize(destination, instance);
        }

        public T Deserialize<T>(Stream source)
        {
            return ZeroFormatterSerializer.Deserialize<T>(source);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return ZeroFormatterSerializer.Deserialize<T>(bytes);
        }

        public IEnumerable<T> DeserializeItems<T>(Stream source)
        {
            return ZeroFormatterSerializer.Deserialize<IEnumerable<T>>(source);
        }
    }
}