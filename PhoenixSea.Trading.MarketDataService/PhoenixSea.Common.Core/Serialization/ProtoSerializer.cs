using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace PhoenixSea.Common.Core.Serialization
{
    public class ProtoSerializer : IProtoSerializer
    {
        public void Serialize<T>(Stream destination, T instance)
        {
            Serializer.Serialize(destination, instance);
        }

        public void Serialize<T>(Stream destination, IEnumerable<T> instances)
        {
            foreach (var instance in instances)
                Serializer.Serialize(destination, instance);
        }

        public T Deserialize<T>(Stream source)
        {
            return Serializer.Deserialize<T>(source);
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
            return Serializer.DeserializeItems<T>(source, PrefixStyle.Base128, 0);
        }
    }
}