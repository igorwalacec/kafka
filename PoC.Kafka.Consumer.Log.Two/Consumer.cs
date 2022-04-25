using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Kafka.Consumer.Log.Two
{
    internal class Consumer : BackgroundService
    {
        private readonly ILogger<Consumer> _logger;
        private readonly IConsumer<Ignore, string> _consumer;

        public Consumer(ILogger<Consumer> logger, IConsumer<Ignore, string> consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {                        
            _logger.LogInformation("Testando o consumo de mensagens com Kafka");            
            try
            {
                while (true)
                {
                    var cr = _consumer.Consume(stoppingToken);
                    _logger.LogInformation(
                        $@"Mensagem lida: {cr.Message.Value}
                        Partition: {cr.Partition}
                        Topic Partition: {cr.TopicPartition}
                        Offset: {cr.Offset}
                        TopicPartitionOffset: {cr.TopicPartitionOffset}
                        ");
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
                _logger.LogWarning("Cancelada a execução do Consumer...");
            }                  
        }
    }
}
