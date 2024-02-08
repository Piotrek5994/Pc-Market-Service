using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Helper;
using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Worker
{
    public class PcMarketWorker : BackgroundService
    {
        private readonly IOptions<ActiveWorkerConfig> _config;
        private readonly Logger.Logger _log;
        private readonly WorkerHelper _workerHelper;

        public PcMarketWorker(IOptions<ActiveWorkerConfig> config,WorkerHelper workerHelper, Logger.Logger log)
        {
            _config = config;
            _workerHelper = workerHelper;
            _log = log;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_config.Value.Worker)
                {
                    _log.LogInformation($"Service not active - turned off.");
                    return;
                }

                _log.LogInformation($"Service is active started at : {DateTime.Now}");
                await _workerHelper.StartAsync(stoppingToken);
                await Task.Delay(Timeout.Infinite, stoppingToken);

            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _workerHelper.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
            _log.LogInformation($"Worker stopped at : {DateTime.Now}");
        }
    }
}
