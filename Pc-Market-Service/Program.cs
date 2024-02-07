using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Repository;

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
                    var databaseConfig = hostingContext.Configuration.GetSection("DatabaseConfig").Get<SqlConfiguration>();

                    // Add configuration for worker
                    services.Configure<ActiveWorkerConfig>(hostingContext.Configuration.GetSection("ActiveWorkerConfig"));
                    
                    // Construct the connection string
                    databaseConfig.ConnectionString = $"Server={databaseConfig.SqlDatabaseServer};" +
                                                      $"Database={databaseConfig.SqlDatabaseName};" +
                                                      "Trusted_Connection=True";

                    // Register the updated configuration
                    services.Configure<SqlConfiguration>(options => hostingContext.Configuration.GetSection("DatabaseConfig").Bind(options));

                    services.AddScoped<IPcMarketRepository, PcMarketRepository>();

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