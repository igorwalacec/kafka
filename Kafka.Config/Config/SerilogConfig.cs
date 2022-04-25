using Microsoft.Extensions.Hosting;
using Serilog;

namespace Poc.Kafka.Shared.Config
{
    public static class SerilogConfig
    {
        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((context, logger) => {
                logger.WriteTo.Console();           
            });            
        }
    }
}
