using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Kafka.Shared.Config;
using PoC.Kafka.Shared;

namespace PoC.Kafka.Consumer
{
    class Program
    {
        static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, service) =>
           {
               service.AddConsumer("ECOMMERCE_NEW_ORDER", new CustomDeserializer<Order>());
               service.AddHostedService<Consumer>();
           }).AddSerilog();
    }
}
