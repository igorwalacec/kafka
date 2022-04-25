using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Kafka.Shared.Config;

namespace PoC.Kafka.Consumer.Log.Two
{
    class Program
    {
        static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, service) =>
            {
                service.AddConsumer<string>("^ECOMMERCE*","dados");
                service.AddHostedService<Consumer>();
            }).AddSerilog();
    }
}
