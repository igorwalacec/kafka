using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace Poc.Kafka.Shared.Config
{
    public static class AddProducer
    {        
        public static IServiceCollection AddPublisher<TKey, TValue>(this IServiceCollection serviceColletion, ISerializer<TValue> serializerValue = null)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = Config.BootstrapServer
            };
            var producer = new ProducerBuilder<TKey, TValue>(config);
            if (serializerValue != null)
            {   
                producer.SetValueSerializer(serializerValue);
            }            
            serviceColletion.AddSingleton(producer.Build());

            return serviceColletion;
        }       

        public static IServiceCollection AddConsumer<T>(this IServiceCollection serviceCollection, string topicName, IDeserializer<T> serializer = null) 
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = Config.BootstrapServer,
                GroupId = $"{topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Ignore, T>(config).SetValueDeserializer(serializer).Build();                
            consumer.Subscribe(topicName);
            serviceCollection.AddSingleton(consumer);

            return serviceCollection;
        }
        public static IServiceCollection AddConsumer<T>(this IServiceCollection serviceCollection, string topicName, string groupId, IDeserializer<T> serializer = null)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = Config.BootstrapServer,
                GroupId = $"{groupId}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Ignore, T>(config).SetValueDeserializer(serializer).Build();
            consumer.Subscribe(topicName);
            serviceCollection.AddSingleton(consumer);

            return serviceCollection;
        }
    }
}
