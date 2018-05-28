using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PhoenixSea.Common.Core.Serialization
{
    public class BinarySerializer : IBinarySerializer
    {
        public void Serialize<T>(Stream destination, T instance)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(destination, instance);
        }

        public void Serialize<T>(Stream destination, IEnumerable<T> instances)
        {
            foreach (var instance in instances)
                Serialize(destination, instance);
        }

        public T Deserialize<T>(Stream source)
        {
            IFormatter formatter = new BinaryFormatter();
            return (T) formatter.Deserialize(source);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return Deserialize<T>(stream);
            }
        }

        public IEnumerable<T> DeserializeItems<T>(Stream source)
        {
            while (source.Position < source.Length)
                yield return Deserialize<T>(source);
        }
    }
}