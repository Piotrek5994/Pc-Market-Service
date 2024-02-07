using Microsoft.Extensions.Options;
using Pc_Market_Service.Configuration;
using Pc_Market_Service.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Repository
{
    public class PcMarketRepository : IPcMarketRepository
    {
        private readonly SqlConfiguration _sqlConfiguration;
        private readonly Logger.Logger _log;

        public PcMarketRepository(IOptions<SqlConfiguration> sqlConfiguration, Logger.Logger log)
        {
            _sqlConfiguration = sqlConfiguration.Value;
            _log = log;
        }
    }
}
