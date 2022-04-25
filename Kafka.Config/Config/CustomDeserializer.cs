using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Poc.Kafka.Shared.Config
{
    public class CustomDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            string result = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
