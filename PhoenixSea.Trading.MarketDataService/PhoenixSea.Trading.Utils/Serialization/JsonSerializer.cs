using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PhoenixSea.Trading.Utils.Serialization
{
    public class JsonSerializer : IJsonSerializer
    {
        public void Serialize<T>(string destination, T instance)
        {
            throw new System.NotImplementedException();
        }

        public void Serialize<T>(string destination, IEnumerable<T> instances)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            var jsonString = Encoding.ASCII.GetString(bytes);
            return Deserialize<T>(jsonString);
        }

        public IEnumerable<T> DeserializeItems<T>(string source)
        {
            throw new System.NotImplementedException();
        }
    }
}