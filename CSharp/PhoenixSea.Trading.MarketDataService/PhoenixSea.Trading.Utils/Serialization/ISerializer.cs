using System.Collections.Generic;

namespace PhoenixSea.Trading.Utils.Serialization
{
    public interface ISerializer<in TInput>
    {
        void Serialize<T>(TInput destination, T instance);
        void Serialize<T>(TInput destination, IEnumerable<T> instances);
        T Deserialize<T>(TInput source);
        T Deserialize<T>(byte[] bytes);
        IEnumerable<T> DeserializeItems<T>(TInput source);
    }
}