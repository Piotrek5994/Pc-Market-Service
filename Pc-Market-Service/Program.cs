using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;

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
                    // Konfiguracja opcji
                    var databaseConfig = hostingContext.Configuration.GetSection("DatabaseConfig").Get<SqlConfiguration>();

                    // Skonstruuj ciąg połączenia
                    databaseConfig.ConnectionString = $"Server={databaseConfig.SqlDatabaseServer};" +
                                                      $"DataBase={databaseConfig.SqlDatabaseName};" +
                                                      "Trusted_Connection=True;MultipleActiveResultSets=true";

                    // Zarejestruj zaktualizowaną konfigurację
                    services.Configure<SqlConfiguration>(options => hostingContext.Configuration.GetSection("DatabaseConfig").Bind(options));

                    services.AddSingleton(databaseConfig); // Dodanie zaktualizowanego obiektu SqlConfiguration

                    services.AddSingleton<IConfiguration>(hostingContext.Configuration);

                    // Rejestrowanie usług
                    //services.AddSingleton<IPurchaseInvoiceService, PurchaseInvoiceService>();
                    // Kontynuuj rejestrowanie innych usług zgodnie z twoimi potrzebami...

                    // Rejestrowanie repozytoriów
                    //services.AddSingleton<IOptimaApiRepository, OptimaApiRepository>();
                    // Kontynuuj rejestrowanie innych repozytoriów...

                    // Rejestrowanie workerów jako usług hostowanych
                    //services.AddHostedService<PzWorker>();
                    // Kontynuuj rejestrowanie innych workerów...

                    services.AddSingleton<Logger.Logger>();
                    // Konfiguracja logowania
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