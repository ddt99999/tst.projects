using System.Collections.Generic;
using System.IO;

namespace PhoenixSea.Common.Core.Serialization
{
    public interface ISerializer
    {
        void Serialize<T>(Stream destination, T instance);
        void Serialize<T>(Stream destination, IEnumerable<T> instances);
        T Deserialize<T>(Stream source);
        T Deserialize<T>(byte[] bytes);
        IEnumerable<T> DeserializeItems<T>(Stream source);
    }
}