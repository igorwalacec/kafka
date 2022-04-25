using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace Poc.Kafka.Shared.Config
{
    public class CustomSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            var stringMessage = JsonConvert.SerializeObject(data, Formatting.None);
            var bytes = Encoding.UTF8.GetBytes(stringMessage);
            return bytes;
        }
    }
}
