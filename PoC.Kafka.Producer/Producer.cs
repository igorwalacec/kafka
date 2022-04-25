using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PoC.Kafka.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoC.Kafka.Producer
{
    internal class Producer : BackgroundService
    {
        private readonly ILogger<Producer> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly IProducer<string, Order> _producerOrder;

        public Producer(ILogger<Producer> logger, IProducer<string, string> producer, IProducer<string, Order> producerOrder)
        {
            _logger = logger;
            _producer = producer;
            _producerOrder = producerOrder;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Testando envio de mensagens com kafka");

            string newOrderTopic = "ECOMMERCE_NEW_ORDER";
            string sendEmailTopic = "ECOMMERCE_SEND_EMAIL";

            try
            {                
                    var valueNewOrder = "123,456,789";
                    for (int i = 0; i < 10; i++)
                    {
                        var key = Guid.NewGuid().ToString();
                        var resultNewOrder = await _producerOrder.ProduceAsync(
                            newOrderTopic,
                            new Message<Null , string>
                            { 
                                Key = null,
                                Value = ""
                            });

                        _logger.LogInformation(
                                @$"Mensagem: {valueNewOrder} | 
                                Status: { resultNewOrder.Status} |
                                Partition: {resultNewOrder.Partition}
                                Topic Partition: {resultNewOrder.TopicPartition}");
                    }

                    var valueSendEmail = "Bem vindo, estamos processando seu pedido!";
                    var resultSendEmail = await _producer.ProduceAsync(
                        sendEmailTopic,
                        new Message<string, string>
                         {
                             Key = $"{Guid.NewGuid()}",
                             Value = valueSendEmail
                         });


                _logger.LogInformation(
                        $@"Mensagem: {valueSendEmail} | 
                        Status: { resultSendEmail.Status} |
                        Partition: {resultSendEmail.Partition}
                        Topic Partition: {resultSendEmail.TopicPartition}");


                _logger.LogInformation("Concluído o envio de mensagens");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exceção: {ex.GetType().FullName} | " +
                             $"Mensagem: {ex.Message}");
            }
        }
    }
}
