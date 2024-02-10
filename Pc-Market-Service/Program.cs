using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Repository;
using Pc_Market_Service.Service.IService;
using Pc_Market_Service.Service;
using Pc_Market_Service.Worker;
using Pc_Market_Service.Helper;
using Pc_Market_Service.Email;

namespace Pc_Market_Service
{
    public class Program
    {


        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    // Configuration options setup
                    services.Configure<SqlConfiguration>(hostingContext.Configuration.GetSection("DatabaseConfig"));

                    // Add configuration for worker
                    services.Configure<ActiveWorkerConfig>(hostingContext.Configuration.GetSection("ActiveWorkerConfig"));

                    // Register the updated configuration
                    services.Configure<SqlConfiguration>(options => hostingContext.Configuration.GetSection("DatabaseConfig").Bind(options));

                    services.AddSingleton<IPcMarketRepository, PcMarketRepository>();

                    services.AddSingleton<IPcMarketService, PcMarketService>();

                    services.AddSingleton<WorkerHelper>();

                    services.AddSingleton<SendingEmails>();

                    services.AddHostedService<PcMarketWorker>();

                    services.AddSingleton<IConfiguration>(hostingContext.Configuration);

                    services.AddSingleton<Logger.Logger>();

                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddConsole(configure =>
                        {
                            configure.LogToStandardErrorThreshold = LogLevel.Information;
                        });
                    });
                });

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}