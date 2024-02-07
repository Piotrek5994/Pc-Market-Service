using Microsoft.Extensions.Hosting;
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
        private readonly IPcMarketService _service;

        public PcMarketWorker(IPcMarketService service)
        {
            _service = service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
