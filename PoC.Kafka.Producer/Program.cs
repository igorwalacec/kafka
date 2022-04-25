using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Kafka.Shared.Config;
using PoC.Kafka.Shared;
using System;
using System.Threading.Tasks;

namespace PoC.Kafka.Producer
{
    class Program
    {
        static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, service) =>
            {
                service.AddHostedService<Producer>();
                service.AddPublisher<string, string>();
                service.AddPublisher<string, Order>(new CustomSerializer<Order>());
            }).AddSerilog();
    }
}