using Microsoft.Extensions.Hosting;
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
        private readonly IPcMarketService _service;
        private readonly IPcMarketRepository _repository;
        //private readonly WorkerHelper _workerHelper;

        public PcMarketWorker(IPcMarketService service,IPcMarketRepository repository)//,WorkerHelper worker )
        {
            _service = service;
            _repository = repository;
            //_workerHelper = worker;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _repository.GetDocumentList();
                Console.WriteLine("test");
            }
            return null;
        }
    }
}
