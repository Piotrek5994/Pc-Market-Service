using Pc_Market_Service.Repository.IRepository;
using Pc_Market_Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Service
{
    public class PcMarketService : IPcMarketService
    {
        private readonly IPcMarketRepository _repository;

        public PcMarketService(IPcMarketRepository repository)
        {
            _repository = repository;
        }
    }
}
